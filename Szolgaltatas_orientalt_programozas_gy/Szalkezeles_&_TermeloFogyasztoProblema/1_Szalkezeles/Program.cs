using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// Készítsünk olyan programot, amely a magyar zászlót rajzolja ki a karakteres képernyőre az alábbi módon: készítsünk először egy külön osztályt az alábbi adatokkal:

// y1,y2 egész szám adatok,
// szín
// kirajzolandó csillagok darabszáma
// egy kirajzol() paraméter nélküli void-os függvény, mely a képernyőn az adott y1..y2 koordináták közé eső sávban adott színnel random koordinátákra csillag karaktert rajzol.

// Példányosítsuk meg az osztályt 3 példányra, az első a y1=0, y2=7, szín=piros beállítások mellett egy piros sávot rajzol hasonlóan a második fehér, a harmadik zöld sávot rajzol ki. A képernyő 25 soros (0..24), és 80 oszlopos (0..79).

// Hívjuk meg a három kirajzol() függvényt egymás után, hogy megkapjuk a három sávot. 

namespace _1_Szalkezeles
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Zaszlo z1 = new Zaszlo(0, 7, ConsoleColor.Red);
            Zaszlo z2 = new Zaszlo(8, 15, ConsoleColor.White);
            Zaszlo z3 = new Zaszlo(16, 24, ConsoleColor.Green);

            Thread t1 = new Thread(z1.Kirajzol);
            Thread t2 = new Thread(z2.Kirajzol);
            Thread t3 = new Thread(z3.Kirajzol);

            t1.Start(); t2.Start(); t3.Start();


            Console.ReadKey();
        }
    }
}
