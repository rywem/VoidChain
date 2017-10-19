using System;
namespace VoidChainLibrary.Objects
{
    public class VoidChainException : Exception
    {
        public VoidChainException() : base()
        {
        }

        public VoidChainException(string ErrorMessage) : base(ErrorMessage)
        {
            
        }

        public VoidChainException(string ErrorMessage, Exception InnerException) : base(ErrorMessage, InnerException)
        {
            
        }
    }
}
