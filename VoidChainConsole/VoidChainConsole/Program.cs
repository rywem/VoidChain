using System;
using VoidChainLib.Objects;
namespace VoidChainConsole
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            byte[] b = new byte[] { 2, 6, 8, 15, 2 };
            var hex = b.ToHexList();
            foreach (var item in hex)
            {
                Console.WriteLine(item);
            }
           // Console.WriteLine(hex);
        }
    }
}
