using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace OpenDeploymentManager.Agent.Host.Logging
{
    public class LoggingCallHandlerAttribute : HandlerAttribute
    {
        private readonly int order;

        public LoggingCallHandlerAttribute()
            : this(0)
        {
        }

        public LoggingCallHandlerAttribute(int order)
        {
            this.order = order;
        }

        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new LoggingCallHandler() { Order = this.order };
        }
    }
}
