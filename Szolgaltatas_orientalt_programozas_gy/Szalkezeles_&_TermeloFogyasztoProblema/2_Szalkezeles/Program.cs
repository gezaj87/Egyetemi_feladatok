using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//2) Írjunk egy programot, amely N<5 labdát (*) tesz ki a képernyőre, majd ezeket elindítja átlós (valamelyik) irányba, és a képernyő szélére érve ezek visszapattannak.
//A konzol 80*25-ös. Az osztály neve labda legyen, a mozgatást a mozog metódus végezze!

namespace _2_Szalkezeles
{
    internal class Program
    {
        public const char WALL = '#';
        public static int width = 80;
        public static int height = 25;
        

        public static void GameFieldDrower()
        {
            
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (y == 0 || y == height -1 )
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(WALL);
                    }
                    else
                    {
                        Console.SetCursorPosition(0, y);
                        Console.Write(WALL);
                        Console.SetCursorPosition(width- 1, y);
                        Console.Write(WALL);
                    }


                }
            }

        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(width+1, height+1);
            GameFieldDrower();

            Labda l1 = new Labda();
            Labda l2 = new Labda();
            Labda l3 = new Labda();

            Thread t1 = new Thread(l1.Mozog);
            Thread t2 = new Thread(l2.Mozog);
            Thread t3 = new Thread(l3.Mozog);

            t1.Start(); t2.Start(); t3.Start();

            while (true)
            {
                if (l1.X == l2.X && l1.Y == l2.Y || l1.X == l3.X && l1.Y == l3.Y)
                {
                    t1.Abort(); t2.Abort(); t3.Abort();
                    break;
                }
            }


            Console.ReadKey();
        }
    }
}
