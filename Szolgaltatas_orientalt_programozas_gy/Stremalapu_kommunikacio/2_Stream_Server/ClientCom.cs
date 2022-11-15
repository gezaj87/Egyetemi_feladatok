using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _2_Stream_Server
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

        private static bool IsPrime(int number)
        {
            if (number < 2)
            {
                return false;
            }
            for (int i = 2; i < number; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
        private static int Fibonacci(int n)
        {
            if (n == 0)
            {
                return 0;
            }
            if (n == 1)
            {
                return 1;
            }
            return Fibonacci(n - 1) + Fibonacci(n - 2);
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
                        case "PRIMEK":
                            {
                                if (stringParts.Length != 2)
                                {
                                    writer.WriteLine("ERR|You have to provide 1 number!");
                                    writer.Flush();
                                    break;
                                }

                                int n = int.Parse(stringParts[1]);

                                List<int> primeNumbers = new List<int>();

                                for (int i = 0; i < n; i++)
                                {
                                    if (IsPrime(i)) primeNumbers.Add(i);
                                }

                                if (primeNumbers.Count > 0)
                                {
                                    writer.WriteLine("OK*");
                                    foreach (int item in primeNumbers)
                                    {
                                        writer.WriteLine(item);
                                    }
                                    writer.WriteLine("OK!");
                                    writer.Flush();
                                    break;
                                }

                                writer.WriteLine("ERR|Didn't found any prime number");
                                writer.Flush();
                                break;


                            }
                        case "ADD":
                            {
                                if (stringParts.Length != 3)
                                {
                                    writer.WriteLine("ERR|You have to provide 2 numbers!");
                                    writer.Flush();
                                    break;
                                }

                                int a = int.Parse(stringParts[1]);
                                int b = int.Parse(stringParts[2]);

                                writer.WriteLine($"OK|{a}+{b} = {a+b}");
                                writer.Flush();
                                break;
                            }
                        case "FIBO":
                            {
                                if (stringParts.Length != 2)
                                {
                                    writer.WriteLine("ERR|You have to provide 1 number!");
                                    writer.Flush();
                                    break;
                                }

                                int n = int.Parse(stringParts[1]);

                                writer.WriteLine(Fibonacci(n));
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
