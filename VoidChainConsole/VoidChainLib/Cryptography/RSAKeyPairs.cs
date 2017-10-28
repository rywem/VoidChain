using System;
using System.Security.Cryptography;//.Rijndael
namespace VoidChainLib.Cryptography
{
    public class RSAKeyPair
    {
        public string publicKey { get; set; }
        public string privateKey { get; set; }

		//https://docs.microsoft.com/en-us/dotnet/standard/security/walkthrough-creating-a-cryptographic-application
		private void Encrypt(byte[] _bytes)
        {
            RijndaelManaged rj = new RijndaelManaged();
        }
    }
}
