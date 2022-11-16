using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XU3R7F
{
    internal abstract class Szenzor : ICloneable
    {
        public Szenzor(Pozicio pozicio)
        {
            //Készítsen egy konstruktort, mely bekéri a pozíciót és eltárolja az abban található értéket!
            this.Pozicio = pozicio;
        }

        public Pozicio Pozicio { get; private set; }
        public abstract bool Aktiv { get; }

        public abstract void Adatkuldes();


        public abstract object Clone();

        public override string ToString()
        {
            return string.Format("{0} ({1}; {2})", Aktiv ? "On" : "Off", Pozicio.x, Pozicio.y);
        }

    }
}
