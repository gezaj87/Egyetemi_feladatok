using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//Egy matematika szerveren négy funkciót kell implementálni:

//    ADD |< a >|< b >: két szám összegét számolja ki,
//    PRIMEK|< N >: prímszámot állít elő az adott N értékig.
//    FIBO|<N> :az N.Fibonacci számot adja vissza
//    BYE: kilépés

//Készítsünk hozzá klienst, és szervert!
//A kliens írja ki, ha sikerült létrehozni a kapcsolatot, majd olvasson be konzolról utasításokat, küldje
//el a szervernek, a válaszokat pedig szintén írja ki a konzolra.
//A szerver válaszoljon a kliens kérdéseire, elköszönés esetén bontsa a kapcsolatot.

namespace _2_Stream_Server
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
