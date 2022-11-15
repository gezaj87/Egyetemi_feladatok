using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// Átmenet a termelő-fogyasztó problémára

// 9. Készítsünk olyan programot, amely egy külön osztályba egy egész szám listát tartalmaz. Két metódust írjunk az osztályba! Az első 100.000 darab random számot ad a listához, a második 100.000 esetben random kiválaszt egy listaelemet, és törli a listáról (amennyiben a listában ott van elem). Ha a két metódust  egymás után lefuttatjuk, a lista természetesen újra üres lesz. A főprogram futtassa le a két függvényt egymás után, és írja ki a (megmaradt) elemeket a listáról. (Nyilván kell egy static változó, amely mutatja, h hány elem van eddig a listában.)
//Majd indítsuk el a két metódust két szálon. Berakni bármikor lehet, kivenni csak akkor, ha van mit. 



namespace _9_Szálkezeles_atmenet_TermFogy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread t1 = new Thread(Vector.Add);
            Thread t2 = new Thread(Vector.Delete);
            t1.Start(); t2.Start();

            t1.Join(); t2.Join();
            Console.WriteLine(Vector.lista.Count());
            

            Console.ReadKey();
        }
    }
}
