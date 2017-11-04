using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoidChainLib.Objects
{
    public class Helpers
    {
        
        public string MerkleRoot(List<string> hashes)
        {
            int count = hashes.Count;
            if (count == 1)
                return hashes[0];
            
            //If an odd number of strings, add a default string to the end
            if (count % 2 == 1)
                hashes.Add(hashes.Last());
            int cap = (int)(hashes.Count / 2);
            while (hashes.Count > cap)
            {
                if (hashes.Count > 1)
                {
                    //hash the first 2 elements
                    hashes.Add(MerkleHash(hashes[0], hashes[1]));
                    hashes.RemoveAt(0);
                    hashes.RemoveAt(0);
                }
            }
            return MerkleRoot(hashes);
        }

        public string MerkleHash(string hash0, string hash1)
        {
            return string.Concat(hash0, hash1).GetSHA256();
        }

        public string GenerateDefaultString(int characterCount)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < characterCount; i++)
            {
                builder.Append('0');
            }
            return builder.ToString();
        }

        public string RandomString(int length, Random random)
        {
            StringBuilder builder = new StringBuilder();
            int add = 0;
            if (random.Next() % 2 == 0)
                add = 65;
            else
                add = 97;
            for (int i = 0; i < length; i++)
            {
                builder.Append((char)(int)(random.Next() % 26)+ add);
            }
            return builder.ToString();
        }
        public byte[] ObjectsToBytes(params object[] objs)
        {
            List<byte> data = new List<byte>();
            foreach (object item in objs)
            {
                if (item != null)
                {
                    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf
                          = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    using (var ms = new System.IO.MemoryStream())
                    {
                        bf.Serialize(ms, item);
                        data.AddRange(ms.ToArray().ToList());
                    }
                }
            }
            return data.ToArray();
        }
    }
}
