using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _4_TermFogyProb
{
    internal class Customer
    {
        private static int startID = 0;

        private ConsoleColor color;
        private int id;

        public Customer(ConsoleColor color)
        {
            lock (typeof(Customer))
            {
                this.id = ++startID;
            }

            this.color = color;
        }

        public void Consume()
        {
            Supervisor.AddCustomer();

            while (true)
            {
                try
                {
                    int product = Supervisor.TakeFromStore();

                    lock (typeof(Program))
                    {
                        Console.ForegroundColor = color;
                        Console.WriteLine($"{id}.Customer taked from Store: {product}");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{id}.{e.Message}.");
                    break;
                }
            }
        }
    }
}
