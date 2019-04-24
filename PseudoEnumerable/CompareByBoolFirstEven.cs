using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoLINQ
{
    public class CompareByBoolFirstEven : IComparer<bool>
    {
        public int Compare(bool x, bool y)
        {
            if (x == true && y == false)
            {
                return -1;
            }
            if (x == false && y == true)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
