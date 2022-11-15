using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _1_TermFogyProb
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
                    int product = rnd.Next(rnd_min, rnd_max);

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

    }
}
