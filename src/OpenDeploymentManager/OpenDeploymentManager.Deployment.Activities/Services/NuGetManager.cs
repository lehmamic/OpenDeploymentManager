using System;
using NuGet;

namespace OpenDeploymentManager.Deployment.Activities.Services
{
    internal class NuGetManager
    {
        private readonly IPackageRepositoryFactory packageRepositoryFactory;

        public NuGetManager()
            : this(PackageRepositoryFactory.Default)
        {
        }

        public NuGetManager(IPackageRepositoryFactory packageRepositoryFactory)
        {
            if (packageRepositoryFactory == null)
            {
                throw new ArgumentNullException("packageRepositoryFactory");
            }

            this.packageRepositoryFactory = packageRepositoryFactory;
        }

        #region Implementation of INuGetService
        public bool CanUpdatePackage(string packageSource, string localPath, string packageId, string packageVersion)
        {
            IPackageRepository repository = this.packageRepositoryFactory.CreateRepository(packageSource);

            IPackageManager packageManager = new PackageManager(repository, localPath);
            IPackage package = packageManager.SourceRepository.FindPackage(packageId, SemanticVersion.Parse(packageVersion));

            IPackage installedPackage = packageManager.SourceRepository.FindPackage(packageId);
            return installedPackage != null && package != null && installedPackage.Version < package.Version;
        }

        public string GetLocalPackagePath(string packageSource, string localPath, string packageId, string packageVersion)
        {
            IPackageRepository repository = this.packageRepositoryFactory.CreateRepository(packageSource);

            IPackageManager packageManager = new PackageManager(repository, localPath);
            IPackage package = packageManager.SourceRepository.FindPackage(packageId, SemanticVersion.Parse(packageVersion));

            return packageManager.PathResolver.GetInstallPath(package);
        }

        public void InstallPackage(string packageSource, string localPath, string packageId, string packageVersion)
        {
            IPackageRepository repository = this.packageRepositoryFactory.CreateRepository(packageSource);

            IPackageManager packageManager = new PackageManager(repository, localPath);
            IPackage package = packageManager.SourceRepository.FindPackage(packageId, SemanticVersion.Parse(packageVersion));

            packageManager.InstallPackage(packageId, SemanticVersion.Parse(packageVersion), true, true);

            packageManager.PathResolver.GetInstallPath(package);
        }

        public void UpdatePackage(string packageSource, string localPath, string packageId, string packageVersion)
        {
            IPackageRepository repository = this.packageRepositoryFactory.CreateRepository(packageSource);

            IPackageManager packageManager = new PackageManager(repository, localPath);
            packageManager.UpdatePackage(packageId, SemanticVersion.Parse(packageVersion) , true, true);
        }

        public void UninstallPackage(string packageSource, string localPath, string packageId, string packageVersion)
        {
            IPackageRepository repository = this.packageRepositoryFactory.CreateRepository(packageSource);

            IPackageManager packageManager = new PackageManager(repository, localPath);
            packageManager.UninstallPackage(packageId, SemanticVersion.Parse(packageVersion), true, true);
        }
        #endregion
    }
}
