using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _2_TermFogyProb
{
    internal class Producer
    {
        //product
        private static Random rnd = new Random();
        private static int rnd_min = 10000;
        private static int rnd_max = 80001;

        private static int startID = 0;

        private int id;
        private int items;
        private ConsoleColor color;

        public Producer(int items, ConsoleColor color)
        {
            lock (typeof(Producer))
            {
                this.id = ++startID;
            }
            this.items = items;
            this.color = color;
        }


        public void Produce()
        {
            Supervisor.AddProducer();

            for (int i = 0; i < items; i++)
            {
                try
                {
                    int product = GetRandomPrime();

                    Supervisor.AddToStore(product);

                    lock (typeof(Program)) //színezés miatt lock-olom.
                    {
                        Console.ForegroundColor = color;
                        Console.WriteLine($"{id}.Producer has added to the Store: {product}");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    Thread.Sleep(130);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    break;
                }
            }

            Console.WriteLine($"{id}.Producer have produced {items} items and stopped working.");
            Supervisor.RemoveProducer();

            
           

        }

        
        public static int GetRandomPrime()
        {
            int prime = 0;
            while (true)
            {
                prime = rnd.Next(rnd_min, rnd_max);
                if (IsPrime(prime))
                {
                    return prime;
                }
            }
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
