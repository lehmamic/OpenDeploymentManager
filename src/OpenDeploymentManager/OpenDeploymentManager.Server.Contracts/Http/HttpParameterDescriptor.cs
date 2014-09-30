using System;
using System.Reflection;

namespace OpenDeploymentManager.Server.Contracts.Http
{
    public class HttpParameterDescriptor
    {
        private readonly ParameterInfo parameterInfo;
        private readonly object parameterValue;

        public HttpParameterDescriptor(ParameterInfo parameterInfo, object parameterValue)
        {
            if (parameterInfo == null)
            {
                throw new ArgumentNullException("parameterType");
            }

            if (parameterValue == null)
            {
                throw new ArgumentNullException("parameterValue");
            }

            this.parameterInfo = parameterInfo;
            this.parameterValue = parameterValue;
        }

        public string ParameterName
        {
            get
            {
                return this.parameterInfo.Name;
            }
        }

        public ParameterInfo ParameterInfo
        {
            get
            {
                return this.parameterInfo;
            }
        }

        public object ParameterValue
        {
            get
            {
                return this.parameterValue;
            }
        }
    }
}