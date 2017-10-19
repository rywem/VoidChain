using System;
using System.Numerics;
using VoidChainLibrary.Objects;
namespace VoidChainLibrary.Blockchain
{
    
    public class GenesisBlock
    {
        public const ulong COIN = 100000000;
		public const ulong CENT = 1000000;

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

    public struct Transaction
    {
        /* Hash of TX */
        public byte[][] merkleHash;// = new uint[32];
        /* Tx serialization before hashing */
        public byte[] serializedData; //uses ref, or * pointer in C
        /* Tx Version */
        public uint version;
        /* Inputs */
        public byte numInputs; //program assumes one input
        public byte[] prevOutput;
        public uint prevoutIndex;
        public byte[] scriptSig; //uses ref, * pointer in C
        public uint sequence;

        /* Output */
        public byte numOutputs; //program assumes one output
        public ulong outValue;
        public byte[] pubkeyScript;

        /* Final */
        public uint locktime; 

        public void InitTransaction(ulong COIN)
        {
			// Set some initial data that will remain constant throughout the program
			this.version = 1;
			this.numInputs = 1;
			this.numOutputs = 1;
			this.locktime = 0;
			this.prevoutIndex = 0xFFFFFFFF;
			this.sequence = 0xFFFFFFFF;
			this.outValue = 50 * COIN;

            prevOutput = new byte[32];
            //initialize a new byte array to zero, as there are no previous output values 
            for (int i = 0; i < prevOutput.Length; i++)
            {
                prevOutput[i] = 0;
            }

            merkleHash = new byte[32][];

        }
    }
}
