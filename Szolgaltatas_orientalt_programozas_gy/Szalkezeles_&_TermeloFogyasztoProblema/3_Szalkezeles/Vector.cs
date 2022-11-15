using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _3_Szalkezeles
{
    internal class Vector
    {
        private static int[] v = new int[500000];
        public static int sum = 0;

        static Vector()
        {
            for (int i = 0; i < v.Length; i++)
            {
                v[i] = 1;
            }
        }

        public static void Alsofele()
        {
            int tempSum = 0;

            for (int i = 0; i < v.Length / 2; i++)
            {
                tempSum += v[i];
            }

            lock (typeof(Vector))
            {
                sum += tempSum;
            }
        }

        public static void Felsofele()
        {
            int tempSum = 0;

            for (int i = v.Length / 2; i < v.Length; i++)
            {
                tempSum += v[i];
            }

            Monitor.Enter(typeof(Vector));
            try
            {
                sum += tempSum;
            }
            finally
            { Monitor.Exit(typeof(Vector)); }
        }


    }
}
