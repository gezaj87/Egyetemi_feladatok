using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _5_TermFogyProb
{
    internal class Producer
    {
        private const char X = 'X';

        
        private static int firstID = 0;

        private int id;
        private int numberOfItemsToProduce;

        public Producer()
        {
            lock (typeof(Producer))
            {
                this.id = ++firstID;
                this.numberOfItemsToProduce = 2000;
            }
        }


        public void Produce()
        {
            Supervisor.AddProducer();

            while (numberOfItemsToProduce != 0)
            {
                try
                {
                    char product = X;

                    if (Supervisor.AddToStore(product)) numberOfItemsToProduce--;
                    //Console.WriteLine($"{id}.Producer has added: {product}");

                    Thread.Sleep(5);
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e.Message);
                    break;
                }
            }

            Supervisor.RemoveProducer();

        }

    }
}
