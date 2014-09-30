using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;

namespace OpenDeploymentManager.Client.Http.Pipeline
{
    internal class MiddlewarePipeline
    {
        private readonly IMiddleware[] handlers;

        public MiddlewarePipeline(IEnumerable<IMiddleware> handlers)
        {
            if (handlers == null)
            {
                throw new ArgumentNullException("handlers");
            }

            this.handlers = handlers.ToArray();
        }

        [DebuggerStepThrough]
        public IHttpResponseContext Invoke(IHttpRequestContext context, IMiddleware end)
        {
            int handlerIndex = 0;

            var invoker = new NextInvoker(() =>
            {
                // process thou the call handler pipeline and at the end call the original next invoker.
                IMiddleware nextMiddleware = this.handlers.Count() > handlerIndex ? this.handlers[handlerIndex] : end;
                handlerIndex++;

                return nextMiddleware;
            });

            return invoker.Invoke(context);
        }
    }
}
