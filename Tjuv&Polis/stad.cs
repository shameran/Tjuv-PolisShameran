using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Tjuv_Polis
{
    public class Stad
    {
        private int bredd;
        private int höjd;
        private List<Person> personer;
        private Random random;

        private int antalRånadeMedborgare = 0;
        private int antalGripnaTjuvar = 0;
        private Fangelse fangelse; // Instans av fängelset

        public Stad(int bredd, int höjd)
        {
            this.bredd = bredd;
            this.höjd = höjd;
            personer = new List<Person>();
            random = new Random();
            fangelse = new Fangelse(); // Initiera fängelset

            // Skapa medborgare, tjuvar och poliser
            for (int i = 0; i < 40; i++)
                personer.Add(new Medborgare(random.Next(bredd), random.Next(höjd)));

            for (int i = 0; i < 5; i++)
            {
                var tjuv = new Tjuv(random.Next(bredd), random.Next(höjd));
                personer.Add(tjuv);
            }

            for (int i = 0; i < 15; i++)
                personer.Add(new Polis(random.Next(bredd), random.Next(höjd)));
        }

        public void Start()
        {
            while (true)
            {
                Console.Clear();
                Staden();
                VisaFängelse();
                fangelse.FlyttaTjuvar(10, 10); // Flytta tjuvarna i fängelset

                HanteraMöte();
                FlyttaPersoner();

                Console.WriteLine($"Antal rånade medborgare: {antalRånadeMedborgare}");
                Console.WriteLine($"Antal gripna tjuvar: {antalGripnaTjuvar}");
                Console.WriteLine($"Antal tjuvar i fängelset: {fangelse.Tjuvar.Count}");


                Thread.Sleep(200);
            }
        }

        private void Staden()
        {
            char[,] stadRutorna = new char[bredd, höjd];

            // Initiera staden med punkter
            for (int x = 0; x < bredd; x++)
                for (int y = 0; y < höjd; y++)
                    stadRutorna[x, y] = '.';

            foreach (var person in personer)
                stadRutorna[person.X, person.Y] = person.Bokstav;

            // Skriv ut staden
            for (int y = 0; y < höjd; y++)
            {
                for (int x = 0; x < bredd; x++)
                {
                    Console.Write(stadRutorna[x, y]);
                }
                Console.WriteLine();
            }
        }

        private void VisaFängelse()
        {
            fangelse.VisaFangelse(); // Anropar fängelsets VisaFangelse-metod
        }

        private void HanteraMöte()
        {
            // personer baserat på deras position
            var grupper = personer.GroupBy(p => (p.X, p.Y)).Where(g => g.Count() > 1);

            foreach (var grupp in grupper)
            {
                var lista = grupp.ToList();
                for (int i = 0; i < lista.Count; i++)
                {
                    if (lista[i] is Tjuv tjuv)
                    {
                        // tjuven rånar någon medborgare 
                        for (int j = 0; j < lista.Count; j++)
                        {
                            if (lista[j] is Medborgare medborgare)
                            {
                                if (!PolisFinnsINärheten(tjuv)) // Kontrollera att ingen polis är nära
                                {
                                    RånaMedborgare(tjuv, medborgare);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void RånaMedborgare(Tjuv tjuv, Medborgare medborgare)
        {
            Console.WriteLine("Tjuven rånar medborgaren.");
            var stöld = medborgare.Inventar.RemoveRandomItem(random);
            if (stöld != null)
            {
                tjuv.Inventar.Add(stöld);
                Console.WriteLine($"Tjuven har stulit: {stöld}");
                antalRånadeMedborgare++;
                tjuv.Efterlyst = true; // Tjuven blir efterlyst efter rånet
                Thread.Sleep(2000); // paus
            }
        }

        private bool PolisFinnsINärheten(Tjuv tjuv)
        {
            // polis inom en radie av 1 ruta
            return personer.OfType<Polis>().Any(polis =>
                Math.Abs(tjuv.X - polis.X) <= 1 && Math.Abs(tjuv.Y - polis.Y) <= 1);
        }

        private void FlyttaPersoner()
        {
            List<Tjuv> tjuvarAttTaBort = new List<Tjuv>(); // Lista för tjuvar som ska tas bort

            foreach (var person in personer)
            {
                person.Flytta(bredd, höjd);
                if (person is Tjuv tjuv && tjuv.Efterlyst)
                {
                    // Låt polisen jaga tjuven
                    foreach (var polis in personer.OfType<Polis>())
                    {
                        if (Math.Abs(tjuv.X - polis.X) <= 1 && Math.Abs(tjuv.Y - polis.Y) <= 1)
                        {
                            Console.WriteLine("Polis jagar tjuven!");
                            tjuv.Inventar.Clear(); // Om tjuven fångas, töm deras inventar
                            antalGripnaTjuvar++;
                            fangelse.LaggTillTjuv(tjuv); // Förpassa tjuven till fängelset
                            tjuvarAttTaBort.Add(tjuv); // Lägg till tjuven i borttagningslistan
                            tjuv.Efterlyst = false; // Återställ efterlysning
                            Thread.Sleep(2000);
                            break; // Sluta jaga denna tjuv
                        }
                    }
                }
            }

            // Ta bort tjuvarna från staden efter att ha avslutat iterationen
            foreach (var tjuv in tjuvarAttTaBort)
            {
                personer.Remove(tjuv);
            }
        }
    }
}
