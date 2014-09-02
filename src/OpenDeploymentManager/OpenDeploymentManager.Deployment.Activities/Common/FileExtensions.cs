using System;
using System.Collections.Generic;
using System.IO;

namespace OpenDeploymentManager.Deployment.Activities.Common
{
    internal static class FileExtensions
    {
        public static string FindExePath(this string exe)
        {
            if (exe == null)
            {
                throw new ArgumentNullException("exe");
            }

            exe = Environment.ExpandEnvironmentVariables(exe);
            if (!File.Exists(exe))
            {
                if (Path.GetDirectoryName(exe) == string.Empty)
                {
                    foreach (string test in GetEnvironmentPaths())
                    {
                        string path = test.Trim();
                        if (!string.IsNullOrEmpty(path) && File.Exists(path = Path.Combine(path, exe)))
                        {
                            return Path.GetFullPath(path);
                        }
                    }
                }

                throw new FileNotFoundException(new FileNotFoundException().Message, exe);
            }

            return Path.GetFullPath(exe);
        }

        public static string GetDefaultIfEmpty(this string value, string defaultValue)
        {
            return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
        }

        public static DirectoryInfo ToDirectoryInfo(this string directory)
        {
            if (directory == null)
            {
                throw new ArgumentNullException("directory");
            }

            return new DirectoryInfo(directory);
        }

        public static FileInfo ToFileInfo(this string file)
        {
            if (file == null)
            {
                throw new ArgumentNullException("file");
            }

            return new FileInfo(file);
        }

        private static IEnumerable<string> GetEnvironmentPaths()
        {
            return (Environment.GetEnvironmentVariable("PATH") ?? string.Empty).Split(';');
        }
    }
}
