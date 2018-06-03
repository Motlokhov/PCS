using System;


namespace Core.Person
{
    using Common;
    public class Person:Entity
    {
        public string Surname 
        {
            get
            {
                return _surname;
            }
            set
            {
                _surname = value;
                NotifyPropertyChanged("Surname");
            }
        }
        public string Lastname 
        {
            get
            {
                return _lastname;
            }
            set
            {
                _lastname = value;
                NotifyPropertyChanged("Lastname");
            }
        }

        private string _surname;
        private string _lastname;

        public string FullName { 
            get 
            {
                return string.Format("{0} {1} {2}", Surname, Name, Lastname);
            } 
        }

    }
}
