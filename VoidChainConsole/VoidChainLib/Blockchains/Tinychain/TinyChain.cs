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
                if (string.IsNullOrEmpty(hash))
                    return null;
                return Chain.FirstOrDefault(x => x.Hash.Equals(hash));
            }
        }

        #endregion

        public TinyBlock CreateGenesisBlock(int difficulty = 0 )
        {
            if (difficulty == 0)
            {
                return new TinyBlock(0, DateTime.Now, new TinyTransaction("0"), difficulty, new Objects.Helpers().GenerateDefaultString(32));
            }
            else
            {
                var block = new TinyBlock(0, DateTime.Now, new TinyTransaction("0"), difficulty, new Objects.Helpers().GenerateDefaultString(32));
                block.MineBlock();
                return block;
            }
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
                if (currentBlock.ValidateHash() == false)
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
            newBlock.MineBlock();
            this.Chain.Add(newBlock);
        }

        public int GetChainDifficulty()
        {
            return 4;
        }

        public TinyBlock GetLatestBlock()
        {
            return this.Chain.Last();
        }
    }
}
