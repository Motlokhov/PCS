using System;

namespace Core.Person
{
    using Enums;
    using Database;

    public class Tested:Person
    {
        private CompositionOfFamily _composition;
        private Education _education;
        private Detained _detained;
        private Defect _defect;
        private SuicideInFamily _suicide;

        public string Composition 
        { 
            get 
            { 
                return EnumUtils.ValueOf(_composition); 
            } 
            set 
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _composition = (CompositionOfFamily)EnumUtils.EnumValueOf(value, typeof(CompositionOfFamily));
                }
            } 
        }
        public string Education 
        {
            get
            {
                return EnumUtils.ValueOf(_education);
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _education = (Education)EnumUtils.EnumValueOf(value, typeof(Education));
                }
            }
        }
        public string Detained
        {
            get
            {
                return EnumUtils.ValueOf(_detained);
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _detained = (Detained)EnumUtils.EnumValueOf(value, typeof(Detained));
                }
            }
        }
        public string Defect 
        {
            get
            {
                return EnumUtils.ValueOf(_defect);
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _defect = (Defect)EnumUtils.EnumValueOf(value, typeof(Defect));
                }
            }
        }
        public string Suicide 
        {
            get
            {
                return EnumUtils.ValueOf(_suicide);
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _suicide = (SuicideInFamily)EnumUtils.EnumValueOf(value, typeof(SuicideInFamily));
                }
            }
        }

        private Tested()
        { }

        private Tested(int id)
        {
            using (var query = Database.Construct())
            {
                var reader = query.ReadData(string.Format("SELECT * FROM DataTested WHERE [ID] = {0}",id));
                if(reader.Read())
                {
                    Name = reader["Name"].ToString();
                    Surname = reader["Surname"].ToString();
                    Lastname = reader["Lastname"].ToString();
                    _composition = (CompositionOfFamily)Convert.ToInt16(reader["Family"]);
                    _defect = (Defect)Convert.ToInt16(reader["Defect"]);
                    _detained = (Detained)Convert.ToInt16(reader["Detained"]);
                    _education = (Education)Convert.ToInt16(reader["Education"]);
                    _suicide = (SuicideInFamily)Convert.ToInt16(reader["Suicide"]);
                    return;
                }
                throw new Exception("Не верный параметер функции {id} = " + id);
            }
        }

        public static Tested Construct(int id)
        {
            return new Tested(id);
        }

        public static Tested Construct()
        {
            return new Tested();
        }
    }
}
