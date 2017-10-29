using System;
using System.Collections.Generic;
using VoidChainLib.Objects;
namespace VoidChainLib.Blockchains.BasicChain
{
    public class BasicBlock
    {
        public string BlockFingerprint { get; set; }
        public string PreviousBlockFingerprint { get; set; }
        private string _MerkleHash;
        public string MerkleHash 
        { 
            get
            {
                if (_MerkleHash == null)
                   _MerkleHash = GetMerkleHash();
                return _MerkleHash;
            }
        }
        public List<BasicTransaction> Transactions { get; set; }

        public BasicBlock()
        {
            Transactions = new List<BasicTransaction>();
        }

        public BasicBlock(List<BasicTransaction> transactions)
        {
            this.Transactions = transactions;
        }
        public string GetMerkleHash()
        {
            if(Transactions != null && Transactions.Count > 0)
            {
                Helpers helper = new Helpers();
                return helper.MerkleHash(Transactions.GetFingerprints())[0];
            }
            throw new Exception("Transactions not set");
        }
    }
}
