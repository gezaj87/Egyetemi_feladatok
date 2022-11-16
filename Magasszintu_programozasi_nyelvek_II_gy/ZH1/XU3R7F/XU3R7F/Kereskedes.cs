using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XU3R7F
{
    class Kereskedes
    {
        public Kereskedes()
        {
            gepkocsik = new List<Gepkocsi>();
        }

        //---Mezők és props
        //Gépkocsik listája: tetszőleges mennyiségű Gepkocsi típusú objektum tárolására alkalmas lista ellátva a megfelelő szintű védelemmel.
        private List<Gepkocsi> gepkocsik;
        public List<Gepkocsi> Gepkocsik
        {
            get
            {
                List<Gepkocsi> temp = new List<Gepkocsi>();
                foreach (Gepkocsi gepkocsi in gepkocsik)
                {
                    temp.Add(gepkocsi);
                }

                return temp;
            }
        }

        //Személygépkocsik: Összegyűjti és visszaadja a gépkocsik között megtalálható személygépkocsikat.
        public List<Szemelygepkocsi> Szemelygepkocsik
        {
            get
            {
                List<Szemelygepkocsi> szemKocsik = new List<Szemelygepkocsi>();
                bool van = false;

                foreach (Gepkocsi gepkocsi in gepkocsik)
                {
                    if (gepkocsi is Szemelygepkocsi)
                    {
                        szemKocsik.Add(gepkocsi as Szemelygepkocsi);
                        van = true;
                    }

                }

                if (!van)
                    throw new Exception("Nincs személygépkocsi rögzítve!");

                return szemKocsik;
            }
        }

        //Legolcsóbb megkímélt személygépkocsi: Megkeresi és viszatér a legolcsóbb olyan személygépkocsi objektummal, melynek állpota megkímélt! Vizsgálja meg, hogy van- egyáltalán személygépkocsi a gépkocsik között!
        public Szemelygepkocsi LegolcsobbMegkimeltSzemgkocsi
        {
            get
            {
                if (Szemelygepkocsik.Count == 0)
                    throw new Exception("Nincs személygépkocsi a listában!");

                Szemelygepkocsi LegolcsobbMegkimelt = Szemelygepkocsik.First();

                bool vanMegkimelt = false;

                foreach (Szemelygepkocsi kocsi in Szemelygepkocsik)
                {
                    if (kocsi.Allapot == Allapot.Megkimelt)
                    {
                        LegolcsobbMegkimelt = kocsi;
                        vanMegkimelt = true;
                        break;
                    }
                }

                if (!vanMegkimelt)
                    throw new Exception("Nincs megkímélt állapotó személygépkocsi rögzítve!");

                foreach (Szemelygepkocsi kocsi in Szemelygepkocsik)
                {
                    if (kocsi.VetelAr() < LegolcsobbMegkimelt.VetelAr())
                        LegolcsobbMegkimelt = kocsi;
                }


                return LegolcsobbMegkimelt;
            }
        }

        //Indexelő: Készítsen Gepkocsi típusú objektummal visszatérő indexelőt, mely rendszám szám alapján adja vissza a keresett gépkocsit!
        public Gepkocsi this[int index]
        {
            get
            {
                if (index >= 0 && index < gepkocsik.Count)
                    return gepkocsik[index];
                else return null;
            }
        }

        //Készítsen metódust AddGepkocsi néven, mely paraméterben kér egy Gepkocsi típusú objektumot! Ellenőrizze, hogy az adott gépkocsi szerepe-e már a rendszerben, mielőtt elmenti!
        public void AddGepkocsi(Gepkocsi kocsi)
        {
            

            gepkocsik.Add(kocsi);


        }

        //Készítsen metódust SzemelyGepkocsikAdottArig néven, mely paraméterben bekér egy állapotot és egy maximum árat.Gyűjtse listába és térjen vissza azon személygépkocsik listájával, melyek megfelelnek a paraméterben kapott értékeknek!
        public List<Szemelygepkocsi> SzemelyGepkocsikAdottArig(Allapot allapot, int maxVetelar)
        {
            if (Szemelygepkocsik.Count == 0)
                throw new Exception("Nincs személygépkocsi tárolva!");

            List<Szemelygepkocsi> temp = new List<Szemelygepkocsi>();

            bool van = false;

            foreach (Szemelygepkocsi kocsi in Szemelygepkocsik)
            {
                if (kocsi.Allapot == allapot && kocsi.VetelAr() <= maxVetelar)
                {
                    temp.Add(kocsi);
                    van = true;
                }
            }

            if (van)
                return temp;
            else return null;

            


            return temp;
        }





    }
}
