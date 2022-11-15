using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//1.Készítsen kliens - szervert alkalmazást, amely egy banki utalási rendszert szimulál. A szerver az alábbi funkciókat valósítja meg:
//-login | nev | jelszó: a felhasználó loginol. Az egyszerűség kedvéért minden logint elfogadunk, az adott user account automatikusan létrejön!

//Az alábbi funkciók csak login után vehetők igénybe:
//-logout: értelemszerű, a kliens elköszön, a szerve egy OK-ot küldd vissza, ha volt login, egyébként ERR|Nem is volt login.A kommunikáció végét jelentse ekkor is!
//- feltoltt|pénzösszeg: a user feltölti a számlaegyenlegét, az adott mennyiségű pénz hozzáadódik a jelenlegi pénzmennyiségéhez.
//- lekerdez: megadja az aktuális egyenleget,
//- utal|celuser|osszeg: átutal adott összegű pénzt az egyenlegről az adott user-nek. A céluser accountnak léteznie kell, és a számlán megfelelő 
//   mennyiségű pénznek léteznie kell.
//- utalasok: string lista, megadja hogy kinek és mennyi pénzt utaltunk át korábban, és mikor történt az utalás (időpont) (Több soros lista...)

//Készítsen szervert, mely tetszőlegesen sok konkurrens logint kezel, azat kiszolgál több klienst is! 
//Készítsen egyszerű klienst, ahol egysoros parancsokat tudunk beküldeni, és a szerver a választ kiértékelni!
//Az IP cím és a PORT az app.config-ban legyen! (Onnan olvassa ki a szerver!)



namespace _1_Feladat_Server
{
    internal class Program
    {
        private static TcpListener listener = null;
        private static Thread connections = null;
        public static List<Thread> threadList = new List<Thread>();
        public static List<ClientCom> clientList = new List<ClientCom>();

        private static void AcceptConnection()
        {
            while (true)
            {
                TcpClient bejovo = listener.AcceptTcpClient();
                ClientCom k = new ClientCom(bejovo);
                Thread t = new Thread(k.StartCom);

                lock (threadList)
                {
                    threadList.Add(t);
                }

                lock (clientList)
                {
                    clientList.Add(k);
                }

                t.Start();
            }
        }

        static void Main(string[] args)
        {
            string ipAddress = ConfigurationManager.AppSettings["IP"];
            string portNumber = ConfigurationManager.AppSettings["PORT"];
            IPAddress ip = IPAddress.Parse(ipAddress);
            int port = int.Parse(portNumber);

            listener = new TcpListener(ip, port);
            listener.Start();
            connections = new Thread(AcceptConnection);
            connections.Start();
            Console.WriteLine($"The server is running on: {ip}:{port}");

            //press button to escape
            Console.ReadLine();
            listener.Stop();
            connections.Abort();


            Console.ReadKey();
        }
    }
}
