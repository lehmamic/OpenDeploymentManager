using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;

namespace OpenDeploymentManager.Deployment.Activities.Common
{
    public class ProcessInvoker : IDisposable
    {
        private readonly Process process = new Process();

        private bool disposed;

        public ProcessInvoker(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException("fileName");
            }

            string exeFullName = fileName.FindExePath();
            var executableFile = new FileInfo(exeFullName);
            if (!executableFile.Exists)
            {
                throw new FileNotFoundException("The executable file does not exist.", fileName);
            }

            this.process.StartInfo.FileName = executableFile.FullName;

            this.process.StartInfo.UseShellExecute = false;
            this.process.StartInfo.RedirectStandardOutput = true;
            this.process.StartInfo.RedirectStandardError = true;
        }

        ~ProcessInvoker()
        {
            this.Dispose(false);
        }

        public event DataReceivedEventHandler OutputDataReceived
        {
            add
            {
                this.process.OutputDataReceived += value;
            }

            remove
            {
                this.process.OutputDataReceived += value;
            }
        }

        public event DataReceivedEventHandler ErrorDataReceived
        {
            add
            {
                this.process.ErrorDataReceived += value;
            }

            remove
            {
                this.process.ErrorDataReceived += value;
            }
        }

        public string WorkingDirectory
        {
            get
            {
                return this.process.StartInfo.WorkingDirectory;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.process.StartInfo.WorkingDirectory = value;
                }
            }
        }

        public StringDictionary EnvironmentVariables
        {
            get
            {
                return this.process.StartInfo.EnvironmentVariables;
            }
        }

        public void Invoke(string arguments)
        {
            this.process.StartInfo.Arguments = arguments;
            this.process.Start();

            this.process.BeginErrorReadLine();
            this.process.BeginOutputReadLine();

            this.process.WaitForExit();
        }

        #region Implementation of IDisposable
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // cleanup managed resources
                    this.process.Dispose();
                }

                // cleanup unmanaged resources
                this.disposed = true;
            }
        }
        #endregion
    }
}
