using System;
using VoidChainLib.Objects;
namespace VoidChainConsole
{
    class MainClass
    {
        public static void Main(string[] args)
        {

            VoidChainLib.BlockChain.Block block = new VoidChainLib.BlockChain.Block();
            var gen = block.Genesis();
           // Console.WriteLine(hex);
        }
    }
}
