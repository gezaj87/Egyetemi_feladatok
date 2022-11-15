using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// Ugyanaz mint az 1-es, csak ....
// "A fenti programot felhasználva írjunk programot, amely prímszámokat rak be a listába, a termelő pedig ezeket veszi ki."

namespace _2_TermFogyProb
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Producer p1 = new Producer(400, ConsoleColor.DarkBlue);
            Customer c1 = new Customer(ConsoleColor.DarkRed);

            Thread t1 = new Thread(p1.Produce);
            Thread t2 = new Thread(c1.Consume);
            t1.Start(); t2.Start();


            Console.ReadKey();
        }
    }
}
