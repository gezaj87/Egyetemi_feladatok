using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_Szalkezeles
{
    internal class Zaszlo
    {
        private static Random rnd = new Random();
        private static char c = '*';

        private int y1;
        private int y2;
        private ConsoleColor color;
        private static int db = 80;

        public Zaszlo(int y1, int y2, ConsoleColor color)
        {
            this.y1 = y1; this.y2 = y2; this.color = color;
        }

        public void Kirajzol()
        {
           

            for (int i = y1; i <= y2; i++)
            {
                int[] numbers = new int[db];
                int k = 0;
                while (k < db)
                {
                    int number = rnd.Next(0, db+1);
                    if (!numbers.Contains(number))
                    {
                        numbers[k] = number;
                        k++;
                        
                        

                        lock(typeof(Program))
                        {
                            Console.SetCursorPosition(number, i);
                            Console.ForegroundColor = color;
                            Console.Write(c);
                        }
                        
                        
                    }
                }

            }
        }

    }
}
