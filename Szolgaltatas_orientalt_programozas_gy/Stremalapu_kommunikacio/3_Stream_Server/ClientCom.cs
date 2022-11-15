using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _3_Stream_Server
{
    internal class ClientCom
    {
        private StreamWriter writer;
        private StreamReader reader;

        private Dictionary<string, string> users = new Dictionary<string, string>();
        private string user = null;
        private List<Product> products = new List<Product>();

        public ClientCom(TcpClient tcpClient)
        {
            this.writer = new StreamWriter(tcpClient.GetStream(), Encoding.UTF8);
            this.reader = new StreamReader(tcpClient.GetStream(), Encoding.UTF8);

            users.Add("Geza", "123");
            users.Add("Malacka", "123");
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
                        case "LIST":
                            {
                                if (products.Count == 0)
                                {
                                    writer.WriteLine("OK|There is no product in Store yet!");
                                    writer.Flush();
                                    break;
                                }

                                writer.WriteLine("OK*");
                                foreach (Product product in products)
                                {
                                    writer.WriteLine($"{product.Id} - {product.Name} - {product.Price} - {product.User}");
                                }
                                writer.WriteLine("OK!");
                                writer.Flush();
                                break;


                            }
                        case "LOGIN":
                            {
                                if (stringParts.Length != 3)
                                {
                                    writer.WriteLine("ERR|You have to provide a username and a password!");
                                    writer.Flush();
                                    break;
                                }

                                string username = stringParts[1];
                                string password = stringParts[2];

                                bool loginSuccess = false;
                                foreach (KeyValuePair<string, string> user in users)
                                {
                                    if (user.Value == username && user.Value == password)
                                    {
                                        loginSuccess = true;
                                        this.user = username;
                                        break;
                                    }
                                }

                                if (loginSuccess)
                                {
                                    writer.WriteLine("OK|Login successful");
                                }
                                else
                                {
                                    writer.WriteLine("ERR|wrong username or password");
                                }
                                writer.Flush();
                                break;
                            }
                        case "ADD":
                            {
                                if (this.user == null)
                                {
                                    writer.WriteLine("ERR|You are not logged in!");
                                    writer.Flush();
                                    break;
                                }

                                if (stringParts.Length != 3)
                                {
                                    writer.WriteLine("ERR|You have to provide 3 parameters: id, name, price");
                                    writer.Flush();
                                    break;
                                }

                                string id = stringParts[1];
                                string name = stringParts[2];
                                int price = int.Parse(stringParts[3]);

                                bool isIdUnique = true;
                                foreach (Product item in products)
                                {
                                    if (item.Id == id)
                                    {
                                        isIdUnique= false;
                                    }
                                }

                                if (!isIdUnique)
                                {
                                    writer.WriteLine("ERR|id have to be unique!");
                                    writer.Flush();
                                    break;
                                }

                                products.Add(new Product(id, name, price, this.user));

                                writer.WriteLine("OK|You have added a product to the store!");
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
