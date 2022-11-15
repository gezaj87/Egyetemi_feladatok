using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//1) Készítsünk olyan socket stream alapú klienst, amely

// felcsatlakozik a szerverre (adott ip címen és porton) 
// elolvassa és kijelzi a szervertől kapott üdvözlőszöveget
// beolvas a billentyűzetről egy utasítást (string) és átküldi a szervernek
// beolvassa a szervertől érkező választ, amely:
//   lehet OK, melyet a kliens kijelez
//   lehet ERR|<hibauzenet> melyet a kliens kijelez (pirossal)
//   lehet OK* mely azt jelzi hogy további sorok érkeznek a szervertől, melyet a kliens mind beolvas és kijelez, amíg az OK! nem érkezik, mely a sorok végét    jelzi (nincs több adat)
//   lehet BYE ami azt jelzi, hogy a szerver befejezi a kommunikációt, így a kliens is kiléphet
//egyéb válasz (amit a szervertől kap) ismeretlen, pirossal jelzi ki.

//Írjuk meg hozzá a szervert, amely
//- kiírja a konzolra az IP-címeit,
//- nyit egy megfelelő portot (lásd kliens)
//-kiírja, hogy melyik IP-címen és porton fog kommunikálni a kliens-sel.
//- ha csatlakozik hozzá egy kliens, kiír egy üdvözlő üzenetet számára
//- az OSZTAS|szam1|szam2 hatására visszaítja szam1/szam2-t. Ha szam2 nem nulla,
//akkor jelzi, h több soros válasz lesz, majd visszaírja az eredményt és az OK! szöveget is utána, egyébként a 
//"Nullával nem tudok osztani!" üzenetet.
//- a BYE üzenetre elköszön és bontja a kapcsolatot.
//- minden más üzenetre értetlenkedő üzenettel válaszol vissza.

namespace _1_Stream_Server
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
