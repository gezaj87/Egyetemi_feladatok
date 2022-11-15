using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// 7.Új feladat:
// Deklaráljunk egy 80x25 - ös mátrixot! Az egyik szál generáljon egy véletlen sor és oszlopindexet (0..79, 0..24), a mátrix 
// így meghatározott helyére tegyen egy X-et! Összesen 2000 darab X-et rakjon be a mátrixba, ha ott még nincs O. (Pontosabban
// ennyiszer generáljon sor, oszlop indexeket! Ha X van ott, akkor mindegy, hogy belerakja vagy nem! :-)
// Egy másik szál ugyanezt csinálja, de ez O-t (nem nulla, O betű) tegyen a mátrixba, szintén 2000 darabot tegyen bele (ennyiszer 
// próbálja meg)!
// Ha ott korábban már X volt, akkor azt ne írja felül! Az X-et generáló szál a legmagasabb prioritással fusson,
// a másik a legalacsonyabbal! A végén a főprogram írja ki, hogy hány X és hány O van a mátrixban! Azonos prioritás esetén változik
// az arány?

namespace _7_Szalkezeles
{
    internal class Program
    {
        private static Random rnd = new Random();
        private const char X = 'X';
        private const char O = 'O';
        private const int TOTAL_ITERATION = 2000;
        private const int DIMENSION_X = 80;
        private const int DIMENSION_Y = 25;

        private static string[,] matrix = new string[DIMENSION_X, DIMENSION_Y];
        private static int totalX = 0;
        private static int totalO = 0;

        public static void FillMatrix(char c)
        {
            int temp_total = 0;

            Console.WriteLine(c);

            for (int i = 0; i < TOTAL_ITERATION; i++)
            {
                int x = rnd.Next(0, DIMENSION_X);
                int y = rnd.Next(0, DIMENSION_Y);

                lock (matrix)
                {
                    if (string.IsNullOrEmpty(matrix[x, y]))
                    {
                        matrix[x, y] = c.ToString();
                        temp_total++;
                    }
                }

                
            }

            if (c == 'X') totalX = temp_total;
            if (c == 'O') totalO = temp_total;
            
        }

        static void Main(string[] args)
        {
            Thread t1 = new Thread(() => FillMatrix(X));
            Thread t2 = new Thread(() => FillMatrix(O));
            t1.Priority = ThreadPriority.Highest; t2.Priority= ThreadPriority.Lowest;
            t1.Start(); t2.Start();

            t1.Join(); t2.Join();
            Console.WriteLine($"Total X: {totalX}");
            Console.WriteLine($"Total O: {totalO}");

            
            

            Console.ReadKey();
        }
    }
}
