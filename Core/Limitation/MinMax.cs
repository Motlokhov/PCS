using System;
using System.Windows.Forms;

namespace Core.Limitation
{
    public class MinMax:ILimitable
    {
        private int _min;
        private int _max;


        private MinMax(int min, int max) 
        {
            _min = min;
            _max = max;
        }

        public dynamic CheckLimit(dynamic value)
        {
            int intValue = Convert.ToInt32(value);
            if (int.TryParse(value.ToString(), out intValue)) 
            {
                if (intValue >= _min && intValue <= _max)
                {
                    return true;
                }
            }
            MessageBox.Show(string.Format("Значение должно находиться в пределе от {0} до {1}.", _min, _max));
            return false;
        }

        public static MinMax Construct(int min, int max) 
        {
            return new MinMax(min, max);
        }
    }
}
