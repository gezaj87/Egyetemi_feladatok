using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//A feladat: fejlesszünk olyan alkalmazást, amely 4 szálon keres prímszámokat; minden szál más-más
//számintervallumon dolgozzon! Az előállított prímszámokat egy 50 elemet tárolni képes gyűjtő (külön osztály)
//kezelje! Indítsunk 2 feldolgozó szálat, melyek a prímszámokat a képernyőre írják, az egyik feldolgozó
//szál kékkel, a másik sárgával írjon! A végén jelenítsük meg, hogy melyik feldolgozó szál hány prímet
//írt ki összesen a képernyőre!

//A Gyűjtő osztály a listán kívül tartalmazza a leállást, indítást, és figyelje a termelők
//és a fogyasztók számát, valamint a berak és kivesz metódusokat is tartalmazza.

//A fogyasztó osztály elkezd metódusa hívja meg a gyujto.kivesz metódusát.
//A termelő osztály elkezd metódusa hívja meg a gyujto.berak metódust.

//A két osztály elkezd metódusaira mutassanak a szálmutatók az indításkor.


namespace _3_TermFogyProb
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Producer p1 = new Producer(0, 100);
            Producer p2 = new Producer(101, 200);
            Producer p3 = new Producer(201, 300);
            Producer p4 = new Producer(301, 400);

            Customer c1 = new Customer(ConsoleColor.Blue);
            Customer c2 = new Customer(ConsoleColor.Yellow);

            Thread t1 = new Thread(p1.Produce);
            Thread t2 = new Thread(p2.Produce);
            Thread t3 = new Thread(p3.Produce);
            Thread t4 = new Thread(p4.Produce);

            Thread t5 = new Thread(c1.Consume);
            Thread t6 = new Thread(c2.Consume);

            t1.Start(); t2.Start(); t3.Start(); t4.Start(); t5.Start(); t6.Start();

            Console.ReadKey();
        }
    }
}
