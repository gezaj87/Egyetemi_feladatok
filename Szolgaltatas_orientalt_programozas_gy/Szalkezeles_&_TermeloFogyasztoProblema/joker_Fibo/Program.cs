using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//SZÁLKEZELÉS JOKER
//Írjunk programot, amely a konzolra megjeleníti a Fibonacci számokat, amíg nem nyomunk egy gombot!

namespace joker_Fibo
{
    internal class Program
    {
        //írj egy metódust ami sorban kiírja a Console-ra a fibonacci számokat egy végtelen ciklusban!
        private static void Fib()
        {
            int a = 0;
            int b = 1;
            int c = 0;
            while (true)
            {
                Console.WriteLine(a);
                c = a + b;
                a = b;
                b = c;

                Thread.Sleep(100);
            }

        }


        static void Main(string[] args)
        {
            Thread t1 = new Thread(Fib);
            t1.Start();

            while (true)
            {
                Console.ReadLine();
                t1.Abort();
                break;
            }

            Console.ReadLine();
        }
    }
}
