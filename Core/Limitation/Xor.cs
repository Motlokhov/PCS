using System;
using System.Windows.Forms;

namespace Core.Limitation
{
    public class Xor:ILimitable
    {
        private string[] _xorLimit;

        private Xor(string[] xorLimit) 
        {
            _xorLimit = xorLimit;
        }

        public dynamic CheckLimit(dynamic value)
        {
            foreach(var limit in _xorLimit)
            {   
                if(limit == value)
                {
                    return true;
                }
            }
            string xorLimit = "";
            foreach (var limit in _xorLimit)
            {
                xorLimit += limit + ",";
            }
            xorLimit.Remove(xorLimit.Length - 1);
            MessageBox.Show(string.Format("Значение должно быть одним из значений '{0}'.", xorLimit));
            return false;
        }

        public static Xor Construct(string[] xorLimit)
        {
            return new Xor(xorLimit);
        }
    }
}
