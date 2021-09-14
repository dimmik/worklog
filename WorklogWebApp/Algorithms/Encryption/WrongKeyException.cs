using System;
using System.Runtime.Serialization;

namespace Algorithms.Encryption
{
    [Serializable]
    internal class WrongKeyException : Exception
    {
        public WrongKeyException()
        {
        }

        public WrongKeyException(string message) : base(message)
        {
        }

        public WrongKeyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WrongKeyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}