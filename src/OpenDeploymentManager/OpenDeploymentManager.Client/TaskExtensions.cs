using System.Threading.Tasks;

namespace OpenDeploymentManager.Client
{
    internal static class TaskExtensions
    {
        public static T WaitOn<T>(this Task<T> task)
        {
            task.Wait();
            return task.Result;
        }
    }
}