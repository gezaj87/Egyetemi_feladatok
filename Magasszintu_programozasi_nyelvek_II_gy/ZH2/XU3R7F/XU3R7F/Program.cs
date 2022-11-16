using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace XU3R7F
{
    internal class Program
    {
        public static Random rnd = new Random();

		static void TelepitSzenzorHalozat(string input, SzenzorHalozat halozat)
		{
			int oldal = 0;

			StreamReader sr = null;
			try
			{
				sr = new StreamReader(input);
			
			while (!sr.EndOfStream)
			{
				string sor = sr.ReadLine();
				string[] adatok = sor.Split(';');
					try
					{
						Homero homero = new Homero(int.Parse(adatok[1]), int.Parse(adatok[2]),
												   int.Parse(adatok[3]), int.Parse(adatok[4]));
						if (rnd.NextDouble() < 0.3)
							homero.SetAktiv(false);
						halozat.Telepit(homero);
					}
					catch (AlacsonyAlsoHatarException ex)
					{
						Console.WriteLine($"Hiba az {oldal}. sorban: {ex.Message} A beírt érték: ${ex.BeirtErtek}");
					}
					oldal++;
			}
			}
			catch (FileNotFoundException ex)
			{
				Console.WriteLine(ex.Message);
			}
            finally
            {
				if (sr != null)
					sr.Close();
			}
			
		}



		static void Main(string[] args)
        {

            SzenzorHalozat halozat = new SzenzorHalozat();
            TelepitSzenzorHalozat("szenzorok.csv", halozat);

            foreach (Szenzor szenzor in halozat)
            {
                Console.WriteLine(szenzor);
            }




            Console.ReadLine();

            

        }
    }
}
