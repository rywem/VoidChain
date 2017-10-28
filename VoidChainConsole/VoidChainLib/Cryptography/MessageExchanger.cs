﻿using System;
using System.Text;
using System.Security.Cryptography;
namespace VoidChainLib.Cryptography
{
    public class MessageExchanger
    {
        public void Run()
        {
            
        }
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

    public class Send
    {
        public byte[] alicePublicKey;
      
        private void Initialize()
        {
            using(ECDiffieHellmanCng alice = new ECDiffieHellmanCng())
            {
                //var privateKey = alice.Key.Export(CngKeyBlobFormat.Pkcs8PrivateBlob);
                alice.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
                alice.HashAlgorithm = CngAlgorithm.Sha256;
                alicePublicKey = alice.PublicKey.ToByteArray();
                Receive receive = new Receive();
                CngKey k = CngKey.Import(receive.publicKey, CngKeyBlobFormat.EccPublicBlob);
                byte[] aliceKey = alice.DeriveKeyMaterial(CngKey.Import(receive.publicKey, CngKeyBlobFormat.EccPublicBlob));
                byte[] encryptedMessage = null;
                byte[] initializationVector = null;
                Send(aliceKey, "My secret message!", out encryptedMessage, out initializationVector);
                Console.WriteLine(receive.DecryptMessage(encryptedMessage, initializationVector));
            }
        }

        public void SendMessage(byte[] privKey, string secretMessage, out byte[] encryptedMessage, out byte[] initializationVector)
        {
            using (Aes aes = new AesCryptoServiceProvider())
            {
                aes.Key = privKey;
                initializationVector = aes.iv;

                using(MemoryStream ciphertext = new MemoryStream())
                {
                    using(CryptoStream cs = new CryptoStream(ciphertext, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        byte[] plainTextMessage = Encoding.UTF8.GetBytes(secretMessage);
                        cs.Write(plainTextMessage, 0, plainTextMessage.Length);
                        cs.Close();
                        encryptedMessage = ciphertext.ToArray();
                    }
                }
            }
        }
    }


    public class Receive
    {
        public byte[] publicKey;
        //private byte[] key;
        public Receive()
        {
            using(ECDiffieHellmanCng pub = new ECDiffieHellmanCng())
            {
                pub.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
                pub.HashAlgorithm = CngAlgorithm.Sha256;
                publicKey = pub.PublicKey.ToByteArray();
                pubKey = pub.DeriveKeyMaterial(CngKey.Import(PrivateManager.alicePublicKey, CngKeyBlobFormat.EccPublicBlob));
            }
        }

        public string DecryptMessage(byte[] encryptedMessage, byte[] initializationVector)
        {
            string result;
            using(Aes aes = new AesCryptoServiceProvider())
            {
                aes.Key = key;
                aes.IV = initializationVector; 
                using(MemoryStream plaintext = new MemoryStream())
                {
                    using(CryptoStream cs = new CryptoStream(plaintext, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(encryptedMessage, 0, encryptedMessage.Length);
                        cs.Close();
                        result = Encoding.UTF8.GetString(plaintext.ToArray());
                    }
                }
            }
            return result;
        }
    }
}
