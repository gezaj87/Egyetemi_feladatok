using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XU3R7F
{
    internal class SzenzorHalozat : IEnumerable
    {

        public IEnumerator GetEnumerator()
        {
            for (int i = szenzorok.Count - 1; i >= 0; i--)
            {
                yield return szenzorok[i].Clone();
            }
        }

        private List<Szenzor> szenzorok = new List<Szenzor>();

        public void Telepit(Szenzor szenzor)
        {
            szenzorok.Add(szenzor);
        }

        public List<Szenzor> AktivSzenzorok
        {
            get
            {
                List<Szenzor> temp = new List<Szenzor>();

                foreach (Szenzor szenzor in szenzorok)
                {
                    if (szenzor.Aktiv)
                    {
                        temp.Add(szenzor.Clone() as Szenzor);
                    }
                }

                return temp;
            }
        }

        



    }
}
