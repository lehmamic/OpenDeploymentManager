using System.IO;

namespace OpenDeploymentManager.Deployment.Activities.Services
{
    public class NpmCli : NodeCli
    {
        private readonly FileInfo npmModuleFile;

        public NpmCli(string nodeExeFile)
            : base(nodeExeFile)
        {
            string nodeModuleFilePath = Path.Combine(this.NodeExe.DirectoryName ?? string.Empty, @"node_modules\npm\bin\npm-cli.js");
            this.npmModuleFile = new FileInfo(nodeModuleFilePath);
            if (!this.npmModuleFile.Exists)
            {
                throw new FileNotFoundException("Could not find the npm module file.", nodeModuleFilePath);
            }
        }

        public void Run(string command)
        {
            this.Run(this.npmModuleFile.FullName, command);
        }
    }
}