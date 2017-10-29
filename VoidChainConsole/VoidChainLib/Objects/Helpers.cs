using System;
using System.Collections.Generic;

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
            int hashCount = hashes.Count;
            if (hashes.Count == 1)
                return hashes;

            List<string> newHashes = new List<string>();

            for (int i = 0; i < hashes.Count; i = i+2)
            {
                if(i + 1 < hashCount)
                    newHashes.Add(MerkleHash(hashes[i], hashes[i+1]));
                else
                {
                    string substituteHash = "000000000000000";
                    newHashes.Add(MerkleHash(hashes[i], substituteHash));
                }
            }
            return MerkleHash(newHashes);
        }

        public string MerkleHash(string hash0, string hash1)
        {
            return (hash0 + hash1).GetSHA256();
        }
    }
}
