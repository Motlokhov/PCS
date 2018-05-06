using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCS_Forms.Core
{
    using Database;
   public class Psychologist
    {
       public int Id { get; private set; }
        public string Name { get; protected set; }
        public string Surname { get; protected set; }
        public string Lastname { get; protected set; }
        private string Password;

        public Psychologist(int id)
        {
            
            Database database = new Database();
            database.ReadData("SELECT Id,Name,Surname,Lastname,Password FROM Psychologist WHERE Id =" + id);
            database.Reader.Read();
            this.Id = (int)database.Reader["Id"];
            this.Name = database.Reader["Name"].ToString();
            this.Surname = database.Reader["Surname"].ToString();
            this.Lastname = database.Reader["Lastname"].ToString();
            this.Password = database.Reader["Password"].ToString();
        }

        public bool CheckPassword(string written_password)
        {
            if (this.Password != written_password)
                return false;
            else
                return true;
        }
        public string ReturnFullName()
        {
            return Surname + " " + Name + " " + Lastname;
        }

      
    }
}
