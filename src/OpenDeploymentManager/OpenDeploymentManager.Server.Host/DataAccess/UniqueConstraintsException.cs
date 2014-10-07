using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using OpenDeploymentManager.Common.Annotations;
using OpenDeploymentManager.Common.Diagnostics;

namespace OpenDeploymentManager.Server.Host.DataAccess
{
    [Serializable]
    public class UniqueConstraintException : Exception
    {
        private readonly PropertyInfo[] properties;

        public UniqueConstraintException()
        {
        }

        public UniqueConstraintException(string message)
            : base(message)
        {
        }

        public UniqueConstraintException(string message, [NotNull] IEnumerable<PropertyInfo> properties)
            : base(message)
        {
            this.properties = properties.ArgumentNotNull("properties").ToArray();
        }

        public UniqueConstraintException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public UniqueConstraintException(string message, [NotNull] IEnumerable<PropertyInfo> properties, Exception innerException)
            : base(message, innerException)
        {
            this.properties = properties.ArgumentNotNull("properties").ToArray();
        }

        protected UniqueConstraintException([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            info.ArgumentNotNull("info");
            this.properties = (PropertyInfo[])info.GetValue("Properties", typeof(PropertyInfo[]));
        }

        public PropertyInfo[] Properties
        {
            get
            {
                return this.properties ?? new PropertyInfo[0];
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.ArgumentNotNull("info");

            base.GetObjectData(info, context);
            info.AddValue("Properties", this.Properties);
        }
    }
}
