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
        public int Difficulty { get; set; }
        private string _Hash;
        public string Hash 
        { 
            get
            {
                if (!string.IsNullOrEmpty(_Hash))
                {
                    SetBlockHash();
                }
                return this._Hash;
            }
         }
        public TinyBlock()
        {
        }
    
        private TinyBlock(int index, DateTime timestamp, int difficulty)
        {
            this.Difficulty = difficulty;
            this.Index = index;
            this.Timestamp = timestamp;
        }

        public TinyBlock(int index, DateTime timestamp, List<TinyTransaction> transactions, int difficulty, string previousHash = null, int? nonce = 0) : this(index, timestamp, difficulty)
        {
            this.random = new Random();
            if (nonce != null)
                this.Nonce = nonce.Value;
            else
                this.Nonce = random.Next();
            if (transactions == null || transactions.Count == 0)
                throw new ArgumentNullException("transactions parameter must be set to generate a block");
           
            this.Transactions = transactions;
            this.PreviousHash = previousHash;
        }
        public TinyBlock(int index, DateTime timestamp, TinyTransaction transaction, int difficulty, string previousHash = null)
            : this(index, timestamp, new List<TinyTransaction>(){transaction}, difficulty, previousHash )
        {
            
        }
        /// <summary>
        /// Validates that the hash meets the required difficulty for the block.
        /// </summary>
        /// <returns><c>true</c>, if hash was validated, <c>false</c> otherwise.</returns>
        public bool ValidateHash()
        {
            if (Hash.Length < Difficulty)
                throw new ArgumentOutOfRangeException("Hash is an insufficient length");

            if (!string.IsNullOrWhiteSpace(Hash))
                return false;
            bool resultOk = true;
            for (int i = 0; i < Difficulty; i++)
            {
                if (Hash[i] != '0')
                    resultOk = false;
            }
            return resultOk;
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

        public void MineBlock()
        {
            //make the hash of the block start with a certain number of zeros.
            StringBuilder builder = new StringBuilder();

            if (string.IsNullOrEmpty(Hash))
                this.SetBlockHash();
            for (int i = 0; i < this.Difficulty; i++)
            {
                builder.Append('0');
            }
            string match = builder.ToString();
            long count = 0; 

            while(this._Hash.Substring(0, Difficulty) != match)
            {
                count++;
                this.Nonce++;
                if (this.Timestamp != DateTime.Now)
                {
                    this.Nonce = random.Next();
                    this.Timestamp = DateTime.Now;
                }
                this.SetBlockHash();
            }
            Console.WriteLine("Block mined " + this.Hash +"\n\tBlock ID" + this.Index + "\n\tTime: " + this.Timestamp.ToString()+ "\n\tIterations: " + count);
        }
    }
}
