using System;
using System.Collections.Generic;

namespace VoidChainLib.Blockchains.Voidchain
{
	public class Transaction
	{
		/* Hash of TX */
		public List<byte> merkleHash { get; set; }
		/* Tx serialization before hashing */
		public List<byte> serializedData { get; set; } //uses ref, or * pointer in C
		/* Tx Version */
		public uint version { get; set; }
		/* Inputs */
		public byte numInputs { get; set; } //program assumes one input
		public List<byte> prevOutput { get; set; }
		public uint prevoutIndex { get; set; }
		public List<byte> scriptSig { get; set; } //uses ref, * pointer in C
		public uint sequence { get; set; }

		/* Output */
		public byte numOutputs { get; set; } //program assumes one output
		public ulong outValue { get; set; }        
		public List<byte> pubkeyScript { get; set; }

		/* Final */
		public uint locktime;

        const int DEFAULT_HASH_LENGTH = 32;
        const int DEFAULT_PREV_OUTPUT_LENGTH = 32;
        const int DEFAULT_PUBKEYSCRIPT_LENGTH = 65;
        public Transaction()
        {
            if(this.merkleHash == null)
                this.merkleHash = new List<byte>(DEFAULT_HASH_LENGTH);
            if(this.serializedData == null)
                this.serializedData = new List<byte>();
            if(this.prevOutput == null)    
                this.prevOutput = new List<byte>(DEFAULT_PREV_OUTPUT_LENGTH);
            if(this.scriptSig == null)
                this.scriptSig = new List<byte>();
            if(this.pubkeyScript == null)
               this.pubkeyScript = new List<byte>(DEFAULT_PUBKEYSCRIPT_LENGTH);
        }

        public Transaction(List<byte> previousOutput)
        {
            this.prevOutput = previousOutput;
        }

		public void Initialize(ulong COIN)
		{
			// Set some initial data that will remain constant throughout the program
			this.version = 1;
			this.numInputs = 1;
			this.numOutputs = 1;
			this.locktime = 0;
			this.prevoutIndex = 0xFFFFFFFF;
			this.sequence = 0xFFFFFFFF;
			this.outValue = 50 * COIN;
            //initialize a new byte array to zero, as there are no previous output values 
		}
	}
}
