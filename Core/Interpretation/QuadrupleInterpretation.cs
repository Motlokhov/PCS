using System;


namespace Core.Interpretation
{
    using Core.Test;
    using Database;
    public class QuadrupleInterpretation :IInterpretable
    {

        string IInterpretable.Interpretate(Value[] values, int methodNumber, int testNumber, int parameterNumber)
        {
            if (values.Length != 4)
            {
                throw new ArgumentException("Значение 'values' имеет неверное входное значение");
            }
            if (values == null)
            {
                throw new NullReferenceException("Значение 'values' явлется null");
            }
            string firstValue = string.Concat(values[0].Meaning,values[1].Meaning);
            string secondValue = string.Concat(values[2].Meaning, values[3].Meaning);
            using (var database = Database.Construct())
            {
                if (string.IsNullOrEmpty(firstValue) && string.IsNullOrEmpty(secondValue))
                {
                    throw new Exception("Не все активные поля заполненны");
                }
                string commandText = string.Format("SELECT [Definition] FROM [Interpretation] WHERE [MethodNumber] = {0} AND [TestNumber] = {1} AND [ParameterNumber] = {2} AND [FirstValue] = '{3}' AND [SecondValue] = '{4}' ORDER BY [FirstValue],[SecondValue]", methodNumber, testNumber, parameterNumber, firstValue, secondValue);
                string definition = database.ExecuteScalar(commandText).ToString();
                return definition;
            }
        }

        public static QuadrupleInterpretation Construct() 
        {
            return new QuadrupleInterpretation();
        }
    }
}
