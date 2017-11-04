using System;
namespace VoidChainLib.Blockchains.Tinychain
{
    public class TinyTransaction
    {
        public string TransactionId { get; set; }

        public TinyTransaction()
        {

        }

        public TinyTransaction(string txid)
        {
            this.TransactionId = txid;
        }
    }
}
