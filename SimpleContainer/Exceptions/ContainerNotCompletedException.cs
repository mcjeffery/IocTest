using System;
using System.Runtime.Serialization;

namespace SimpleContainer.Exceptions
{
    [Serializable]
    public class ContainerNotCompletedException : Exception
    {
        public ContainerNotCompletedException()
        {
        }

        public ContainerNotCompletedException(string message) : base(message)
        {
        }

        public ContainerNotCompletedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ContainerNotCompletedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}