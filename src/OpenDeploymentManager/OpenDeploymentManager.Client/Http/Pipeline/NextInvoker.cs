using System;
using System.Diagnostics;

namespace OpenDeploymentManager.Client.Http.Pipeline
{
    internal class NextInvoker : INextInvoker
    {
        private readonly Func<IMiddleware> _getNext;

        public NextInvoker(Func<IMiddleware> getNext)
        {
            if (getNext == null)
            {
                throw new ArgumentNullException("getNext");
            }

            this._getNext = getNext;
        }

        #region Implementation of INextInvoker
        [DebuggerStepThrough]
        public IHttpResponseContext Invoke(IHttpRequestContext context)
        {
            return this._getNext().Invoke(context, this);
        }
        #endregion
    }
}