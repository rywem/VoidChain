using System;
using VoidChainLib.Objects;

namespace VoidChainLib.Blockchains.Tinychain
{
    public class TinyChainManager
    {
        public TinyChain BlockChain { get; set; }

        public TinyChainManager()
        {
            this.BlockChain = new TinyChain();
        }
        public void Run()
        {
            Helpers helper = new Helpers();
            Random random = new Random();
            int chainSize = 10;
            int randomInt = random.Next() % chainSize;
            string randomHash = string.Empty;
            for (int i = 0; i < 10; i++)
            {
                this.BlockChain.AddBlock(new TinyBlock(BlockChain.Chain.Count, DateTime.Now, new TinyTransaction(helper.RandomString(20, random)), BlockChain.GetLatestBlock().Hash));
                if (randomInt == i)
                    randomHash = this.BlockChain.GetLatestBlock().Hash; 
                this.BlockChain.GetLatestBlock().MineBlock(5);
            }
           
            //Review blockchain
            foreach (var chain in BlockChain.Chain)
            {
                string jsonx = Newtonsoft.Json.JsonConvert.SerializeObject(chain, Newtonsoft.Json.Formatting.Indented);
                Console.WriteLine(jsonx);
                Console.WriteLine("---------------------------------------------");
            }

            //select a random block
            Console.WriteLine("Find a block by hash indexer \t" + randomHash);
            var block = this.BlockChain[randomHash];
            Console.WriteLine(randomInt);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(block, Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine(json);
            Console.WriteLine("Validate block chain");
            Console.WriteLine(this.BlockChain.Validate().ToString());
            Console.WriteLine("Try tampering with block chain...");
            this.BlockChain.Chain[3].PreviousHash += "3";
            //Console.WriteLine("Is chain valid? " + this.BlockChain.Validate().ToString());
            //Console.ReadLine();

        }
    }
}
