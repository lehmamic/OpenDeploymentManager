using System;
using System.Activities;
using System.ComponentModel;
using System.Globalization;
using OpenDeploymentManager.Deployment.Activities.Common;
using OpenDeploymentManager.Deployment.Activities.Services;

namespace OpenDeploymentManager.Deployment.Activities
{
    public class NuGet : BaseCodeActivity
    {
        [RequiredArgument]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("NuGet")]
        [Description("The nuget command to execute.")]
        public InArgument<NuGetCommand> Command { get; set; }

        [RequiredArgument]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("NuGet")]
        [Description("The source repository uri.")]
        public InArgument<string> SourceRepository { get; set; }

        [RequiredArgument]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("NuGet")]
        [Description("The package installation path.")]
        public InArgument<string> LocalPath { get; set; }

        [RequiredArgument]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("NuGet")]
        [Description("The package id.")]
        public InArgument<string> PackageId { get; set; }

        [RequiredArgument]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("NuGet")]
        [Description("The package version.")]
        public InArgument<string> PackageVersion { get; set; }

        [RequiredArgument]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("NuGet")]
        [Description("The package installation path.")]
        public OutArgument<string> PackageInstallationPath { get; set; }

        #region Overrides of BaseCodeActivity
        protected override void InternalExecute()
        {
            var command = this.Command.Get<NuGetCommand>(this.ActivityContext);
            var packageSource = this.SourceRepository.Get<string>(this.ActivityContext);
            var localPath = this.LocalPath.Get<string>(this.ActivityContext);
            var packageId = this.PackageId.Get<string>(this.ActivityContext);
            var packageVersion = this.PackageVersion.Get<string>(this.ActivityContext);

            var nuGet = new NuGetManager();
            switch (command)
            {
                case NuGetCommand.Install:
                    nuGet.InstallPackage(packageSource, localPath, packageId, packageVersion);
                    break;
                case NuGetCommand.Update:
                    nuGet.UpdatePackage(packageSource, localPath, packageId, packageVersion);
                    break;
                case NuGetCommand.Uninstall:
                    nuGet.UninstallPackage(packageSource, localPath, packageId, packageVersion);
                    break;
                default:
                    string message = string.Format(CultureInfo.InvariantCulture, "The NuGet command {0} has not been implemented yet.", command);
                    throw new NotImplementedException(message);
            }

            string packageInstallationPath = nuGet.GetLocalPackagePath(packageSource, localPath, packageId, packageVersion);
            this.PackageInstallationPath.Set(this.ActivityContext, packageInstallationPath);
        }
        #endregion
    }
}
