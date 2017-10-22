using System;
using System.Collections.Generic;
using System.Linq;
using VoidChainLib.Objects;

namespace VoidChainLib.BlockChain
{
    public class VoidChain
    {

        byte[] hash1;
        byte[] hash2;

        string timestamp; //max length 255
        string pubkey = string.Empty; //max length 132
        uint nBits = 0;
        uint pubkeyScript_len = 0;
        public uint pubkey_len { get; set; }
        public string previousBlockHash { get; set; }
        uint scriptSig_len;
        public uint timestamp_len { get; set; }
        public Block Block { get; set; }
        public Transaction transaction
        { 
            get
            {
                return this.Block.Transaction;
            }
            set{
                this.Block.Transaction = value;
            }
        }

        List<byte> block_header;//{ get; set; }
        public List<byte> block_hash1 { get; set; }
		public List<byte> block_hash2 { get; set; }

        public VoidChain()
        {
            for (int i = 0; i < 65; i++)
            {
                this.pubkey += "a";
            }

			this.timestamp = DateTime.Now.ToString();
        }
        public VoidChain(string publicKey, string timeStamp)
		{
			this.pubkey = publicKey;
			this.timestamp = timeStamp;
		}

        public VoidChain(string publicKey, string timeStamp, uint nbit) : this(publicKey, timeStamp)
        {
            this.nBits = nbit;
        }

