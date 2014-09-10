using System;
using System.Runtime.Serialization;

namespace OpenDeploymentManager.Client.Exceptions
{
    [Serializable]
    public class SecurityException : ServerException
    {
        public SecurityException()
        {
        }

        public SecurityException(string message)
            : base(message)
        {
        }

        public SecurityException(string message, int statusCode)
            : base(message, statusCode)
        {
        }

        public SecurityException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public SecurityException(string message, int statusCode, Exception innerException)
            : base(message, statusCode, innerException)
        {
        }

        protected SecurityException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}