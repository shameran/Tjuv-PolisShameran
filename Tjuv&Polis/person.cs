using System;

namespace Tjuv_Polis
{
    public abstract class Person
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Inventory Inventar { get; private set; }
        public char Bokstav { get; protected set; }
        protected int Xdirection; // Riktning för rörelse i X-led
        protected int Ydirection;
        private static Random random = new Random();

        protected Person(int x, int y)
        {
            X = x;
            Y = y;
            Inventar = new Inventory();
            SlumpMässigt(); // slumpmässig rörelseriktning
        }

        //  slumpmässig riktning för rörelse
        private void SlumpMässigt()
        {
            int direction = random.Next(8); // ett värde mellan 0 och 7
            switch (direction)
            {
                case 0: Xdirection = -1; Ydirection = 0; break; // Vänster
                case 1: Xdirection = 1; Ydirection = 0; break; // Höger
                case 2: Xdirection = 0; Ydirection = 1; break; // Ner
                case 3: Xdirection = 0; Ydirection = -1; break; // Upp
                case 4: Xdirection = -1; Ydirection = 1; break; // Vänster ner
                case 5: Xdirection = -1; Ydirection = -1; break; // Vänster upp
                case 6: Xdirection = 1; Ydirection = 1; break; // Höger ner
                case 7: Xdirection = 1; Ydirection = -1; break; // Höger upp
            }
        }

        public void Flytta(int bredd, int höjd)
        {
            X = (X + Xdirection + bredd) % bredd; // Wrappa X-positionen
            Y = (Y + Ydirection + höjd) % höjd;   // Wrappa Y-positionen
        }
    }
}
