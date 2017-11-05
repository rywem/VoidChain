using System;
using System.Collections.Generic;
using System.Linq;

namespace VoidChainLib.Blockchains.Tinychain
{
    public class TinyChain
    {

        public List<TinyBlock> Chain { get; set; }

        public TinyChain()
        {
            //initialize our block chain
            this.Chain = new List<TinyBlock>() { this.CreateGenesisBlock() };
        }
        public TinyChain(List<TinyBlock> blocks)
        {
            this.Chain = blocks;
        }

        #region indexers
        public TinyBlock this[string hash]
        {
            get
            {
                return Chain.FirstOrDefault(x => x.Hash.Equals(hash));
            }
        }

        #endregion

        public TinyBlock CreateGenesisBlock()
        {
            return new TinyBlock(0, DateTime.Now, new TinyTransaction("0"), new Objects.Helpers().GenerateDefaultString(32));
        }

        public bool Validate()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                var currentBlock = this.Chain[i];
                var prevBlock = this.Chain[i - 1];

                if(currentBlock.Hash != currentBlock.CalculateBlockHash())
                    return false;
                
                if (currentBlock.PreviousHash != prevBlock.Hash)
                    return false;
            }
            return true;
        }

        public void AddBlock(TinyBlock newBlock)
        {
            if (newBlock.PreviousHash == null)
            {
                newBlock.PreviousHash = GetLatestBlock().Hash;
                newBlock.SetBlockHash();
            }
            newBlock.MineBlock(GetChainDifficulty());
            this.Chain.Add(newBlock);
        }

        public int GetChainDifficulty()
        {
            return 5;
        }

        public TinyBlock GetLatestBlock()
        {
            return this.Chain.Last();
        }
    }
}
