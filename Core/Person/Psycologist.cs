using System;


namespace Core.Person
{
    using Database;

    public class Psycologist:Person
    {
        private string _password;
        public bool CheckPassword(string password) 
        {
            if (_password != password) 
            {
                return false;
            }
            return true;
        }

        private Psycologist()
        { }

        private Psycologist(int id)
        {
            using (var query = Database.Construct())
            {
                var reader = query.ReadData(string.Format("SELECT [Name],[Surname],[Lastname],[Password] FROM Psychologist WHERE ID = {0}",id));
                if(reader.Read())
                {
                    ID = id;
                    Name = reader["Name"].ToString();
                    Surname = reader["Surname"].ToString();
                    Lastname = reader["Lastname"].ToString();
                    _password = reader["Password"].ToString();
                    return;
                }
                throw new Exception("Не верный параметер функции {id} = " + id);
            }
        }

        

        public static Psycologist Construct(int id)
        {
            return new Psycologist(id);
        }

        public static Psycologist Construct()
        {
            return new Psycologist();
        }
        
        public bool AddNewToDataBase(string password)
        {
            using (var query = Database.Construct())
            {
                var count = query.ExecuteNonQuery(string.Format("INSERT INTO Psychologist ([Name],[Surname],[LastName],[Password]) VALUES ('{0}','{1}','{2}','{3}')", Name, Surname, Lastname, password));
                if (count == 1)
                {
                    _password = password;
                    return true;
                }
                return false;
            }
        }
    }
}
