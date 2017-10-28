using System;
//using VoidChainLib.Objects.Interfaces;

namespace VoidChainLib.Cryptography
{
    public class PrivateKey : IKey
    {
        public byte[] PublicKey { get; set; }
        private byte[] _privateKey;

    }
    public interface IKey
    {
        byte[] PublicKey { get; set; }

        byte[] Encrypt(string message);
        string Decrypt(byte[] encryptedMessage);
    }
    public class Key
    {
        /// <summary>
        /// The recipient's private key, used for decrypting the message.
        /// </summary>
        public byte[] publicKey;
        /// <summary>
        /// The sender's private key, used for signing the message.
        /// </summary>
        public byte[] privateKey;

        public void Initialize()
        {
            using (ECDiffieHellmanCng alice = new ECDiffieHellmanCng())
            {
                alice.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
                alice.HashAlgorithm = CngAlgorithm.Sha256;
                publicKey = alice.PublicKey.ToByteArray();

            }
        }
    }
}