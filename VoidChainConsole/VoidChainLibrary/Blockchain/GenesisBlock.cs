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
        public Transaction Tx; 

        private void ByteSwap(int length, ref byte[] buf)
        {
            int i;
            byte temp;

            for (i = 0; i < length; i++)
            {
                temp = buf[i];
                buf[i] = buf[length - i - 1];
                buf[length - i - 1] = temp;
            }
        }

    }

    public struct Transaction
    {
        /* Hash of TX */
        byte[] merkleHash;// = new uint[32];
        /* Tx serialization before hashing */
        byte[] serializedData; //uses ref, or * pointer in C
        /* Tx Version */
        uint version;
        /* Inputs */
        byte[] numInputs; //program assumes one input
        byte[] prevOutput;
        uint prevoutIndex;
        byte[] scriptSig; //uses ref, * pointer in C
        uint sequence;

        /* Output */
        byte[] numOuputs; //program assumes one output
        ulong outValue;
        byte[] pubkeyScript;

        /* Final */
        uint lockTime; 
    }
}
