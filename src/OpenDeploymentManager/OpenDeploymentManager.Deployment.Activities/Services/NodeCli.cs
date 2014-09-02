using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using OpenDeploymentManager.Deployment.Activities.Common;

namespace OpenDeploymentManager.Deployment.Activities.Services
{
    public class NodeCli : IDisposable
    {
        private readonly ProcessInvoker nodeProcess;
        private readonly FileInfo nodeFileInfo;

        private bool disposed;

        public NodeCli(string nodeExeFile)
        {
            string fullNodeExePath = nodeExeFile.FindExePath();
            this.nodeFileInfo = new FileInfo(fullNodeExePath);
            this.nodeProcess = new ProcessInvoker(this.NodeExe.FullName);
        }

        ~ NodeCli()
        {
            this.Dispose(false);
        }

        public event DataReceivedEventHandler OutputDataReceived
        {
            add
            {
                this.NodeProcess.OutputDataReceived += value;
            }

            remove
            {
                this.NodeProcess.OutputDataReceived += value;
            }
        }

        public event DataReceivedEventHandler ErrorDataReceived
        {
            add
            {
                this.NodeProcess.ErrorDataReceived += value;
            }

            remove
            {
                this.NodeProcess.ErrorDataReceived += value;
            }
        }

        public string WorkingDirectory
        {
            get
            {
                return this.NodeProcess.WorkingDirectory;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.NodeProcess.WorkingDirectory = value;
                }
            }
        }

        public StringDictionary EnvironmentVariables
        {
            get
            {
                return this.NodeProcess.EnvironmentVariables;
            }
        }

        protected FileInfo NodeExe
        {
            get
            {
                return this.nodeFileInfo;
            }
        }

        protected ProcessInvoker NodeProcess
        {
            get
            {
                return this.nodeProcess;
            }
        }

        #region Implementation of IDisposable
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        public void Run(string javaScriptFile, string arguments)
        {
            if (javaScriptFile == null)
            {
                throw new ArgumentNullException("javaScriptFile");
            }

            if (!File.Exists(javaScriptFile))
            {
                string message = string.Format(CultureInfo.InvariantCulture, "The JavaScript file {0} does not exist.", javaScriptFile);
                throw new FileNotFoundException(message, javaScriptFile);
            }

            string npmCommand = BuildNodeCommand(javaScriptFile, arguments);
            this.NodeProcess.Invoke(npmCommand);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // cleanup managed resources
                    this.NodeProcess.Dispose();
                }

                // cleanup unmanaged resources
                this.disposed = true;
            }
        }

        private static string BuildNodeCommand(string javaScriptFile, string arguments)
        {
            return string.Format(CultureInfo.InvariantCulture, "\"{0}\" {1}", javaScriptFile, arguments ?? string.Empty);
        }
    }
}
