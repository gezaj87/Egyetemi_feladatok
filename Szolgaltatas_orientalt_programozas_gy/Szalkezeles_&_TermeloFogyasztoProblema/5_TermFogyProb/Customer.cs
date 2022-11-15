using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _5_TermFogyProb
{
    internal class Customer
    {
        

        public void Consume()
        {
            Supervisor.AddCustomer();

            while (true)
            {
                try
                {
                    Supervisor.TakeFromStore();
                    Thread.Sleep(20);
                }
                catch (Exception e)
                {
                    break;
                }
            }
        }
    }
}
