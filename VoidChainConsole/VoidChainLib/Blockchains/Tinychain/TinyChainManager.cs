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
            int chainSize = 10;
            int randomInt = random.Next() % chainSize;
            string randomHash = string.Empty;
            for (int i = 0; i < 10; i++)
            {
                this.BlockChain.AddBlock(new TinyBlock(BlockChain.Chain.Count, DateTime.Now, new TinyTransaction(helper.RandomString(20, random)), BlockChain.GetLatestBlock().Hash));
                if (randomInt == i)
                    randomHash = this.BlockChain.GetLatestBlock().Hash;
                    
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
            Console.ReadLine();

        }
    }
}
