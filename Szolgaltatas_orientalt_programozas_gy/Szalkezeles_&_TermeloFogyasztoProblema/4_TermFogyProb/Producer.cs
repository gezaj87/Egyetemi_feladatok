using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _4_TermFogyProb
{
    internal class Producer
    {
        private static Random rnd = new Random();
        private static int firstID = 0;
        private const int MIN = 10000;
        private const int MAX = 90000;

        private int divisor;
        private int N;
        private int id;

        public Producer(int divisor, int N)
        {
            this.divisor = divisor;
            this.N = N;
            this.id = ++firstID;
        }


        public void Produce()
        {
            Supervisor.AddProducer();

            for (int i = 0; i < N; i++)
            {
                try
                {
                    int product = rnd.Next(MIN, MAX);
                    if (!IsDivisible(divisor, product)) continue;

                    Supervisor.AddToStore(product);
                    Console.WriteLine($"{id}.Producer has added: {product}");
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    break;
                }
            }

            Console.WriteLine($"{id}.Producer has finished producing.");
            Supervisor.RemoveProducer();

        }


        public static bool IsDivisible(int divisor, int number)
        {
            if (number % divisor == 0) return true;
            else return false;
        }




    }
}
