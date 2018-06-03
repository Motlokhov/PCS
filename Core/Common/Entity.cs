using System;
using System.ComponentModel;

namespace Core.Common
{
    public class Entity:Identity,INotifyPropertyChanged
    {
        private string _name;
        public string Name 
        {
            get 
            { 
                return _name; 
            }
            set 
            { 
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

       
    }
}