		public void Initialize()
		{
			if (pubkey.Length != 65)
				throw new VoidChainException("Invalid public key");
			if (timestamp.Length > 254 || timestamp.Length <= 0)
				throw new VoidChainException("Invalid timestamp");
			//initialize the genesis block and set the initial transaction values
			this.Block = new Block().Genesis();
			//transaction = this.Block.Transaction;

			pubkey_len = (uint)pubkey.Length >> 1;
			timestamp_len = (uint)timestamp.Length;
			scriptSig_len = timestamp_len;
			//Encode pubkey to binary and prepend pubkey size, then append the OP_CHECKSIG byte         
			//transaction->pubkeyScript[0] = 0x41; // A public key is 32 bytes X coordinate, 
			//32 bytes Y coordinate and one byte 0x04, so 65 bytes i.e 0x41 in Hex.
			transaction.pubkeyScript.Add((byte)0x41);
            //Goes after the pubkeyScript is filled out
            transaction.pubkeyScript.Add((byte)Block.OP_CHECKSIG);

            //pubkeyScript_len = transaction
            //transaction.pubkeyScript = new byte[]();

            //transaction.pubkeyScript[pubkey_len++] = Block.OP_CHECKSIG.ToByte();

            //transaction.pubkeyScript = malloc((pubkey_len+2)*sizeof(uint8_t));
            if (nBits <= 255)
            {
                transaction.scriptSig.Add(0x01);
            }
            else if (nBits <= 65535)
            {
                transaction.scriptSig.Add(0x02);
            }
            else if (nBits <= 16777215)
            { 
                transaction.scriptSig.Add(0x03);
            }
            else
            {
                transaction.scriptSig.Add(0x04);
            }
			transaction.scriptSig.AddRange(nBits.ToBytes()); //I think?

            // Important! In the Bitcoin code there is a statement 'CBigNum(4)' 
            // i've been wondering for a while what it is but			
            // seeing as alt-coins keep it the same, we'll do it here as well		
            // It should essentially mean PUSH 1 byte on the stack which in this case is 0x04 or just 4

            transaction.scriptSig.Add(0x01);
            transaction.scriptSig.Add(0x04);
            transaction.scriptSig.Add((byte)scriptSig_len);
            //this step may not be necessary
			uint serializedLen = 4// tx version
                            	+ 1   // number of inputs
                            	+ 32  // hash of previous output
                            	+ 4   // previous output's index
                            	+ 1   // 1 byte for the size of scriptSig
                            	+ scriptSig_len
                            	+ 4   // size of sequence
                            	+ 1   // number of outputs
                            	+ 8   // 8 bytes for coin value
                            	+ 1   // 1 byte to represent size of the pubkey Script
                            	+ pubkeyScript_len
                            	+ 4;   // 4 bytes for lock time

            // Now let's serialize the data
            transaction.serializedData.AddRange(transaction.version.ToBytes());
            transaction.serializedData.Add(transaction.numInputs);
            transaction.serializedData.AddRange(transaction.prevOutput);
            transaction.serializedData.AddRange(transaction.prevoutIndex.ToBytes());
            transaction.serializedData.AddRange(scriptSig_len.ToBytes());
            transaction.serializedData.AddRange(transaction.scriptSig);
            transaction.serializedData.AddRange(transaction.sequence.ToBytes());
            transaction.serializedData.Add(transaction.numOutputs);
            transaction.serializedData.AddRange(transaction.outValue.ToBytes());
            transaction.serializedData.AddRange(transaction.pubkeyScript);
            transaction.serializedData.AddRange(transaction.locktime.ToBytes());

            // Now that the data is serialized
            // we hash it with SHA256 and then hash that result to get merkle hash
            hash1 = transaction.serializedData.ToArray().GetSHA256();
            hash2 = hash1.GetSHA256();
            //I think?
            transaction.merkleHash = hash2.ToArray().ByteSwap().ToList();
            string merkleHash = transaction.merkleHash.ToArray().ToHex();
            string txScriptSig = transaction.scriptSig.ToArray().ToHex();
            string pubScriptSig = transaction.pubkeyScript.ToArray().ToHex();
            uint counter = 0; 
            uint start = DateTime.Now.ToUnixTime();
            block_header = new List<byte>();
            if(Block.GenerateBlock)
            {
                if(Block.UnixTime == 0)
                {
                    Block.UnixTime = DateTime.UtcNow.ToUnixTime();
                }

                uint blockversion = 1;

                List<byte> vers = blockversion.ToBytes().ToList();
                for (int i = vers.Count; i < 4; i++)
                {
                    block_header.Add(0);
                }
                block_header.AddRange(vers);
                int hcount = block_header.Count;
                for (int i = block_header.Count; i < hcount+32; i++)
                {
                    //previous block hash should go here I think
                    block_header.Add(0);
                }
                //byte swap again
                transaction.merkleHash = transaction.merkleHash.ToArray().ByteSwap().ToList();
                block_header.AddRange(transaction.merkleHash);
                List<byte> time = Block.UnixTime.ToBytes().ToList();
                for (int i = time.Count; i < 4; i++)
                {
                    time.Add(0);
                }
                block_header.AddRange(time);
                List<byte> nbit = nBits.ToBytes().ToList();
                for (int i = nbit.Count; i < 4; i++)
                {
                    nbit.Add(0);
                }
                block_header.AddRange(nbit);
                List<byte> nonce = Block.StartNonce.ToBytes().ToList();
                for (int i = nonce.Count; i < 4; i++)
                {
                    nonce.Add(0);
                }
                block_header.AddRange(nonce);
                //I'm not certain if these steps are necessary, but lets do it for now.
                uint pNonce = block_header.Skip(76).Take(4).ToList().ToUInt32();
                uint pUnixtime = block_header.Skip(68).Take(4).ToList().ToUInt32();

                while(true)
                {
                    block_hash1 = block_header.ToArray().GetSHA256().ToList();
                    block_hash2 = block_hash1.ToArray().GetSHA256().ToList();
                    uint check = block_hash2.Skip(28).Take(4).ToList().ToArray().ToUInt32();

                    if(check == 0)
                    {
                        var _bytes = block_hash2.ToArray().ByteSwap();
                        Block.BlockHash = _bytes.ToHex();
                        break;
                    }


					if (Block.StartNonce == uint.MaxValue)
					{
						Block.UnixTime++;
						var newTime = Block.UnixTime.ToBytes().ToArray();
						UpdateByteList(Block.UnixTime, 68, ref block_header);
						Block.StartNonce = 0; //Bug? 
                    }
                    else
                    {
						Block.StartNonce++;                       
                    }
                    uint curTime = DateTime.Now.ToUnixTime();
                    counter++;

                    if(curTime - start >= 1)
                    {
                        counter = 0;
                        start = curTime;
                    }
					
                    // insert new nonce into array
                    UpdateByteList(Block.StartNonce, 76, ref block_header);

                }
            }
        }

        public void UpdateByteList(uint newValue, int startIndex, ref List<byte> listToUpdate)
        {
            //List<byte> temp = listToUpdate;
            var newvalues = newValue.ToBytes().ToArray();
			for (int i = 0; i < newvalues.Length; i++)
			{
				listToUpdate[startIndex + i] = newvalues[i];
			}
        }

        public void Execute()
		{

		}
	}
}
