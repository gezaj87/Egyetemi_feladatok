using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XU3R7F
{
    internal class Homero : Szenzor, IHomero
    {
        public Homero(int pozX, int pozY, int alsohatar, int felsohatar) : base(new Pozicio(pozX, pozY))
        {
            HatarokatBeallit(alsohatar, felsohatar);
            SetAktiv(true);
        }

        private int alsoHatar;
        public int AlsoHatar
        {
            get
            {
                return alsoHatar;
            }
            private set
            {
                if (value < -60)
                    throw new AlacsonyAlsoHatarException(value);
                alsoHatar = value;
            }

        }

        private int felsohatar;
        public int FelsoHatar
        {
            get
            {
                return felsohatar;
            }
            private set
            {
                felsohatar = value;
            }
        }

        private bool aktiv;
        public void SetAktiv(bool ertek)
        {
            aktiv = ertek;
        }


        public override bool Aktiv { get { return aktiv; } }

        public override void Adatkuldes()
        {
            //Hőmérséklet a(z) (50 ;730) pozíción 2022.05.13 16:53 időpontban : 32,05°C
            Console.WriteLine($"Hőmérséklet a(z) ({Pozicio.x};{Pozicio.y}) pozíción {DateTime.Now.ToString("yyyy.MM.dd HH:mm")} időpontban : {HomersekletetMer()}");

        }

        public override object Clone()
        {
            Homero klon = new Homero(Pozicio.x, Pozicio.y, AlsoHatar, FelsoHatar);
            return klon;
        }

        public void HatarokatBeallit(int alsoHatar, int felsoHatar)
        {
            //Elmenti a paraméterben kapott értékeket.
            this.AlsoHatar = alsoHatar;
            this.FelsoHatar = felsoHatar;
        }

        public double HomersekletetMer()
        {
            if (!Aktiv)
                throw new SzenzorInaktivException();

            
            return Math.Round(Program.rnd.NextDouble() * (FelsoHatar - AlsoHatar) + AlsoHatar, 2);
            
        }


        public override string ToString()
        {
            return string.Format("Hőmérő: {0}, A:{1} - F{2}", base.ToString(), AlsoHatar, FelsoHatar);
        }

    }
}
