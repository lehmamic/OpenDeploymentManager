using System;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace OpenDeploymentManager.Client
{
    internal static class TaskExtensions
    {
        public static T WaitOn<T>(this Task<T> task)
        {
            AggregateException exception = null;
            try
            {
                task.Wait();

                if (task.IsFaulted)
                {
                    exception = task.Exception;
                }
            }
            catch (AggregateException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                ExceptionDispatchInfo.Capture(exception.InnerExceptions.First()).Throw();
            }

            return task.Result;
        }
    }
}