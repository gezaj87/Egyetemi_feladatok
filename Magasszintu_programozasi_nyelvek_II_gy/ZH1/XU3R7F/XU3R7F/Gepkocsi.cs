using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XU3R7F
{
    class Gepkocsi
    {
        private static string VALIDKARAKTEREK = "0123456789QWERTZUIOPASDFGHJKLYXCVBNM-";
        private static string SZAMOK = "0123456789";
        private static string BETUK = "QWERTZUIOPASDFGHJKLYXCVBNM";

        //------konstruktorok
        //Készítsen konstruktort, mely az összes adatot (rendszám, évjárat, eredeti ár és állapot) bekéri és eltárolja.
        public Gepkocsi(string rendszam, int evjarat, int eredetiAr, Allapot allapot)
        {
            this.Rendszam = rendszam;
            this.Evjarat = evjarat;
            this.eredetiAr = eredetiAr;
            this.Allapot = allapot;
        }

        //Készítsen konstruktort, mely minden adatot bekér a gépkocsi állapotán kívül! Ezt az előző konstruktor meghívásával automatikusan megkíméltre állítja!
        public Gepkocsi(string rendszam, int evjarat, int eredetiAr) : this(rendszam, evjarat, eredetiAr, Allapot.Megkimelt) { }
        



        private string rendszam;
        public string Rendszam
        {
            get
            {
                return rendszam;
            }
            private set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("Rendszám nem lehet null vagy üres string!");

                //Csak számokat, ékezetmentes nagybetűket és ’-’ karaktert tartalmazhat!
                foreach (char betu in value)
                {
                    if (!VALIDKARAKTEREK.Contains(betu))
                        throw new Exception($"A rendszám nem valid karaktert tartalmaz: {betu}");
                }

                //Pontosan 7 karakter hosszú!
                if (value.Length != 7)
                    throw new Exception("A rendszám 7 karakter hosszú kell legyen!");

                //Az első három karakter csak betű, míg az utolsó három karakter csak szám lehet. Az utolsó három karakter nem lehet csupa 0!
                for (int i = 0; i <= 2; i++)
                {
                    if (!BETUK.Contains(value[i]))
                        throw new Exception("Az rendszám első három karakter betű kell legyen!");
                }

                for (int i = 4; i <= 6; i++)
                {
                    if (!SZAMOK.Contains(value[i]))
                        throw new Exception("A rendszám utolsó három karaktere betű kell legyen!");
                }

                rendszam = value;

            }
        }

        private int evjarat;
        private int evjaratMegadva = 0;
        public int Evjarat
        {
            get
            {
                return evjarat;
            }
            set
            {
                //Értéke az [1950; 2022] intervallumban érvényes
                if (value < 1950 || value > 2022)
                    throw new Exception("Évjárat engedélyezett intervallum: [1950; 2022] !");
                //Értéke csak egyszer adható meg
                if (evjaratMegadva != 0)
                    throw new Exception("Az évjárat csak 1x adható meg!");

                evjarat = value;
                evjaratMegadva++;

            }
        }

        private int eredetiAr;
        private int eredetiArMegadva = 0;
        private void SetEredetiAr (int ar)
        {
            //Értéke a [300 000; 12 000 000]  intervallumban érvényes.
            if (ar < 300000 || ar > 12000000)
                throw new Exception("Eredeti ár értéke csak [300 000; 12 000 000] intervallumban érvenyes!");
            // Értéke csak egyszer adható meg
            if (eredetiArMegadva != 0)
                throw new Exception("Eredi ár csak 1x adható meg!");

            eredetiAr = ar;
        }
        public int GetEredetiAr()
        {
            return eredetiAr;
        }

        public Allapot Allapot { get; set; }

        //egész értékű, csak olvasható property. Értéke az aktuális év és az évjárat különbsége!
        public int Kor
        {
            get
            {
                return 2022 - evjarat;
            }
        }

        //Extra ár: egész értékű, csak olvasható property. Amennyiben a gépkocsi legfeljebb 2 éves
        //és újszerű, úgy az extra ár az eredeti ár 2%-a, egyébként 0F t.Alkalmazzon késői kötést!
        public virtual int ExtraAr
        {
            get
            {
                if (Kor <= 2 && Allapot == Allapot.Ujszeru)
                {
                    return (int)(eredetiAr + (eredetiAr * 0.02));
                }
                else
                {
                    return 0;
                }
            }
        }


        //Készítsen egész értékkel visszatérő késői kötésű metódust VetelAr néven a következők
        //szerint.A vételárhoz először meg kell állapítani mennyit amotrizálódik évente az autó az
        //állapotától függően.
        public virtual int VetelAr()
        {
            int amortizacioMerteke = 0;

            if (this.Allapot == Allapot.Ujszeru) amortizacioMerteke = 9;
            if (this.Allapot == Allapot.Megkimelt) amortizacioMerteke = 10;
            if (this.Allapot == Allapot.Serult) amortizacioMerteke = 11;
            if (this.Allapot == Allapot.Hibas) amortizacioMerteke = 12;

            //eredetiAr = amortizacio gepjarmuKora + extraAr
            return (int)(eredetiAr * Math.Pow(amortizacioMerteke, Kor) + ExtraAr);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Rendszám: {Rendszam}");
            sb.AppendLine($"Évjárat: {Evjarat}");
            sb.AppendLine($"Rendszám: {Rendszam}");
            sb.AppendLine($"Eredeti ár: {GetEredetiAr()}");
            sb.AppendLine($"Rendszám: {Rendszam}");
            sb.AppendLine($"Állapot: {Allapot}");
            sb.AppendLine($"Rendszám: {Rendszam}");
            sb.AppendLine($"Kor: {Kor}");
            sb.AppendLine($"Extra ár: {ExtraAr}");

            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            //Két gépkocsi akkor egyezik meg, ha ugyanaz a rendszámuk

            if(obj is Gepkocsi)
            {
                if ((obj as Gepkocsi).Rendszam == this.Rendszam)
                    return true;
            }

            return false;
        }


    }
}
