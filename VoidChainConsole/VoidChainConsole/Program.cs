using System;
using System.Collections.Generic;
using System.Linq;
using VoidChainLib.Objects;
namespace VoidChainConsole
{
    class MainClass
    {
        public static void Main(string[] args)
        {

            VoidChainLib.BlockChain.VoidChain chain = new VoidChainLib.BlockChain.VoidChain();
            chain.Initialize();

            Console.WriteLine(x.ToArray().ToUInt32());
            VoidChainLib.BlockChain.Block block = new VoidChainLib.BlockChain.Block();
            var gen = block.Genesis();
           // Console.WriteLine(hex);
        }
    }
}
