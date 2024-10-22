using System;
using System.Collections.Generic;

namespace Tjuv_Polis
{
    internal class Fangelse
    {
        private List<Tjuv> tjuvar = new List<Tjuv>();
        private Random random = new Random();

        public void LaggTillTjuv(Tjuv tjuv)
        {
            tjuvar.Add(tjuv);
        }

        public void FlyttaTjuvar(int bredd, int höjd)
        {
            // Flytta tjuvarna inom fängelset
            foreach (var tjuv in tjuvar)
            {
                tjuv.Flytta(bredd, höjd);
            }
        }

        public void VisaFangelse()
        {
            Console.WriteLine("\nFängelse:");
            char[,] fangelseRutorna = new char[10, 10];

            // Initiera fängelset
            for (int x = 0; x < 10; x++)
                for (int y = 0; y < 10; y++)
                    fangelseRutorna[x, y] = ' ';

            // Placera tjuvar i fängelset
            foreach (var tjuv in tjuvar)
            {
                if (tjuv.X < 10 && tjuv.Y < 10) // Säkerställ att tjuven är inom fängelset
                {
                    fangelseRutorna[tjuv.X, tjuv.Y] = 'T'; // T för tjuv
                }
            }

            // Skriv ut fängelset
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    Console.Write(fangelseRutorna[x, y]);
                }
                Console.WriteLine();
            }
        }

        // Nytt property för att få tillgång till listan av tjuvar
        public List<Tjuv> Tjuvar => tjuvar;
    }
}

