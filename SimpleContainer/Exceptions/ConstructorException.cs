using System;
using System.Runtime.Serialization;

namespace SimpleContainer.Exceptions
{
    [Serializable]
    public class ConstructorException : Exception
    {
        public ConstructorException()
        {
        }

        public ConstructorException(string message) : base(message)
        {
        }

        public ConstructorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ConstructorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}