using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matek_Imp
{
    public class Matek : MarshalByRefObject, Matek_Service.IMatek
    {
        public int Add(int A, int B)
        {
            return A + B;
        }

        public int Sub(int A, int B)
        {
            return A - B;
        }
    }
}
