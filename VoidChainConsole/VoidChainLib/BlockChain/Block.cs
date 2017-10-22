using System;
namespace VoidChainLib.BlockChain
{
    public interface IBlock
    {
        ulong COIN { get; }
        ulong CENT { get; }

        uint OP_CHECKSIG { get; set; } //expressed as 0xAC
        bool GenerateBlock { get; set; }

        uint StartNonce { get; set; }
        uint UnixTime { get; set; }
        Transaction Transaction { get; set; }
        Block Genesis();
    }

    public class Block : IBlock
    {
        public ulong COIN { get; } = 100000000;
        public ulong CENT { get; } = 1000000;
        public uint OP_CHECKSIG { get; set; } = 172; //expressed as 0xAC
        public bool GenerateBlock { get; set; } = false;

        public uint StartNonce { get; set; } = 0;
        public uint UnixTime { get; set; } = 0;
        public Transaction Transaction { get; set; }

        public Block Genesis()
        {
            this.Transaction = new Transaction();
            Transaction.Initialize(COIN);
            return this;
        }
    }
}