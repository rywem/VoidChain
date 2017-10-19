using System;
using System.Numerics;
using VoidChainLibrary.Objects;
namespace VoidChainLibrary.Blockchain
{

   
    public class GenesisBlock
    {
        public ulong COIN { get; } = 100000000;
        public ulong CENT { get; } = 1000000;

        public uint OP_CHECKSIG { get; set; } = 172; //expressed as 0xAC
        public bool GenerateBlock { get; set; } = false;

        public uint StartNonce { get; set; } = 0;
        public uint UnixTime { get; set; } = 0;
        public Transaction transaction { get; set; }

        public GenesisBlock(bool initialize = false)
        {
            if(initialize == true)
                transaction.InitTransaction(COIN);
        }

    }

  
}
