using System;
using System.Runtime.Serialization;

namespace OpenDeploymentManager.Client.Exceptions
{
    [Serializable]
    public class NotFoundException : ServerException
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string message)
            : base(message, 404)
        {
        }

        public NotFoundException(string message, Exception innerException)
            : base(message, 404, innerException)
        {
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}