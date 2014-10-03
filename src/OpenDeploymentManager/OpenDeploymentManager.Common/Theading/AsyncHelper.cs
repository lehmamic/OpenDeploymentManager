using System.Threading.Tasks;

namespace OpenDeploymentManager.Common.Theading
{
    public static class AsyncHelper
    {
        public static Task Void
        {
            get
            {
                return Task.FromResult(new AsyncVoid());
            }
        }
    }
}