using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _3_Stream_Server
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
