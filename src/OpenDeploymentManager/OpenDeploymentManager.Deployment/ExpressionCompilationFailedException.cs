using System;
using System.Runtime.Serialization;

namespace OpenDeploymentManager.Deployment
{
    public class ExpressionCompilationFailedException : Exception
    {
        public ExpressionCompilationFailedException(string message)
            : base(message)
        {
        }

        public ExpressionCompilationFailedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ExpressionCompilationFailedException()
        {
        }

        protected ExpressionCompilationFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}