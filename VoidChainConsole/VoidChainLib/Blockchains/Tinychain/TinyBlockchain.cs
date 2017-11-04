using System;
using System.Collections.Generic;

namespace VoidChainLib.Blockchains.Tinychain
{
    public class TinyBlockchain
    {
        
        public TinyBlockchain()
        {
            //initialize our block chain
        }


        public TinyBlock CreateGenesisBlock()
        {
            return new TinyBlock(0, DateTime.Now, new TinyTransaction("0"), new Objects.Helpers().GenerateDefaultString(32));
        }
    }
}
