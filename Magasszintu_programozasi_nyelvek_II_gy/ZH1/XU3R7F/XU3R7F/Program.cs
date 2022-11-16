using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace XU3R7F
{
    class Program
    {
        static void Main(string[] args)
        {
            Kereskedes KER = new Kereskedes();

            StreamReader file = new StreamReader("gepkocsik.csv", Encoding.Default);

            while (!file.EndOfStream)
            {
                string[] sor = file.ReadLine().Split(';');

                if (sor[0] == "SZ")
                {   //string rendszam, int evjarat, int eredetiAr, Allapot allapot, int szallithatoSzemelyekSzama, bool horog, Klima klima
                    Szemelygepkocsi szemkocsi = new Szemelygepkocsi(sor[1], int.Parse(sor[2]), int.Parse(sor[3]), (Allapot)Enum.Parse(typeof(Allapot), sor[4]), int.Parse(sor[5]), sor[6] == "van_vonohorog" ? true : false, (Klima)Enum.Parse(typeof(Klima), sor[7]));
                    KER.AddGepkocsi(szemkocsi);
                }
                else
                {
                    Gepkocsi gepkocsi = new Gepkocsi(sor[1], int.Parse(sor[2]), int.Parse(sor[3]));
                    KER.AddGepkocsi(gepkocsi);
                }
            }

            //Jelenítse meg az összes a konténerosztályban implementált property és metódus eredményét a kijelzőn!

            foreach (Gepkocsi kocsi in KER.Gepkocsik)
            {
                Console.WriteLine(kocsi);
            }


            Console.ReadLine();
        }
    }
}
