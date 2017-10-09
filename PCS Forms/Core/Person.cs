using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCS_Forms.Core
{
   public class Person
    {
        public string Name { get; protected set; }
        public string Surname { get; protected set; }
        public string Lastname { get; protected set; }

        public Person(string name, string surname, string lastname)
        {
            this.Name = name;
            this.Surname = surname;
            this.Lastname = lastname;
        }

        public string ReturnFullName()
        {
            return Surname + " " + Name + " " + Lastname;
        }
    }
}
