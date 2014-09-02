using System.Activities;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using OpenDeploymentManager.Deployment.Activities.Common;
using OpenDeploymentManager.Deployment.Activities.Services;

namespace OpenDeploymentManager.Deployment.Activities
{
    [Description("Activity to run npm with a command.")]
    public class Npm : BaseCodeActivity
    {
        private readonly object syncRoot = new object();

        [RequiredArgument]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("The working directory in which npm gets called.")]
        public InArgument<string> WorkingDirectory { get; set; }

        [RequiredArgument]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("The command which shall be execute with npm.")]
        public InArgument<string> Command { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("By default the node exe from the path variable gets used. However if node is not install or a specific node.exe shall be use its path can be defined with this property. Please provide the full path with file name.")]
        public InArgument<string> NodeExePath { get; set; }

        protected override void InternalExecute()
        {
            if (!string.IsNullOrEmpty(this.WorkingDirectory.Get(this.ActivityContext)) && !Directory.Exists(this.WorkingDirectory.Get(this.ActivityContext)))
            {
                this.LogBuildError(string.Format(CultureInfo.CurrentCulture, "The working directory does not exist: {0}", this.WorkingDirectory.Get(this.ActivityContext)));
                return;
            }

            string nodeExeFullPath = this.NodeExePath.Get(this.ActivityContext).GetDefaultIfEmpty(Constants.DefaultNodeExePath).FindExePath();
            DirectoryInfo workingDirectory = this.WorkingDirectory.Get(this.ActivityContext).ToDirectoryInfo();
            string npmCommand = this.Command.Get(this.ActivityContext);

            using (var npm = new NpmCli(nodeExeFullPath))
            {
                npm.OutputDataReceived += this.OnOutputDataReceived;
                npm.ErrorDataReceived += this.OnErrorDataReceived;

                npm.WorkingDirectory = workingDirectory.FullName;

                this.LogBuildMessage("Running npm command: " + npmCommand);
                npm.Run(npmCommand);
            }
        }

        private void OnErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.Data))
            {
                lock (this.syncRoot)
                {
                    var errorString = e.Data ?? string.Empty;
                    if (errorString.ToUpperInvariant().Contains("WARN"))
                    {
                        this.LogBuildWarning(e.Data);
                    }
                    else
                    {
                        this.LogBuildError(e.Data);
                    }
                }
            }
        }

        private void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.Data))
            {
                lock (this.syncRoot)
                {
                    this.LogBuildMessage(e.Data);
                }
            }
        }
    }
}