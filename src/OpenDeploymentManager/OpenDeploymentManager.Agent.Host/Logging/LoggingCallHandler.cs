using System;
using System.Globalization;
using Microsoft.Practices.Unity.InterceptionExtension;
using NLog;
using OpenDeploymentManager.Agent.Host.Properties;

namespace OpenDeploymentManager.Agent.Host.Logging
{
    public class LoggingCallHandler : ICallHandler
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        #region Implementation of ICallHandler
        public int Order { get; set; }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            // Before invoking the method on the original target
            Log.Debug(Resources.LoggingCallHandler_InvokingMethod, input.MethodBase, DateTime.Now.ToLongTimeString());

            // Invoke the next handler in the chain
            var result = getNext().Invoke(input, getNext);

            // After invoking the method on the original target
            if (result.Exception != null)
            {
                string message = string.Format(CultureInfo.InvariantCulture, Resources.LoggingCallHandler_MethodThrewException, input.MethodBase, result.Exception.Message, DateTime.Now.ToLongTimeString());
                Log.Error(message, result.Exception);
            }
            else
            {
                Log.Debug(Resources.LoggingCallHandler_MethodReturned, input.MethodBase, result.ReturnValue, DateTime.Now.ToLongTimeString());
            }

            return result;
        }
        #endregion
    }
}