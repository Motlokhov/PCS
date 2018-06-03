using System;
using System.Collections.ObjectModel;


namespace Core.Test
{
    public class MeaningFromOtherValues
    {
        public ObservableCollection<Value> Values { get; private set; }
        private Value _targetValue;

        private MeaningFromOtherValues(ObservableCollection<Value> values) 
        {
            // rewrite to contains only values for summing
            Values = values;
            AddEvents();
        }

        public void SetTarget(Value target)
        {
            _targetValue = target; 
        }

        private void AddEvents()
        {
             foreach (var value in Values)
             {
                 value.PropertyChanged += value_PropertyChanged;
             }
        }

        private void value_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            int meaning = 0;
            foreach (var value in Values)
            {
                if (!string.IsNullOrEmpty(value.Meaning))
                {
                    int tempInt;
                    if (int.TryParse(value.Meaning, out tempInt))
                    {
                        meaning += tempInt;
                    }
                    else
                    {
                        value.Meaning = "";
                    }
                }
            }
            _targetValue.Meaning = meaning.ToString();
        }



        private void DeleteEvents()
        {
            foreach (var value in Values)
            {
                value.PropertyChanged -= value_PropertyChanged;
            }
        }      

      

        public static MeaningFromOtherValues Construct(ObservableCollection<Value> values) 
        {
            return new MeaningFromOtherValues(values);
        }
    }
}
