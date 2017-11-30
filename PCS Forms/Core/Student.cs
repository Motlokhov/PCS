

namespace PCS_Forms.Core
{
    using Database;
    using System;
    public class Student
    {
        public int Id { get; private set; }
        public string Name { get; protected set; }
        public string Surname { get; protected set; }
        public string Lastname { get; protected set; }
        public Background Background { get; private set; }

        public Student(string name,string surname,string lastname, Background background)
        {
            this.AddFullName(name, surname, lastname);
            this.Background=background;
        }

        public Student(int id)
        {
            this.Id = id;
            Database database = new Database();
            database.ReadData("SELECT Name,Surname,Lastname,Education,Family,Detained,Defect,Suicide FROM DataTested WHERE Id = " + id);
            database.Reader.Read();
            this.AddFullName(
                database.Reader["Name"].ToString(),
                database.Reader["Surname"].ToString(),
                database.Reader["Lastname"].ToString());
            this.Background = new Background(
                 (Education)Convert.ToByte(database.Reader["Education"]),
                 (Composition_of_family)Convert.ToByte(database.Reader["Family"]),
                 (Detained)Convert.ToByte(database.Reader["Detained"]),
                 (Defect)Convert.ToByte(database.Reader["Defect"]),
                 (Suicide_in_family)Convert.ToByte(database.Reader["Suicide"]));
            database.ConnectionClose();
        }

        private void AddFullName(string name, string surname, string lastname)
        {
            this.Name = name;
            this.Surname = surname;
            this.Lastname = lastname;
        }

        public string ReturnFullName()
        {
            return Surname + " " + Name + " " + Lastname;
        }

        public string ValueOfEducation()
        {
            return EnumUtils.ValueOf(this.Background.Education);
        }

        public string ValueOfDetained()
        {
            return EnumUtils.ValueOf(this.Background.Detained);
        }

        public string ValueOfDefect()
        {
            return EnumUtils.ValueOf(this.Background.Defect);
        }

        public string ValueOfSuicide()
        {
            return EnumUtils.ValueOf(this.Background.Suicide);
        }

        public string ValueOfFamily()
        {
            return EnumUtils.ValueOf(this.Background.Family);
        }

        
    }
}
