using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Feladat
{
    internal class Supervisor
    {
        private static Random rnd = new Random();
        private static List<int> list = new List<int>();
        private const int numberOfItems = 100000;

        public static int list_length()
        {
            return list.Count;
        }

        public static int smallerThan10()
        {
            int sum = 0;
            foreach (int item in list)
            {
                if (item < 10) sum++;
            }

            return sum;

        }

        public static void Method_1()
        {
            //Az első metódus 100.000 darab random számot ad a listához, ezek a számok pozítív egész számok, maximum 3 jegyűek!
            for (int i = 0; i < numberOfItems; i++)
            {
                list.Add(rnd.Next(0, 999 + 1));
            }
        }

        public static void Method_2()
        {
            //A második szál megkeresi (előről, a 0. indextől kezdve) az első kettő vagy 3 jegyű számot, és helyére -1-et ír. Ha nem talál ilyet,
            //akkor a szál azonnal álljon le!

            lock (list)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] > 9)
                    {
                        list[i] = -1;
                    }
                }
            }
        }
    }
}
