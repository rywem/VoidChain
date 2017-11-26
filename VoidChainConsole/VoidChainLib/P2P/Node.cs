using System;
using System.Management;
namespace VoidChainLib.P2P
{
    public class Node
    {
        public string HashId { get; set; }
        public string PublicKey { get; set; }
        public bool Trustless { get; set; }
        public Node()
        {
        }

        public void Initialize()
        {
            
        }

        public void Identifier()
        {
            //// Win32_CPU will work too
            //var search = new ManagementObjectSearcher("SELECT * FROM Win32_baseboard");
            //var mobos = search.Get();

            //foreach (var m in mobos)
            //{
            //    var serial = m["SerialNumber"]; // ProcessorID if you use Win32_CPU
            //}
        }
    }
}
