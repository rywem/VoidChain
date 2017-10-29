using System;
namespace VoidChainLib.Blockchains.BasicChain
{
    public class BasicTransaction
    {

        //http://ecomunsing.com/build-your-own-blockchain

        public decimal Amount { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string Signature { get; set; } 
    }
}
