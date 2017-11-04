using System;
using System.Collections.Generic;
using System.Linq;

namespace VoidChainLib.Blockchains.Tinychain
{
    public class TinyBlockchain
    {

        public List<TinyBlock> Chain { get; set; }

        public TinyBlockchain()
        {
            //initialize our block chain
            this.Chain = new List<TinyBlock>() { this.CreateGenesisBlock() };
        }
        public TinyBlockchain(List<TinyBlock> blocks)
        {
            this.Chain = blocks;
        }


        public TinyBlock CreateGenesisBlock()
        {
            return new TinyBlock(0, DateTime.Now, new TinyTransaction("0"), new Objects.Helpers().GenerateDefaultString(32));
        }

        public void AddBlock(TinyBlock newBlock)
        {
            if (newBlock.PreviousHash == null)
            {
                newBlock.PreviousHash = GetLatestBlock().Hash;
                newBlock.CalculateBlockHash();
            }
            this.Chain.Add(newBlock);
        }
        public TinyBlock GetLatestBlock()
        {
            return this.Chain.Last();
        }
    }
}
