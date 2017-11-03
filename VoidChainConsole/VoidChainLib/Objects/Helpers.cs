using System;
using System.Collections.Generic;
using System.Linq;

namespace VoidChainLib.Objects
{
    public class Helpers
    {
        /// <summary>
        /// Gets the merkle hash.
        /// </summary>
        /// <returns>The merkle hash.</returns>
        /// <param name="hashes">Hashes, unsorted.</param>
        public string GetMerkleHash(List<string> hashes)
        {
            hashes.Sort(); //always sort your hashes
            return MerkleHash(hashes)[0];
        }
        public List<string> MerkleHash(List<string> hashes)
        {
            throw new NotImplementedException("Not working correctly");
            int count = hashes.Count;
            if (count == 1)
                return hashes;
            int cap = (int)(count / 2);
            if(count > 2)
                cap = cap + (count % 2);
            for (int i = 0; i < cap; i++)
            {
                if (i <= cap)
                {
                    hashes.Add(MerkleHash(hashes[i], hashes[i + 1]));
                    hashes.RemoveAt(i + 1);
                    hashes.RemoveAt(i);
                }   
                else
                {
                    string substituteHash = "000000000000000";
                    hashes.Add(MerkleHash(hashes[i], substituteHash));
                    hashes.RemoveAt(i);
                }
            }
            return MerkleHash(hashes);
        }

        public string MerkleHash(string hash0, string hash1)
        {
            return (hash0 + hash1).GetSHA256();
        }

        public byte[] HashObjects(params object[] objs)
        {
            List<byte> data = new List<byte>();
            foreach (object item in objs)
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf 
                      = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                using (var ms = new System.IO.MemoryStream())
                {
                    bf.Serialize(ms, item);
                    data.AddRange(ms.ToArray().ToList());
                }
            }
            return data.ToArray().GetSHA256();
        }
    }
}
