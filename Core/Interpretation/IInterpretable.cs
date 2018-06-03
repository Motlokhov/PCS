using Core.Test;
using System;


namespace Core.Interpretation
{
   public interface IInterpretable
    {

        string Interpretate(Value[] values, int methodNumber, int testNumber, int parameterNumber);

    }
}
