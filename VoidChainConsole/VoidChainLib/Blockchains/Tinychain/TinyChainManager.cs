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
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(chain, Newtonsoft.Json.Formatting.Indented);
                Console.WriteLine(json);
                Console.WriteLine("---------------------------------------------");
            }
        }
    }
}
