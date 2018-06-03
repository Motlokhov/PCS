using System;
using System.Collections.Generic;

namespace Core.Test
{
    using Person;
    using Enums;
    using Database;

    public class Result
    {
        public int Number                  { get; private set; }
        public Psycologist Psy             { get; private set; }
        public Tested Tested               { get; private set; }
        public Method Method               { get; private set; }
        public string Date                 { get; private set; }
        public List<string> Interpretation { get; private set; }


        private Result(int number, Psycologist psy, Tested tested, Method method, string date, List<string> interpretation)
        {
            Number = number;
            Psy = psy;
            Tested = tested;
            Method = method;
            Date = date;
            Interpretation = interpretation;
        }

        public static List<Result> LastResults()
        {
                var listResult = new List<Result>();
                using (var query = Database.Construct())
                {
                    var reader = query.ReadData("SELECT [ID],[PsychologistID],[TestedID],[Method],[Date] FROM [Testing]");
                    while (reader.Read())
                    {
                        int number = listResult.Count + 1;
                        var psy = Psycologist.Construct((int)reader["PsychologistID"]);
                        var tested = Tested.Construct((int)reader["TestedID"]);
                        Method method = (Method)reader["Method"];
                        string date = reader["Date"].ToString();
                        var list = new List<string>();
                        int testingID = Convert.ToInt32(reader["ID"]);

                        using (var query2 = Database.Construct())
                        {
                            var reader2 = query2.ReadData(string.Format("SELECT [Text] FROM [TestingResult] WHERE [TestingID] = {0}", testingID));
                            while (reader2.Read())
                            {
                                list.Add(reader2["Text"].ToString());
                            }
                        }

                        listResult.Add(Result.Construct(number, psy, tested, method, date, list));
                    }
            }
                return listResult;
        }

        public static Result Construct(int number, Psycologist psy, Tested tested, Method method, string date, List<string> interpretation)
        {
            return new Result(number, psy, tested, method, date, interpretation);
        }

    }
}
