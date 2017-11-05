using System;
using System.Collections.Generic;
using System.Text;
using VoidChainLib.Objects;

namespace VoidChainLib.Blockchains.Tinychain
{
    //https://www.youtube.com/watch?v=zVqczFZr124
    public class TinyBlock
    {
        public int Index { get; set; }
        public string MerkleRoot 
        { 
            get
            {
                return GetMerkleRoot();
            }
        }
        private Random random { get; set; }
        public int Nonce { get; set; }
        public DateTime Timestamp { get; set; }
        public List<TinyTransaction> Transactions { get; set; }
        public string PreviousHash { get; set; }
        private string _Hash;
        public string Hash 
        { 
            get
            {
                if (string.IsNullOrEmpty(_Hash))
                {
                    SetBlockHash();
                }
                return this._Hash;
            }
         }
        public TinyBlock()
        {
        }
        public TinyBlock(int index, DateTime timestamp, List<TinyTransaction> transactions, string previousHash = null, int? nonce = 0)
        {
            this.random = new Random();
            if (nonce != null)
                this.Nonce = nonce.Value;
            else
                this.Nonce = random.Next();
            if (transactions == null || transactions.Count == 0)
                throw new ArgumentNullException("transactions parameter must be set to generate a block");
            this.Index = index;
            this.Timestamp = timestamp;
            this.Transactions = transactions;
            this.PreviousHash = previousHash;
        }
        public TinyBlock(int index, DateTime timestamp, TinyTransaction transaction, string previousHash = null) : this(index, timestamp, new List<TinyTransaction>(){transaction}, previousHash )
        {
            
        }
        private string GetMerkleRoot()
        {
            return new Helpers().MerkleRoot(Transactions.GetFingerprints());
        }
        public void SetBlockHash()
        {
            this._Hash = CalculateBlockHash();
        }
        public string CalculateBlockHash()
        {
            return new Helpers().ObjectsToBytes(Index, Timestamp, Transactions.GetFingerprint(), PreviousHash, Nonce).GetSHA256AsString();
        }

        public void MineBlock(int difficulty)
        {
            //make the hash of the block start with a certain number of zeros.
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < difficulty; i++)
            {
                builder.Append('0');
            }
            string match = builder.ToString();
            while(this.Hash.Substring(0, difficulty) != match)
            {
                this.Nonce++;
                if (this.Timestamp != DateTime.Now)
                {
                    this.Nonce = random.Next();
                    this.Timestamp = DateTime.Now;
                }
                this.SetBlockHash();
            }

            Console.WriteLine("Block mined " + this.Hash);
        }
    }
}
