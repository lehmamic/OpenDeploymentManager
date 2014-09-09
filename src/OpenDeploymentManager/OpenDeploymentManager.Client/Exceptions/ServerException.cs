using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OpenDeploymentManager.Client.Exceptions
{
    [Serializable]
    public class ServerException : Exception
    {
        private readonly int statusCode;

        public ServerException()
        {
        }

        public ServerException(string message)
            : base(message)
        {
        }

        public ServerException(string message, int statusCode)
            : base(message)
        {
            this.statusCode = statusCode;
        }

        public ServerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ServerException(string message, int statusCode, Exception innerException)
            : base(message, innerException)
        {
            this.statusCode = statusCode;
        }

        protected ServerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            this.statusCode = info.GetInt32("StatusCode");
        }

        public int StatusCode
        {
            get
            {
                return this.statusCode;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            base.GetObjectData(info, context);
            info.AddValue("StatusCode", this.statusCode);
        }
    }
}
