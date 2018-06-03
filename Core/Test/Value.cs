using System;
using System.ComponentModel;
using System.Windows;

namespace Core.Test
{
    public class Value:INotifyPropertyChanged
    {
        public dynamic Meaning
        {
            get
            {
                return _meaning;
            }
            set
            {
                    string val = Convert.ToString(value);
                    if (!string.IsNullOrEmpty(val.Trim()) && _parameter.Limitation.CheckLimit(val))
                    {
                        _meaning = value;
                    }
                    else
                    {
                        _meaning = "";
                    }
                    if (PropertyChanged != null)
                    {
                        NotifyPropertyChanged("Meaning");
                    }
            }
        }

        private dynamic _meaning;
        private Parameter _parameter;
        public MeaningFromOtherValues SumFromOtherValues { get; private set; }
        public bool IsEnable { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public Value(Parameter parameter, MeaningFromOtherValues sumFromOtherValues)
        {
            _parameter = parameter;
            SumFromOtherValues = sumFromOtherValues;
            IsEnable = true;
            if (SumFromOtherValues != null)
            {
                SumFromOtherValues.SetTarget(this);
                IsEnable = false;
            }
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
       
    }
}
