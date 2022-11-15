using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//0.Készítsen egy olyan programot, amelyben 2 metódus külön szálon futva egy listához ad elemeket, illetve vesz ki belőle.
//Az első metódus 100.000 darab random számot ad a listához, ezek a számok pozítív egész számok, maximum 3 jegyűek!
//A második szál megkeresi (előről, a 0. indextől kezdve) az első kettő vagy 3 jegyű számot, és helyére -1-et ír. Ha nem talál ilyet,
//akkor a szál azonnal álljon le!
//A főprogram futtassa le a két metódust "párhuzamosan" és írja ki, hogy hány szám maradt a listában, és ezek közül hány darab az egyjegyű szám!
//Az első metódus fusson a legmagasabb prioritással, a második a legalacsonyabbal! (Ez nem termelő-fogyasztó probléma!!!) 

namespace _0_Feladat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread t1 = new Thread(Supervisor.Method_1);
            Thread t2 = new Thread(Supervisor.Method_2);

            t1.Priority = ThreadPriority.Highest;
            t2.Priority = ThreadPriority.Lowest;
            t1.Start(); t2.Start();

            t1.Join(); t2.Join();

            Console.WriteLine($"Összesen {Supervisor.list_length()} szám maradt a listában! Ebből {Supervisor.smallerThan10()} darab az egyjegyű szám!");


            Console.ReadLine();
        }
    }
}
