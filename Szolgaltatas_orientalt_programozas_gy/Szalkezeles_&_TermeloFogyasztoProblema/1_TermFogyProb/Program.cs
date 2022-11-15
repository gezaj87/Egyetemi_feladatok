using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//1.Írjunk egy programot, amely egy szál segítségével egy max. 40 elemű listába tesz elemeket (400 darabot) , egy másik
//szál pedig ebből a listából vesz ki elemeket. 
//A termelő szál írja ki, ha berak a listába (királykék színnel), a fogyasztó jelezze piros színnel, amikor kivesz a listából egy elemetl
//A főprogram indítsa el a szálakat.

//A termelo osztály Termel metódusa tegye be a listában az elemeket (10 000 és 80 001 között számok). HA tele van a lista, figyelmeztessen és várjunk, amíg nincs tele.

//A fogyaszto osztály fogyaszt metódusa vegye ki az elemet a listából, ha abban van elem.

//A termelő írja ki, ha berakott 400 elemet és leállt. A fogyasztó írja ki, ha kivette az összes elemet, és leállít.
 
//Figyeljünk arra, hogy ha a termelő nem tud belrakni a listába, akkor várnia kell egy fogyasztóra, aki kivesz, és fordítva....


//Ha készen van a program, akkor a termelő várjon 1,3 ezredsec-et a feltöltés után, a fogyasztó pedig 3,8 ezredsec-et.

namespace _1_TermFogyProb
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Producer p1 = new Producer(400, ConsoleColor.DarkBlue);
            Customer c1 = new Customer(ConsoleColor.DarkRed);

            Thread t1 = new Thread(p1.Produce);
            Thread t2 = new Thread(c1.Consume);
            t1.Start(); t2.Start();


            Console.ReadKey();
        }
    }
}
