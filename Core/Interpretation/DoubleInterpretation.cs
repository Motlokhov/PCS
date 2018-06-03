using System;

namespace Core.Interpretation
{
    using Core.Test;
    using Database;
    public class DoubleInterpretation :IInterpretable
    {


        string IInterpretable.Interpretate(Value[] values, int methodNumber, int testNumber, int parameterNumber)
        {
            if (values.Length != 2)
            {
                throw new ArgumentException("Значение 'values' имеет неверное входное значение");
            }
            if (values == null)
            {
                throw new NullReferenceException("Значение 'values' явлется null");
            }
            string firstValue = values[0].Meaning;
            string secondValue = values[1].Meaning;
            using (var database = Database.Construct())
            {
                if (string.IsNullOrEmpty(firstValue) && string.IsNullOrEmpty(secondValue))
                {
                    throw new Exception("Не все активные поля заполненны");
                }
                string commandText = string.Format("SELECT [Definition] FROM [Interpretation] WHERE [MethodNumber] = {0} AND [TestNumber] = {1} AND [ParameterNumber] = {2} AND [FirstValue] >= {3} AND [SecondValue] >= {4} ORDER BY [FirstValue],[SecondValue]", methodNumber, testNumber, parameterNumber,firstValue,secondValue);
                string definition = database.ExecuteScalar(commandText).ToString();
                return definition;
            }
        }

        public static DoubleInterpretation Construct() 
        {
            return new DoubleInterpretation();
        }
    }
}
