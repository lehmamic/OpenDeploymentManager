using System;
using System.Runtime.Serialization;
using OpenDeploymentManager.Server.Contracts;

namespace OpenDeploymentManager.Client.Exceptions
{
    [Serializable]
    public class ValidationException : ServerException
    {
        private readonly ErrorResult errorResult;

        public ValidationException()
        {
            this.errorResult = new ErrorResult();
        }

        public ValidationException(string message)
            : base(message, 400)
        {
            this.errorResult = new ErrorResult();
        }

        public ValidationException(string message, ErrorResult errorResult)
            : base(message, 400)
        {
            if (errorResult == null)
            {
                throw new ArgumentNullException("errorResult");
            }

            this.errorResult = errorResult;
        }

        public ValidationException(string message, ErrorResult errorResult, Exception innerException)
            : base(message, 400, innerException)
        {
            if (errorResult == null)
            {
                throw new ArgumentNullException("errorResult");
            }

            this.errorResult = errorResult;
        }

        protected ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            this.errorResult = (ErrorResult)info.GetValue("ErrorResult", typeof(ErrorResult));
        }

        public ErrorResult ErrorResult
        {
            get
            {
                return this.errorResult;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            base.GetObjectData(info, context);
            info.AddValue("ErrorResult", this.errorResult);
        }
    }
}