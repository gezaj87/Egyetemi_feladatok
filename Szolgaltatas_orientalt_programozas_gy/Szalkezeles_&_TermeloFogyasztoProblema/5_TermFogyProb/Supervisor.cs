using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _5_TermFogyProb
{
    internal class Supervisor
    {
        public const int DIMENSION_A = 80;
        public const int DIMENSION_B = 25;

        public static Random rnd = new Random();
        private static string[,] store = new string[DIMENSION_A, DIMENSION_B];
        private static bool noMoreProducers = false;
        private static bool noMoreCustomers = false;
        private static int numberOfCustomers = 0;
        private static int numberOfProducers = 0;

        static bool NoMoreProducers { get { return noMoreProducers; } }
        static bool NoMoreCustomers { get { return noMoreCustomers; } }

        private const string PRODUCER_WAIT = "Store is full, Producer have to wait!";


        public static void AddCustomer()
        {
            lock (typeof(Supervisor))
            {
                numberOfCustomers++;
            }
        }

        public static void AddProducer()
        {
            lock (typeof(Supervisor))
            {
                numberOfProducers++;
            }
        }

        public static void RemoveCustomer()
        {
            lock (typeof(Supervisor))
            {
                if (numberOfCustomers > 0)
                    numberOfCustomers--;

                if (numberOfCustomers <= 0)
                {
                    noMoreCustomers = true;
                    lock (store)
                    {
                        Monitor.PulseAll(store);
                    }
                }
            }
        }

        public static void RemoveProducer()
        {
            lock (typeof(Supervisor))
            {
                if (numberOfProducers > 0)
                    numberOfProducers--;

                if (numberOfProducers <= 0)
                {
                    noMoreProducers = true;
                    lock (store)
                    {
                        Monitor.PulseAll(store);
                    }
                }
            }
        }


        public static bool AddToStore(char X)
        {
            int dimension_a = 0;
            int dimension_b = 0;

            bool success = false;

            lock (store)
            {
                while (!success)
                {
                    dimension_a = rnd.Next(0, DIMENSION_A);
                    dimension_b = rnd.Next(0, DIMENSION_B);

                    if (noMoreCustomers) throw new Exception("No more customer. Stop!");

                    if (string.IsNullOrEmpty(store[dimension_a, dimension_b]))
                    {
                        success = true;
                        store[dimension_a, dimension_b] = X.ToString();
                        Console.SetCursorPosition(dimension_a, dimension_b);
                        Console.Write(store[dimension_a, dimension_b]);
                        
                    }

                    
                }

                //store[dimension_a, dimension_b] = X.ToString();
                Monitor.PulseAll(store);
                return true;

            }
        }

        public static void TakeFromStore()
        {
            bool success = false;

            lock (store)
            {
                while (!success)
                {
                    bool foundOne = false;

                    for (int i = 0; i < store.GetLength(0); i++)
                    {
                        for (int j = 0; j < store.GetLength(1); j++)
                        {
                            if (!string.IsNullOrEmpty(store[i, j]))
                            {
                                store[i, j] = null;
                                Console.SetCursorPosition(i, j);
                                Console.Write(" ");
                                foundOne = true;
                                success = true;
                                break;
                            }
                        }

                        if (foundOne) break;
                    }

                    if (!foundOne && noMoreProducers) throw new Exception("Customer took all the items and stopped.");

                    if (!foundOne && !noMoreProducers) Monitor.Wait(store);

                }

                Monitor.PulseAll(store);


            }
            

        }
    }
}
