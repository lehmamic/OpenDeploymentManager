using System.Activities;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using OpenDeploymentManager.Deployment.Activities.Common;
using OpenDeploymentManager.Deployment.Activities.Services;

namespace OpenDeploymentManager.Deployment.Activities
{
    public class NodeJs : BaseCodeActivity
    {
        private readonly object syncRoot = new object();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("By default the node exe from the path variable gets used. However if node is not install or a specific node.exe shall be use its path can be defined with this property. Please provide the full path with file name.")]
        public InArgument<string> NodeExePath { get; set; }

        [RequiredArgument]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("The working directory for node.js.")]
        public InArgument<string> WorkingDirectory { get; set; }

        [RequiredArgument]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("The JavaScript file which shall be executed by node.js.")]
        public InArgument<string> JavaScriptFile { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("The parameters passed to the java script application.")]
        public InArgument<string> ApplicationParameters { get; set; }

        #region Overrides of BaseCodeActivity
        protected override void InternalExecute()
        {
            if (!string.IsNullOrEmpty(this.WorkingDirectory.Get(this.ActivityContext)) && !Directory.Exists(this.WorkingDirectory.Get(this.ActivityContext)))
            {
                this.LogBuildError(string.Format(CultureInfo.CurrentCulture, "The working directory does not exist: {0}", this.WorkingDirectory.Get(this.ActivityContext)));
                return;
            }

            if (!string.IsNullOrEmpty(this.JavaScriptFile.Get(this.ActivityContext)) && !File.Exists(this.JavaScriptFile.Get(this.ActivityContext)))
            {
                this.LogBuildError(string.Format(CultureInfo.CurrentCulture, "The java script file does not exist: {0}", this.JavaScriptFile.Get(this.ActivityContext)));
                return;
            }

            string nodeExeFullPath = this.NodeExePath.Get(this.ActivityContext).GetDefaultIfEmpty(Constants.DefaultNodeExePath).FindExePath();
            DirectoryInfo workingDirectory = this.WorkingDirectory.Get(this.ActivityContext).ToDirectoryInfo();
            FileInfo javaScriptFile = this.JavaScriptFile.Get(this.ActivityContext).ToFileInfo();
            string applicationParameters = this.ApplicationParameters.Get(this.ActivityContext);

            using (var node = new NodeCli(nodeExeFullPath))
            {
                node.OutputDataReceived += this.OnOutputDataReceived;
                node.ErrorDataReceived += this.OnErrorDataReceived;

                node.WorkingDirectory = workingDirectory.FullName;

                this.LogBuildMessage("Executing Node.js with JavaScript file: {0}, {1}.", javaScriptFile, applicationParameters ?? string.Empty);
                node.Run(javaScriptFile.FullName, applicationParameters);
            }
        }
        #endregion


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
