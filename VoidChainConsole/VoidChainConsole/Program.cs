using System;
using System.Collections.Generic;
using System.Linq;
using VoidChainLib.Objects;
using VoidChainLib.Blockchains.BasicChain;
namespace VoidChainConsole
{
    class MainClass
    {
        public static void Main(string[] args)
        {

            VoidChainLib.Blockchains.Tinychain.TinyChainManager manager = new VoidChainLib.Blockchains.Tinychain.TinyChainManager();
            manager.Run();
            Console.ReadLine();

            List<BasicTransaction> transactions = new List<BasicTransaction>();
            transactions.Add(new BasicTransaction()
            {
                Amount = 1.2m, Destination = "x", Source ="y", Signature = "33"
            });
            transactions.Add(new BasicTransaction()
            {
                Amount = 12.2m,
                Destination = "t",
                Source = "v",
                Signature = "233"
            });
            transactions.Add(new BasicTransaction()
            {
                Amount = 12.2m,
                Destination = "re",
                Source = "ye",
                Signature = "254g33"
            });

            BasicBlock block = new BasicBlock(transactions);
            string x = block.MerkleHash;
            Console.WriteLine(x);
            Console.ReadLine();

            VoidChainLib.Blockchains.Voidchain.VoidChain chain = new VoidChainLib.Blockchains.Voidchain.VoidChain();

            chain.Initialize();

            //Console.WriteLine(x.ToArray().ToUInt32());
            //VoidChainLib.BlockChain.Block block = new VoidChainLib.BlockChain.Block();
            //var gen = block.Genesis();
           // Console.WriteLine(hex);
        }
    }
}
