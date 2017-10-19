using System;
using VoidChainLib.Objects;
namespace VoidChainConsole
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            byte[] b = new byte[] { 12, 6 };
            Console.WriteLine(b.ToHex()[0]);
        }
    }
}
