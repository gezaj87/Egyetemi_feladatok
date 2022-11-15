using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _3_TermFogyProb
{
    internal class Producer
    {
        private int numberMin;
        private int numberMax;

        public Producer(int min, int max)
        {
            this.numberMin = min;
            this.numberMax = max;
        }


        public void Produce()
        {
            Supervisor.AddProducer();

            for (int i = numberMin; i <= numberMax; i++)
            {
                try
                {
                    if (!IsPrime(i)) continue;

                    int product = i;

                    Supervisor.AddToStore(product);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    break;
                }
            }

            Console.WriteLine($"Producer has finished producing.");
            Supervisor.RemoveProducer();

        }


        public static bool IsPrime(int number)
        {
            if (number == 1)
            {
                return false;
            }
            if (number == 2)
            {
                return true;
            }
            if (number % 2 == 0)
            {
                return false;
            }
            int boundary = (int)Math.Floor(Math.Sqrt(number));
            for (int i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
