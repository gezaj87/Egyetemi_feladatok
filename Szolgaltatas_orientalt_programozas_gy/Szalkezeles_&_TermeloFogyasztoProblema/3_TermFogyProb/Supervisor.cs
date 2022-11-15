
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _3_TermFogyProb
{
    internal class Supervisor
    {
        private static List<int> store = new List<int>();
        private const int CAPACITY = 50;
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


        public static void AddToStore(int x)
        {
            lock (store)
            {
                while (store.Count >= CAPACITY)
                {
                    if (noMoreCustomers) throw new Exception("No more customer. Stop!");

                    Console.WriteLine(PRODUCER_WAIT + " |" + store.Count);
                    Monitor.Wait(store);
                }

                store.Add(x);
                Monitor.PulseAll(store);
            }
        }

        public static int TakeFromStore()
        {
            int x;

            lock (store)
            {
                while (store.Count == 0)
                {
                    if (noMoreProducers)
                        throw new Exception("Customer took all the items and stopped.");

                    Monitor.Wait(store);
                }

                x = store[0];
                store.RemoveAt(0);
                Monitor.PulseAll(store);
            }

            return x;
        }
    }
}
