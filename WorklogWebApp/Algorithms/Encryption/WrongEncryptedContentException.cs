using System;
using System.Runtime.Serialization;

namespace Algorithms.Encryption
{
    [Serializable]
    public class WrongEncryptedContentException : Exception
    {
        public WrongEncryptedContentException()
        {
        }

        public WrongEncryptedContentException(string message) : base(message)
        {
        }

        public WrongEncryptedContentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WrongEncryptedContentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}