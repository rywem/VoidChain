using System;
namespace VoidChainLib.Objects.Interfaces
{
    public interface IKey
    {
        byte[] PublicKey { get; set; }

        byte[] Encrypt(string message);
        string Decrypt(byte[] encryptedMessage);
    }
}
