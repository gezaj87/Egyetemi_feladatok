using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _1_Stream_Server
{
    internal class ClientCom
    {
        private StreamWriter writer;
        private StreamReader reader;

        public ClientCom(TcpClient tcpClient)
        {
            this.writer = new StreamWriter(tcpClient.GetStream(), Encoding.UTF8);
            this.reader = new StreamReader(tcpClient.GetStream(), Encoding.UTF8);
        }

        public void StartCom()
        {
            bool haveToDelete = true;

            writer.WriteLine("Welcome!");
            writer.Flush();

            bool finished = false;

            try
            {
                while (!finished)
                {
                    //nincs most timeout!

                    string line = reader.ReadLine();
                    Console.WriteLine(line);

                    string[] stringParts = line.Split('|');
                    string command = stringParts[0].ToUpper();

                    switch (command)
                    {
                        case "OSZTAS":
                            {
                                if (stringParts.Length != 3)
                                {
                                    writer.WriteLine("ERR|You have to give 2 numbers!");
                                    writer.Flush();
                                    break;
                                }

                                int a = int.Parse(stringParts[1]);
                                int b = int.Parse(stringParts[2]);

                                if (a == 0 || b == 0)
                                {
                                    writer.WriteLine("ERR|You can't divide by zero!");
                                    writer.Flush();
                                    break;
                                }

                                writer.WriteLine("OK*");
                                writer.WriteLine($"{a} / {b} = ?");
                                writer.WriteLine($"Result: {(float)a / b}");
                                writer.WriteLine("OK!");
                                writer.Flush();
                                break;

                                
                            }
                        case "BYE":
                            {
                                writer.WriteLine("BYE");
                                finished = true;
                                writer.Flush();
                                break;
                            }

                        default:
                            {
                                {
                                    writer.WriteLine("ERR|Ismererlen parancs!");
                                    writer.Flush();
                                    break;
                                }
                            }
                    }
                }

            }
            catch (Exception e)
            {
                if (e is ThreadAbortException) haveToDelete = false;
            }

            if (haveToDelete)
            {
                Console.WriteLine("Connection has been deleted.");
                lock (Program.threadList)
                {

                    Thread ez = Thread.CurrentThread;
                    int index = Program.threadList.IndexOf(ez); //megkeresem a szálat a futó szálak listájában
                    if (index != -1) Program.threadList.RemoveAt(index);
                }

                lock (Program.clientList)
                {
                    int index = Program.clientList.IndexOf(this);
                    if (index != -1) Program.clientList.RemoveAt(index);
                }
            }

        }
    }
}
