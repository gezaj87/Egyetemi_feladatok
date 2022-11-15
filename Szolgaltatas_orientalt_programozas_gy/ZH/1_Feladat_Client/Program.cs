using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _1_Feladat_Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpClient connection = null;
            StreamReader reader = null;
            StreamWriter writer = null;

            try
            {
                string ipAddress = ConfigurationManager.AppSettings["IP"];
                string portNumber = ConfigurationManager.AppSettings["PORT"];
                IPAddress ip = IPAddress.Parse(ipAddress);
                int port = int.Parse(portNumber);

                connection = new TcpClient(ipAddress, port);
                reader = new StreamReader(connection.GetStream(), Encoding.UTF8);
                writer = new StreamWriter(connection.GetStream(), Encoding.UTF8);
                Console.WriteLine($"connection established: {ipAddress}:{port}");
            }
            catch
            {
                connection = null;
            }

            string welcomeMessage = reader.ReadLine();
            Console.WriteLine(welcomeMessage);

            bool finished = false;

            while (!finished)
            {
                string command = Console.ReadLine();
                writer.WriteLine(command);
                writer.Flush();

                string response = reader.ReadLine();
                string[] stringParts = response.Split('|');

                if (stringParts[0] == "ERR") Console.WriteLine($"ERR|{stringParts[1]}");

                else if (response == "OK*" || response == "OK!")
                {
                    while (response != "OK!")
                    {
                        response = reader.ReadLine();
                        if (response != "OK!") Console.WriteLine(response);
                    }
                }

                else
                {
                    Console.WriteLine(response);
                }

                if (response == "OK") finished = true;
            }
        }
    }
}
