using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//8.Új feladat:
//Deklaráljunk egy 80x25 - ös mátrixot! Az egyik szál generáljon egy véletlen sor és oszlopindexet (0..79, 0..24), a mátrix 
//így meghatározott helyére tegyen egy X-et! Összesen 2000 darab X-et rakjon be a mátrixba, ennyiszer próbálja meg!
//Egy másik szál ugyanezt csinálja, de ez O-t (nem nulla, O betű) tegyen a mátrixba, szintén 2000 darabot tegyen bele.

//Ha ott korábban már X volt, akkor azt felülírhatja. Az X-et generáló szál a legmagasabb prioritással fusson,
//a másik a legalacsonyabbal! A szálak jelenítsék meg, amit csinálnak, azaz a képernyő mátrixnak megfelelő pozíciójára is
//tegyék ki az X-t (pirossal) és a O-t (zölddel)!

//Kérdés: mi a fő különbség a két megvalósítás között? (A képernyőt kezelést nem számítva?)

namespace _8_Szalkezeles
{
    internal class Program
    {
        private const int DIMENSION_A = 80;
        private const int DIMENSION_B = 25;
        private const int ITERATIONS = 2000;

        private static Random rnd = new Random();
        private static string[,] matrix = new string[DIMENSION_A, DIMENSION_B];

        private static void Method_X()
        {
            for (int i = 0; i < ITERATIONS; i++)
            {
                int a = rnd.Next(0, DIMENSION_A);
                int b = rnd.Next(0, DIMENSION_B);

                Monitor.Enter(matrix);
                try
                {
                    if (string.IsNullOrEmpty(matrix[a, b]))
                    {
                        matrix[a, b] = "X";
                        Console.ForegroundColor= ConsoleColor.Red;
                        Console.SetCursorPosition(a, b);
                        Console.Write(matrix[a, b]);
                    }
                }
                finally
                {
                    Monitor.Exit(matrix);
                }
            }
        }

        private static void Method_O()
        {
            for (int i = 0; i < ITERATIONS; i++)
            {
                int a = rnd.Next(0, DIMENSION_A);
                int b = rnd.Next(0, DIMENSION_B);

                Monitor.Enter(matrix);
                try
                {
                    matrix[a, b] = "O";
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(a, b);
                    Console.Write(matrix[a, b]);
                }
                finally { Monitor.Exit(matrix); }

            }
        }


        static void Main(string[] args)
        {
            Thread t1 = new Thread(Method_X);
            Thread t2 = new Thread(Method_O);
            t1.Priority = ThreadPriority.Highest;
            t2.Priority = ThreadPriority.Lowest;

            t1.Start(); t2.Start();

            Console.ReadKey();
        }
    }
}
