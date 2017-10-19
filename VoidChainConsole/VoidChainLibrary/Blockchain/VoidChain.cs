using System;
using VoidChainLibrary.Objects;

namespace VoidChainLibrary.Blockchain
{
	public class VoidChain
	{

		byte[] hash1 = new byte[32];
		byte[] hash2 = new byte[32];

		string timestamp; //max length 255
		string pubkey = string.Empty; //max length 132
		uint nBits = 0;
        public uint pubkey_len { get; set; }

        uint scriptSig_len;
        public uint timestamp_len { get; set; }
        public GenesisBlock Block { get; private set; }
		public Transaction transaction 
		{
			get { return Block.transaction; }
            set { Block.transaction = value; }
		}

		public VoidChain(string publicKey, string timeStamp)
		{
			this.pubkey = publicKey;
			this.timestamp = timeStamp;
            Initialize();
		}

		public void Initialize()
		{
			if (pubkey.Length != 65)
				throw new VoidChainException("Invalid public key");
			if (timestamp.Length > 254 || timestamp.Length <= 0)
				throw new VoidChainException("Invalid timestamp");
			//initialize the genesis block and set the initial transaction values
			this.Block = new GenesisBlock(true);
            pubkey_len = (uint)pubkey.Length >> 1;
            timestamp_len = (uint)timestamp.Length;
            scriptSig_len = timestamp_len;

            // Encode pubkey to binary and prepend pubkey size, then append the OP_CHECKSIG byte
            //transaction.pubkeyScript = new byte[]();

            //transaction.pubkeyScript[pubkey_len++] = Block.OP_CHECKSIG.ToByte();

            //transaction.pubkeyScript = malloc((pubkey_len+2)*sizeof(uint8_t));

        }
		public void Execute()
		{

		}
	}
}
