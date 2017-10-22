using System;
using System.Collections.Generic;
using System.Linq;
using VoidChainLib.Objects;
namespace VoidChainConsole
{
    class MainClass
    {
        public static void Main(string[] args)
        {

            uint ten = 10;
            Console.WriteLine(ten.ToSpecial());

            uint k = 16777315;
            //byte[] bb = k as byte[];
            //byte[] b = k.;
            List<byte> x = k.ToBytes().ToList();
            //Console.WriteLine(b);
            foreach (var item in x)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine(x.ToArray().ToUInt32());
            VoidChainLib.BlockChain.Block block = new VoidChainLib.BlockChain.Block();
            var gen = block.Genesis();
           // Console.WriteLine(hex);
        }
    }
}
