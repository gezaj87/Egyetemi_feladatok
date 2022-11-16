using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XU3R7F
{
    internal class SzenzorInaktivException : Exception
    {
        public SzenzorInaktivException(string message="A szenzor nem aktív!") : base(message)
        {

        }
    }
}
