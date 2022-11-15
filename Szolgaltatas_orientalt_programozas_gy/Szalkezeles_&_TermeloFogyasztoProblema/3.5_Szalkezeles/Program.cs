using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//3,5)
//Írjunk egy programot, amely két metódust felhasználva megszámolja, hogy melyik egy 10000 elemű listában a legnagyobb és
//a legkisebb elem, és ezek hányszor fordulnak elő!


namespace _3._5_Szalkezeles
{
    internal class Program
    {
        const int N = 10000;

        private static Random rnd = new Random();
        private static int[] v = new int[N];

        private static void Biggest()
        {
            int items = 0;
            int biggest = v.Last();

            for (int i = 0; i < v.Length; i++)
            {
                if (v[i] > biggest)
                {
                    biggest = v[i];
                    items = 1;
                }

                if (v[i] == biggest) items++;
            }

            Console.WriteLine($"Biggest number in vector: {biggest}, number of occurrences: {items}");
        }

        private static void Smallest()
        {
            //...
        }

        static void Main(string[] args)
        {
            for (int i = 0; i < v.Length; i++)
            {
                v[i] = rnd.Next(0, N);
            }

            Biggest();


            // thread stb...


            Console.ReadLine();

        }
    }
}
