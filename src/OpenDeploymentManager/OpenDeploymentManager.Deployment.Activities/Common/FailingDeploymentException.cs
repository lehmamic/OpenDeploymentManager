using System;
using System.Runtime.Serialization;

namespace OpenDeploymentManager.Deployment.Activities.Common
{
    public class FailingDeploymentException : Exception
    {
        public FailingDeploymentException()
        {
        }

        public FailingDeploymentException(string message)
            : base(message)
        {
        }

        public FailingDeploymentException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected FailingDeploymentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}