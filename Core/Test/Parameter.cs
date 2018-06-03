using System;
using System.Collections.ObjectModel;
namespace Core.Test
{
    using Common;
    using Interpretation;
    using Limitation;

     public class Parameter:Entity
    {
        public Value[] Values { get; set; }
        public ILimitable Limitation { get; private set; }
        public IInterpretable Interpretation { get; private set; }
        public CriticalLimit CriticalLimit { get; private set; }

        private Parameter(int number,string name, int countValues, ILimitable limitation, IInterpretable interpretation, CriticalLimit criticalLimit, MeaningFromOtherValues sumFromOtherValues) 
        {
            Name = name;
            ID = number;
            Limitation = limitation;
            Interpretation = interpretation;
            CriticalLimit = criticalLimit;
            Values = new Value[countValues];

            for (var i = 0; i < countValues; i++)
            {
                Values[i] = new Value(this, sumFromOtherValues);
            }

        }


        public static Parameter Conctruct(int number, string name, int countValues, ILimitable limitation,IInterpretable interpretation, CriticalLimit criticalLimit, MeaningFromOtherValues sumFromOtherParameters) 
        {
            return new Parameter(number, name, countValues, limitation, interpretation, criticalLimit, sumFromOtherParameters);
        }

    }
}
