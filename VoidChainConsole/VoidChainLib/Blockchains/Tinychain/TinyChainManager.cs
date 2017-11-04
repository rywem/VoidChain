using System;
using VoidChainLib.Objects;

namespace VoidChainLib.Blockchains.Tinychain
{
    public class TinyChainManager
    {
        public TinyBlockchain BlockChain { get; set; }

        public TinyChainManager()
        {
            this.BlockChain = new TinyBlockchain();
        }
        public void Run()
        {
            Helpers helper = new Helpers();
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                this.BlockChain.AddBlock(new TinyBlock(BlockChain.Chain.Count, DateTime.Now, new TinyTransaction(helper.RandomString(20, random)), BlockChain.GetLatestBlock().Hash));
            }

            //Review blockchain
            foreach (var chain in BlockChain.Chain)
            {
                Console.WriteLine($"{chain.Index} \tPH: {chain.PreviousHash}, \t {chain.Timestamp} \n{chain.Index} \tNH: {chain.Hash}");
            }
        }
    }
}
