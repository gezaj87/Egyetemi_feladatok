using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XU3R7F
{
    internal class AlacsonyAlsoHatarException : Exception
    {
        public AlacsonyAlsoHatarException(int beirtErtek, string message= "Az alsó határ nem lehet −60-nál kisebb!") : base(message)
        {
            BeirtErtek = beirtErtek;
        }

        public int BeirtErtek { get; set; }
    }
}
