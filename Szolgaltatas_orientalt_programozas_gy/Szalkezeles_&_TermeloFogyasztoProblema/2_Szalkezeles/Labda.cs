using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _2_Szalkezeles
{
    internal class Labda
    {
        private static Random rnd = new Random();
        private const char BALL = '*';

        private int x;
        private int y;
        private EnumDirection direction;

        public int X { get { return x; } }
        public int Y { get { return y; } }


        public Labda()
        {
            this.x = rnd.Next(1, Program.width - 1);
            this.y = rnd.Next(1, Program.height- 1);
            this.direction = (EnumDirection)rnd.Next(0, 3 + 1);
        }

        public void Mozog()
        {
            while (true)
            {
                if (direction == EnumDirection.topLeft)
                {
                    if (x == 1 || y == 1) { direction = EnumDirection.bottomRight; continue; }
                    Print(x+1, y+1);
                    x--; y--;
                }

                if (direction == EnumDirection.bottomRight)
                {
                    if (x == Program.width - 2 || y == Program.height - 2) { direction = EnumDirection.topLeft; continue; }
                    Print(x-1, y-1);
                    x++; y++;
                }

                if (direction == EnumDirection.topRight)
                {
                    if (x == Program.width - 2 || y == 1) { direction = EnumDirection.bottomLeft; continue; }
                    Print(x-1, y+1);
                    x++; y--;
                }

                if (direction == EnumDirection.bottomLeft)
                {
                    if (x == 1 || y == Program.height - 2) { direction = EnumDirection.topRight; continue; }
                    Print(x+1, y-1);
                    x--; y++;
                }

            }
        }

        private void Print(int prevX, int prevY)
        {
            lock (typeof(Program))
            {
                Console.SetCursorPosition(x, y);
                Console.Write(BALL);

                Console.SetCursorPosition(prevX, prevY);
                Console.Write(' ');
                Thread.Sleep(50);
            }
        }


    }
}
