using System;


namespace Core.Limitation
{
    public class CriticalLimit:ILimitable
    {
        private int _criticalLimit;

        private CriticalLimit(int criticalLimit) 
        {
            _criticalLimit = criticalLimit;
        }

        public dynamic CheckLimit(dynamic value)
        {
            if (value > _criticalLimit)
            {
                return "Значение превышает критический лимит";
            }
            return null;
        }

        public static CriticalLimit Construct(int criticalLimit) 
        {
            return new CriticalLimit(criticalLimit);
        }
    }
}
