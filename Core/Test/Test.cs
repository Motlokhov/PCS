using System;
using System.Collections.Generic;

namespace Core.Test
{
    using Common;
    
    public class Test:Entity
    {
        public bool ShouldValuesBeRepeated { get; set; }
        public bool IsActive { get; set; }

        public Parameter[] Parameters { get; set; }

        private Test(int number, string name, bool shouldValuesBeRepeated, Parameter[] parameters) 
        {
            ID = number;
            Name = name;
            ShouldValuesBeRepeated = shouldValuesBeRepeated;
            Parameters = parameters;
            IsActive = false;
        }

        public bool CheckRepeatedValues()
        {
            if (!ShouldValuesBeRepeated)
            {
                string meaning = null;
                foreach (var parameter in Parameters)
                {
                    
                    foreach (var value in parameter.Values)
                    {
                        if (meaning != value.Meaning)
                        {
                            meaning = value.Meaning;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public static Test Construct(int number,string name,bool shouldValuesBeRepeated,Parameter[] parameters) 
        {
            return new Test(number, name, shouldValuesBeRepeated, parameters);
        }
    }
}
