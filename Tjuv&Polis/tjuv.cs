using System;

namespace Tjuv_Polis
{
    public class Tjuv : Person
    {
        public bool Efterlyst { get; set; } // Flagga för att indikera om tjuven är efterlyst

        public Tjuv(int x, int y) : base(x, y)
        {
            Bokstav = 'T';
            Efterlyst = false; // Tjuven är inte efterlyst vid start
        }
    }
}