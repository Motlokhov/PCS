using Core.Enums;
using Core.Factory;
using Core.Person;
using Core.Test;
using System;
using Database;
using System.Collections.Generic;

namespace PCS_Forms
{
    using Database;
    
    public class Core
    {
        static Core self;

        private Method _method = (Method)2;
        public Psycologist Psy { get; set; }
        public Tested Tested { get; set; }        
        public Test[] Tests { get; set; }

        public String Date { 
            get
            {
                return _date.ToShortDateString();
            }
            set { 
                _date = Convert.ToDateTime(value);
            }
        }

        private DateTime _date;

        public string Method
        {
            get
            {
                return EnumUtils.ValueOf(_method);
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    //_method = (Method)EnumUtils.EnumValueOf(value, typeof(Method));
                    CreateTests();
                }
            }
        }



        private Core()
        {
            _date = DateTime.Today;
            Tested = Tested.Construct();
        }

        public void CreateTests()
        {

            if (Tests == null)
            {
                Pav2TestCreator creator = new Pav2TestCreator();
                Tests = creator.Create();

            }
        }

        public static Core Construct()
        {
            if (Core.self == null)
            {
                self = new Core();
            }
            return self;
        }

        public static bool CheckActiveUser()
        {
            using (var query = Database.Construct())
            {
                var reader = query.ReadData("SELECT [ID] FROM [Psychologist] WHERE [IsActive] = 1 ORDER BY [ID]");
                if (reader.Read())
                {
                    int id = Convert.ToInt32(reader["ID"]);
                    var psy = Psycologist.Construct(id);
                    self.Psy = psy;
                    return true;
                }
                return false;
            }
        }

        public static void SetAccountAsUnactive()
        {
            using (var query = Database.Construct())
            {
                query.ExecuteScalar("UPDATE Psychologist SET IsActive = 0");
                
            }
        }

        public void VerifyFields()
        {
            if (string.IsNullOrEmpty(Tested.Name) && string.IsNullOrEmpty(Tested.Surname))
            {
                throw new Exception("Поля тестируемого не заполненны ('Имя' или 'Фамилия').");
            }
            if (_date.Year < 1980 )
            {
                throw new Exception("Поле даты не заполненно");
            }
        }

        public Result Interpretation()
        {
            VerifyFields();
            var list = new List<string>();
            foreach(var test in Tests)
            {
                if (test.IsActive)
                {
                    if (!test.CheckRepeatedValues())
                    {
                        list.Add("Данные по тесту недостоверны необходимо повторное прохождение тестирования");
                    }
                    else
                    {
                        foreach (var parameter in test.Parameters)
                        {
                            if (parameter.Interpretation != null)
                            {
                                string tempInterpretation = parameter.Interpretation.Interpretate(parameter.Values, Convert.ToInt16(EnumUtils.EnumValueOf(Method, typeof(Method))), test.ID, parameter.ID);
                                if (!string.IsNullOrEmpty(tempInterpretation))
                                {
                                    list.Add(tempInterpretation);
                                }
                            }
                        }
                    }
                }
            }
            return Result.Construct(1, Psy, Tested, (Method)EnumUtils.EnumValueOf(Method,typeof(Method)), Date, list);
        }


        public void SaveResult(Result result)
        {
            using (var query = Database.Construct())
            {
               var count = query.ExecuteNonQuery(string.Format("INSERT INTO [DataTested] ([Name],[Surname],[Lastname],[Education],[Family],[Detained],[Defect],[Suicide]) VALUES ('{0}','{1}','{2}',{3},{4},{5},{6},{7})",
                    result.Tested.Name, 
                    result.Tested.Surname, 
                    result.Tested.Lastname, 
                    Convert.ToInt16(EnumUtils.EnumValueOf(result.Tested.Education,typeof(Education))), 
                     Convert.ToInt16(EnumUtils.EnumValueOf(result.Tested.Composition,typeof(CompositionOfFamily))), 
                     Convert.ToInt16(EnumUtils.EnumValueOf(result.Tested.Detained,typeof(Detained))),
                     Convert.ToInt16(EnumUtils.EnumValueOf(result.Tested.Defect, typeof(Defect))),
                     Convert.ToInt16(EnumUtils.EnumValueOf(result.Tested.Suicide,typeof(SuicideInFamily)))
                    ));
               if (count == 1)
               {
                   var reader = query.ReadData("SELECT MAX([ID]) AS ID FROM [DataTested]");
                   if (reader.Read())
                   {
                       var testedID = reader["ID"];
                       count = query.ExecuteNonQuery(string.Format("INSERT INTO [Testing] ([PsychologistID],[TestedID],[Date],[Method]) VALUES ({0},{1},'{2}','{3}')", result.Psy.ID,testedID,result.Date,Convert.ToInt16(result.Method)));
                       if (count == 1)
                       {
                           reader = query.ReadData("SELECT MAX([ID]) AS ID FROM [Testing]");
                           if (reader.Read())
                           {
                               var testingID = reader["ID"];
                               foreach (var item in result.Interpretation)
                               {
                                   query.ExecuteNonQuery(string.Format("INSERT INTO [TestingResult] ([TestingID],[Text]) VALUES ({0},'{1}')", testingID, item));
                               }
                           }
                       }
                   }
               }
                
            }
        }

        public void ClearResult()
        {
            Tested.Name = Tested.Lastname = Tested.Surname = "";
            foreach (var test in Tests)
            {
                foreach (var parameter in test.Parameters)
                {
                    foreach (var value in parameter.Values)
                    {
                        value.Meaning = "";
                    }
                }
            }
        }
    }
}
