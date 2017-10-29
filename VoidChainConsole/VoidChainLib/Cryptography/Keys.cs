using System;
using System.Security.Cryptography;
//using VoidChainLib.Objects.Interfaces;

namespace VoidChainLib.Cryptography
{
    public class PrivateKey : IKey
    {
        public byte[] PublicKey { get; set; }
        private byte[] _privateKey;

        public void Generate()
        {
            using (ECDiffieHellmanCng key = new ECDiffieHellmanCng())
            {
                //var privateKey = alice.Key.Export(CngKeyBlobFormat.Pkcs8PrivateBlob);
                key.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
                key.HashAlgorithm = CngAlgorithm.Sha256;
                PublicKey = key.PublicKey.ToByteArray();
            }
        }
    }

    public class SecureCommunication : ISecureCommunication
    {
        public byte[] InitializationVector { get; set; }
        public IKey key { get; set; }
        public string UnsecureMessage { get; set; }
        public byte[] EncryptedMessage { get; set; }

        public void Decrypt()
        {
            throw new NotImplementedException();
        }

        public void Encrypt(string Message)
        {
            throw new NotImplementedException();
        }
    }
    public interface IKey
    {
        byte[] PublicKey { get; set; }
        void Generate();
    }

    public interface ISecureCommunication
    {
        byte[] InitializationVector { get; set; }
        IKey key { get; set; }
        string UnsecureMessage { get; set; }
        byte[] EncryptedMessage { get; set; }

        void Encrypt(string Message);
        void Decrypt();
    }
}