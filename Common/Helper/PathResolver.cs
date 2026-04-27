using System;
using System.IO;

namespace Common
{
    public static class PathResolver
    {
        public static string GetAbsolutePath(string relativeOrAbsolutePath)
        {
            if (string.IsNullOrWhiteSpace(relativeOrAbsolutePath))
                return string.Empty;

            if (Path.IsPathRooted(relativeOrAbsolutePath))
                return relativeOrAbsolutePath;

            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Replace(@"bin\Debug\", "").Replace(@"bin\Release\", "").Replace(@"Sophon.UI\", ""), relativeOrAbsolutePath);
        }
    }
}