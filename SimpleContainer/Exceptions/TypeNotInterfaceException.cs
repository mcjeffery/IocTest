using System;
using System.Runtime.Serialization;

namespace SimpleContainer.Exceptions
{
    [Serializable]
    public class TypeNotInterfaceException : Exception
    {
        public TypeNotInterfaceException()
        {
        }

        public TypeNotInterfaceException(string message) : base(message)
        {
        }

        public TypeNotInterfaceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public TypeNotInterfaceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}