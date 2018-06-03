using System;


namespace Core.Limitation
{
    public interface ILimitable
    {
        dynamic CheckLimit(dynamic value);
       
    }
}
