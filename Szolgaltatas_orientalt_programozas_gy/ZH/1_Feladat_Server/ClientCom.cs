using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _1_Feladat_Server
{
    internal class ClientCom
    {
        private StreamWriter writer;
        private StreamReader reader;

        private static List<User> users = new List<User>();
        private string user = null;
        private List<string> transfers = new List<string>();


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
                    string line = reader.ReadLine();
                    Console.WriteLine(line);

                    string[] stringParts = line.Split('|');
                    string command = stringParts[0].ToUpper();

                    switch (command)
                    {
                        case "UTALASOK":
                            {
                                if (this.transfers.Count == 0)
                                {
                                    writer.WriteLine("ERR|There were not money transfer to anybody!");
                                    writer.Flush();
                                    break;
                                }

                                writer.WriteLine("OK*");
                                foreach (string item in this.transfers)
                                {
                                    writer.WriteLine(item);
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
                                users.Add(new User(username, password, 0));
                                loginSuccess = true;

                                if (loginSuccess)
                                {
                                    this.user = username;
                                    writer.WriteLine("OK|Login successful");
                                }
                                else
                                {
                                    writer.WriteLine("ERR|wrong username or password");
                                }
                                writer.Flush();
                                break;
                            }
                        case "FELTOLT":
                            {
                                if (this.user == null)
                                {
                                    writer.WriteLine("ERR|You are not logged in!");
                                    writer.Flush();
                                    break;
                                }

                                if (stringParts.Length != 2)
                                {
                                    writer.WriteLine("ERR|You have to provide the amount!");
                                    writer.Flush();
                                    break;
                                }

                                int amount = int.Parse(stringParts[1]);
                                for (int i = 0; i < users.Count; i++)
                                {
                                    if (this.user == users[i].Name)
                                    {
                                        users[i].Money += amount;
                                        break;
                                    }
                                }

                                writer.WriteLine($"{amount} was added to your account!");
                                writer.Flush();
                                break;

                            }

                        case "LEKERDEZ":
                            {
                                if (this.user == null)
                                {
                                    writer.WriteLine("ERR|You are not logged in!");
                                    writer.Flush();
                                    break;
                                }

                                foreach (User user in users)
                                {
                                    if (user.Name == this.user)
                                    {
                                        writer.WriteLine($"You have {user.Money} money.");
                                        break;
                                    }
                                }
                                writer.Flush();
                                break;
                            }

                        case "UTAL":
                            {
                                ////- utal|celuser|osszeg: átutal adott összegű pénzt az egyenlegről az adott user-nek. A céluser accountnak léteznie kell, és a számlán megfelelő 
                                //   mennyiségű pénznek léteznie kell.
                                if (this.user == null)
                                {
                                    writer.WriteLine("ERR|You are not logged in!");
                                    writer.Flush();
                                    break;
                                }

                                if (stringParts.Length != 3)
                                {
                                    writer.WriteLine("ERR|You have to provide the username and the amount of money to transfer!");
                                    writer.Flush();
                                    break;
                                }

                                string userToTransferMoney = stringParts[1];
                                int amount = int.Parse(stringParts[2]);

                                bool success = false;

                                for (int i = 0; i < users.Count; i++)
                                {
                                    if (users[i].Name == this.user)
                                    {
                                        if (users[i].Money >= amount)
                                        {
                                            success = true;
                                            users[i].Money -= amount;
                                            break;
                                        } else
                                        {
                                            break;
                                        }
                                    }
                                }

                                if (!success)
                                {
                                    writer.WriteLine($"ERR|You have not enough money on your account!");
                                    writer.Flush();
                                    break;
                                }

                                bool foundUser = false;
                                for (int i = 0; i < users.Count; i++)
                                {
                                    if (users[i].Name == userToTransferMoney)
                                    {
                                        foundUser = true;
                                        users[i].Money += amount;
                                        //- utalasok: string lista, megadja hogy kinek és mennyi pénzt utaltunk át korábban, és mikor történt az utalás (időpont) (Több soros lista...)
                                        this.transfers.Add($"{DateTime.Now} - {userToTransferMoney} - {amount}");
                                        break;
                                    }
                                }

                                if (!foundUser)
                                {
                                    writer.WriteLine("User is not found in database!");
                                    writer.Flush();
                                    break;
                                }

                                writer.WriteLine($"You have successfully transferred {amount} to {userToTransferMoney}!");
                                writer.Flush();
                                break;


                            }

                        case "LOGOUT":
                            {
                                if (string.IsNullOrEmpty(this.user))
                                {
                                    writer.WriteLine("ERR|Nem is volt login");
                                }else
                                {
                                    writer.WriteLine("OK");
                                }
                                
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
