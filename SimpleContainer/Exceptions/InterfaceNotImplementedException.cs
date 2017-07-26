using System;
using System.Runtime.Serialization;

namespace SimpleContainer.Exceptions
{
    [Serializable]
    public class InterfaceNotImplementedException : Exception
    {
        public InterfaceNotImplementedException()
        {
        }

        public InterfaceNotImplementedException(string message) : base(message)
        {
        }

        public InterfaceNotImplementedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InterfaceNotImplementedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}