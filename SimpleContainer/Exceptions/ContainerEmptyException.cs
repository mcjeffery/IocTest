using System;
using System.Runtime.Serialization;

namespace SimpleContainer.Exceptions
{
    [Serializable]
    public class ContainerEmptyException : Exception
    {
        public ContainerEmptyException()
        {
        }

        public ContainerEmptyException(string message) : base(message)
        {
        }

        public ContainerEmptyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ContainerEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}