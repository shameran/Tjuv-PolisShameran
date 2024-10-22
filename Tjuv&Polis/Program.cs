using System;

namespace Tjuv_Polis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Staden simuleras...");
            Stad stad = new Stad(100, 25); // Skapar en stad med 100x25 rutor
            stad.Start();
        }
    }
}
