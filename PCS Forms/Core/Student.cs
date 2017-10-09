

namespace PCS_Forms.Core
{
    public class Student
    {
        public string Name { get; protected set; }
        public string Surname { get; protected set; }
        public string Lastname { get; protected set; }
        public Background Background;

        public Student(string name,string surname,string lastname, Background background)
        {
            this.Name = name;
            this.Surname= surname;
            this.Lastname=lastname;
            this.Background=background;
        }

        public string ReturnFullName()
        {
            return Surname + " " + Name + " " + Lastname;
        }

        


    }
}
