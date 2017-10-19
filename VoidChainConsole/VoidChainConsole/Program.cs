using System;
using VoidChainLib.Objects;
namespace VoidChainConsole
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            byte[] b = new byte[] { 2, 6, 8, 15, 2 };
            string hex = b.ToHex();
            Console.WriteLine(hex);
        }
    }
}
