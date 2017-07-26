using System;
using System.Runtime.Serialization;

namespace SimpleContainer.Exceptions
{
    [Serializable]
    public class ContainerCompletedException : Exception
    {
        public ContainerCompletedException()
        {
        }

        public ContainerCompletedException(string message) : base(message)
        {
        }

        public ContainerCompletedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ContainerCompletedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}