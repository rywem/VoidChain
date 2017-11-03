using System;
namespace VoidChainLib.Blockchains.Tinychain
{
    //https://www.youtube.com/watch?v=zVqczFZr124
    public class TinyBlock
    {
        public int Index { get; set; }
        public DateTime Timestamp { get; set; }
        public object Data { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; } = null;
        public TinyBlock()
        {
        }
        public TinyBlock(int index, DateTime timestamp, object data, string previousHash = null)
        {
            this.Index = index;
            this.Timestamp = timestamp;
            this.Data = data;
            this.PreviousHash = previousHash;
        }

        public void CalculateHash()
        {
            
        }
    }
}
