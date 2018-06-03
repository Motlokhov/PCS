using System;

namespace Core.Interpretation
{
    using Core.Test;
    using Database;
    public class SingleInterpretation:IInterpretable
    {


        string IInterpretable.Interpretate(Value[] values, int methodNumber, int testNumber, int parameterNumber)
        {
            if (values.Length != 1) 
            {
                throw new ArgumentException("Значение 'values' имеет неверное входное значение");
            }
            if (values == null)
            {
                throw new NullReferenceException("Значение 'values' явлется null");
            }

            string value = values[0].Meaning;
            using (var database = Database.Construct())
            {
                if(string.IsNullOrEmpty(value))
                {
                    throw new Exception("Не все активные поля заполненны");
                }
                string commandText = string.Format("SELECT [Definition] FROM [Interpretation] WHERE [FirstValue] <= {3} AND [SecondValue] >= {3} AND [MethodNumber] = {0} AND [TestNumber] = {1} AND [ParameterNumber] = {2}", methodNumber, testNumber, parameterNumber,value);
                string definition = database.ExecuteScalar(commandText).ToString();
                if (string.IsNullOrEmpty(definition))
                {
                    return null;
                }
                return definition;
            }
        }


        public static SingleInterpretation Construct() 
        {
            return new SingleInterpretation();
        }

    }
}
