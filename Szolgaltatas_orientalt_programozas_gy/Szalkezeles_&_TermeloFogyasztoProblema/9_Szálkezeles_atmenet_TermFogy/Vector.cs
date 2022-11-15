using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9_Szálkezeles_atmenet_TermFogy
{
    internal class Vector
    {
        private const int SIZE = 100000;

        private static Random rnd = new Random();
        public static List<int> lista = new List<int>();

        public static void Add()
        {
            for (int i = 0; i < SIZE; i++)
            {
                lock(lista)
                {
                    lista.Add(rnd.Next(0, SIZE));
                }
            }

        }

        public static void Delete()
        {
            for (int i = 0; i < SIZE; i++)
            {
                int index = rnd.Next(0, SIZE);

                lock (lista)
                {
                    if (lista.Count > index)
                    {
                        try
                        {
                            lista.RemoveAt(index);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine($"count: {lista.Count}, index: {index}, az elem: {lista[index]}");
                        }
                    }
                }
            }
        }
    }
}
