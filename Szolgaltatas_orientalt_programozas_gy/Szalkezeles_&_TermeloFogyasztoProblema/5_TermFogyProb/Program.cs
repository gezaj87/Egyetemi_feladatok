using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//5.Új feladat:
//Termelő fogyasztó probléma
//A puffer most 80x25-ös mátrix! Van 3 termelő, mindhárom 2000 darab X-et rak bele a mátrixba. Az, hogy a mátrix 
//melyik sorába és oszlopába teszi, az legyen véletlenszerű. Ha már van ott X, akkor generáljon egy másik pozíciót!
//Ezt mindaddig tegye a termelő, amíg sikerül üres helyre rakni az X-et! Ha tele van a verem, azaz mindenhol van,
//akkor várjon addig, amíg van hely! Ha sikerült a termelőnek X-et raknia a mátrixba, akkor az jelenjen meg az azonos
//méretű képernyőn! 
//Van 2 fogyasztó. Mindkettő keressen a mátrixban egy X-t, ha megtalálta, akkor vegye  ki onnan! A képernyőn törlődjön az
//X. A fogyasztók minden X-et töröljenek a képernyőről!


namespace _5_TermFogyProb
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Producer p1 = new Producer();
            Producer p2 = new Producer();
            Producer p3 = new Producer();

            Customer c1 = new Customer();
            Customer c2 = new Customer();

            Thread t1 = new Thread(p1.Produce); t1.Start();
            Thread t2 = new Thread(p2.Produce); t2.Start();
            Thread t3 = new Thread(p3.Produce); t3.Start();

            Thread t4 = new Thread(c1.Consume); t4.Start();
            Thread t5 = new Thread(c2.Consume); t5.Start();

            Customer c3 = new Customer();
            Customer c4 = new Customer();
            Thread t6 = new Thread(c3.Consume); t6.Start();
            Thread t7 = new Thread(c4.Consume); t7.Start();


            Console.ReadKey();
        }
    }
}
