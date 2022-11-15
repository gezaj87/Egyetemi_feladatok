using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//Készítsünk többszálú, termelő-fogyasztó modellű megoldást az alábbiakra:

//-a puffer mérete maximum 100 elem,
//-    három termelő kell
//-    a termelők adott F számmal osztható random számokat állítanak elő, adott N mennyiségben, a 10000..90000 intervallumból
//     Ha berakták a számot, írják ki a számot, és azt, hogy berakták. 
//-    az első termelő 200 db 3-al osztható számot,
//-    a második termelő 100 db, 5-el oszthatót,
//-    a harmadik termelő 200 db, 7-el oszthatót,
//-    három fogyasztó kell
//-    az egyes fogyasztók az előállított random számokat kiírják a képernyőre, minden fogyasztó más-más színt használ (vörös, fehér, zöld).
//-a fogyasztók álljanak le, ha már a termelők által előállított összes számot kiírták.
//-    Nem lehet berakni a pufferbe, ha legalább 100 elem van benne.
//-    0 elemnél nem lehet kivenni....
//-  A console ablak fejléce mutassa, hogy hány elem van a pufferben. :-)

namespace _4_TermFogyProb
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // a konzol ablak fejléce a következő legyen: "hello"
            Console.Title = "hello";

            Producer p1 = new Producer(3, 200);
            Producer p2 = new Producer(5, 100);
            Producer p3 = new Producer(7, 200);

            Customer c1 = new Customer(ConsoleColor.DarkBlue);
            Customer c2 = new Customer(ConsoleColor.DarkGreen);
            Customer c3 = new Customer(ConsoleColor.DarkYellow);

            Thread t1 = new Thread(p1.Produce);
            Thread t2 = new Thread(p2.Produce);
            Thread t3 = new Thread(p3.Produce);
            Thread t4 = new Thread(c1.Consume);
            Thread t5 = new Thread(c2.Consume);
            Thread t6 = new Thread(c3.Consume);

            t1.Start(); t2.Start(); t3.Start(); t4.Start(); t5.Start(); t6.Start();

            while (true)
            {
                Console.Title = Supervisor.StoreLength.ToString();
                if (!t4.IsAlive && !t5.IsAlive && !t6.IsAlive) break;
            }

            Console.ReadKey();
        }
    }
}
