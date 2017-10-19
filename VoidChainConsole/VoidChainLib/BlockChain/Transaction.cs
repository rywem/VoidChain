using System;
using System.Collections.Generic;

namespace VoidChainLib.BlockChain
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

        public Transaction()
        {
            this.merkleHash = new List<byte>(32);
            this.serializedData = new List<byte>();
            this.prevOutput = new List<byte>(32);
            this.scriptSig = new List<byte>();
            this.pubkeyScript = new List<byte>(65);
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

			//prevOutput = new List<byte>(32);
			//initialize a new byte array to zero, as there are no previous output values 
			for (int i = 0; i < prevOutput.Count; i++)
			{
				prevOutput[i] = 0;
			}
			//pubkeyScript = new List<byte>(65);
			//merkleHash = new List<byte>(32);
		}
	}
}
