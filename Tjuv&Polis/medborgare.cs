using System;

namespace Tjuv_Polis
{
    public class Medborgare : Person
    {
        public Medborgare(int x, int y) : base(x, y)
        {
            Bokstav = 'M';
            //  objekt i inventory
            Inventar.Add("Nycklar");
            Inventar.Add("Mobiltelefon");
            Inventar.Add("Pengar");
            Inventar.Add("Klocka");
        }
    }
}
