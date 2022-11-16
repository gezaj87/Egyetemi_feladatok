using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XU3R7F
{
    class Szemelygepkocsi : Gepkocsi
    {
        static int[] szallSzem = new int[] { 2, 4, 5, 7 };


        //Készítsen konstruktort, mely az összes adatot (rendszám, évjárat, eredeti ár, állapot, szállítható személyek száma, vonóhorog és klíma) bekéri és eltárolja.
        public Szemelygepkocsi(string rendszam, int evjarat, int eredetiAr, Allapot allapot, int szallithatoSzemelyekSzama, bool horog, Klima klima) : base(rendszam, evjarat, eredetiAr, allapot)
        {
            this.SzallithatoSzemelyekSzama = szallithatoSzemelyekSzama;
            this.Vonohorog = horog;
            this.Klima = klima;
        }

        //Készítsen konstruktort, mely minden adatot bekér az gépkocsi állapotán és a klímán kívül! Ezt az előző konstruktor meghívásával automatikusan megkíméltre és digitális-ra (nem többzónás) állítja!
        public Szemelygepkocsi(string rendszam, int evjarat, int eredetiAr, int szallithatoSzemelyekSzama, bool horog) : this(rendszam, evjarat, eredetiAr, Allapot.Megkimelt, szallithatoSzemelyekSzama, horog, Klima.Digitalis) { }



        //----Mezők és property-k
        //Szállítható személyek száma: egész típusú adat, kívülről nem módosítható. Értéke a {2, 4, 5, 7} halmaz valamely eleme
        private int szallithatoSzemelyekSzama;
        public int SzallithatoSzemelyekSzama
        {
            get
            {
                return szallithatoSzemelyekSzama;
            }
            private set
            {
                if (!szallSzem.Contains(value))
                    throw new Exception("Megengedett értékek: 2, 4, 5, 7 !");

                szallithatoSzemelyekSzama = value;
            }
        }

        //Van vonóhorog: logikai érték, nincs megkötés.
        public bool Vonohorog { get; set; }

        public Klima Klima { get; set; }



       

        public override int ExtraAr
        {
            get
            {
               

                int vonohorog = 0;
                if (this.Vonohorog) vonohorog = 60000;

                int szallSzem7 = 0;
                if (this.SzallithatoSzemelyekSzama == 7) szallSzem7 = 100000;

                int klimaAra = 0;
                if (this.Klima == Klima.Manualis) klimaAra = 40000;
                if (this.Klima == Klima.Digitalis) klimaAra = 150000;
                if (this.Klima == Klima.DigitalisTobbzonas) klimaAra = 350000;

                return base.ExtraAr + vonohorog + szallSzem7 + klimaAra;
            }
        }

        public override int VetelAr()
        {
            int amortizacioMerteke = 0;
            
            if (this.Allapot == Allapot.Ujszeru) amortizacioMerteke = 8;
            if (this.Allapot == Allapot.Megkimelt) amortizacioMerteke = 9;
            if (this.Allapot == Allapot.Serult) amortizacioMerteke = 12;
            if (this.Allapot == Allapot.Hibas) amortizacioMerteke = 13;

            //Amennyiben a szállítható személyek száma 7, úgy az amortizáció mértékének számoljon az 1,2 - szeresével!
            if (szallithatoSzemelyekSzama == 7)
                amortizacioMerteke = (int)(amortizacioMerteke * 1.2);



            return (int)(base.GetEredetiAr() * Math.Pow(amortizacioMerteke, base.Kor) + this.ExtraAr);

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(base.ToString());
            sb.AppendLine($"Szállítható személyek száma: {SzallithatoSzemelyekSzama}");
            sb.AppendFormat("Vonóhorog: {0}", Vonohorog? "van" : "nincs");
            sb.AppendLine($"Klima: {Klima}");

            return sb.ToString();
        }

    }
}
