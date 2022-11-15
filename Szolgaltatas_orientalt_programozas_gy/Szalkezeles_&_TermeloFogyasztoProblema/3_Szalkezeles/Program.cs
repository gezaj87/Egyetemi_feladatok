using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//3) Egy osztályt definiáljunk, amely tartalmaz egy "nagy" méretű vektort (500.000 elemmel). Az egyszerűség kedvéért töltsük fel ezt a vektort "1" értékekkel. Így tudjuk hogy a tömbelemek összege 500.000 lesz.

//Tartalmazzon egy publikus osszeg mezőt is, mely induláskor legyen 0. Készítsünk két void-os visszatérésű, paraméter nélküli függvényt alsofele() és felsofele() névvel.Az első a vektor alsó felét, 0..249999 közötti elemek összegét számolja ki oly módon, hogy minden vektorelemet hozzáad az osszeg mezőhöz. Hasonlóan, a felső felét összegző függvény a 2500000 és 4999999 sorszámú elemeket összegzi.

//Készítsünk programot, amely a két függvényt egymás után meghívja, és kiírja a vektor elemeinek összegét (nem meglepő módon ez 500.000 lesz). 

namespace _3_Szalkezeles
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread t1 = new Thread(Vector.Alsofele);
            Thread t2 = new Thread(Vector.Felsofele);

            t1.Start(); t2.Start();

            t1.Join(); t2.Join();
            Console.WriteLine(Vector.sum);


            Console.ReadKey();

        }
    }
}
