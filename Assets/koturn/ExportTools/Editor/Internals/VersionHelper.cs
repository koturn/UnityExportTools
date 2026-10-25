using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using Koturn.ExportTools.Editor.Internals;
using Koturn.ExportTools.Editor.Json;


namespace Koturn.ExportTools.Editor
{
    /// <summary>
    /// Version number utility.
    /// </summary>
    [System.Runtime.InteropServices.Guid("750a7aa4-f183-9734-598b-fe77497d834a")]
    internal static class VersionHelper
    {
        /// <summary>
        /// Get version number from package.json.
        /// </summary>
        /// <param name="filePath">A file path to package.json.</param>
        /// <returns>Version number string.</returns>
        public static string GetVersionFromPackageJson(string filePath)
        {
            return PackageVersion.LoadFromJsonFile(filePath).Version;
        }

        /// <summary>
        /// Get version number from DLL.
        /// </summary>
        /// <param name="dllPath">A file path to DLL.</param>
        /// <returns>Version number string.</returns>
        public static string GetVersionFromDotNetDll(string dllPath)
        {
            var assemblyName = AssemblyName.GetAssemblyName(dllPath);

            //
            // Version number of AssemblyVersionAttribute.
            //
            if (assemblyName.Version.ToString() != "0.0.0.0")
            {
                return assemblyName.Version.ToString(3);
            }

            var fvi = FileVersionInfo.GetVersionInfo(dllPath);

            //
            // Version number of AssemblyFileVersionAttribute.
            //
            if (fvi.FileVersion != "0.0.0.0")
            {
                return ToSemVer(fvi.FileVersion);
            }

            //
            // Version number of AssemblyInformationalVersionAttribute.
            //
            if (fvi.ProductVersion != "0.0.0.0")
            {
                return ToSemVer(fvi.ProductVersion);
            }

            return null;
        }

        /// <summary>
        /// Get version number from the .NET DLL file associated with the specified assembly definition file.
        /// </summary>
        /// <param name="filePath">A file path to assembly definition file.</param>
        /// <returns>Version number string.</returns>
        public static string GetVersionFromAssemblyDefinition(string filePath)
        {
            var asmName = AssemblyDefinition.LoadFromJsonFile(filePath).Name;
            if (asmName == null)
            {
                return null;
            }
            var dllPath = "Library/ScriptAssemblies/" + asmName + ".dll";
            return GetVersionFromDotNetDll(dllPath);
        }

        /// <summary>
        /// Get version number from AssemblyInfo.cs.
        /// </summary>
        /// <param name="filePath">A file path to AssemblyInfo.cs.</param>
        /// <returns>Version number string.</returns>
        public static string GetVersionFromAssemblyInfoCs(string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 8192, FileOptions.SequentialScan))
            using (var sr = new StreamReader(fs))
            {
                string line;
                string asmVer = null;
                string asmFileVer = null;
                string asmInfoVer = null;
                while ((line = sr.ReadLine()) != null)
                {
                    var match = RegexProvider.AssemblyVersionRegex.Match(line);
                    if (match.Success)
                    {
                        asmVer = ToSemVer(match.Groups[1].Value.Trim());
                        break;
                    }

                    match = RegexProvider.AssemblyFileVersionRegex.Match(line);
                    if (match.Success)
                    {
                        asmFileVer = ToSemVer(match.Groups[1].Value.Trim());
                    }

                    match = RegexProvider.AssemblyInformationalVersionRegex.Match(line);
                    if (match.Success)
                    {
                        asmInfoVer = ToSemVer(match.Groups[1].Value.Trim());
                    }
                }

                if (asmVer != null)
                {
                    return asmVer;
                }
                else if (asmFileVer != null)
                {
                    return asmFileVer;
                }
                else if (asmInfoVer != null)
                {
                    return asmInfoVer;
                }

                return null;
            }
        }

        /// <summary>
        /// Convert to a version number that follows the <see href="https://semver.org/">SemVer</see> format.
        /// </summary>
        /// <param name="version">Version number string.</param>
        /// <returns></returns>
        private static string ToSemVer(string version)
        {
            var sb = new StringBuilder();
            var parts = version.Split('.');
            for (int i = 0; i < 3; i++)
            {
                if (sb.Length > 0)
                {
                    sb.Append('.');
                }
                sb.AppendFormat(parts.Length > i ? parts[i] : "0");
            }

            var ver = sb.ToString();
            if (!RegexProvider.SemVerRegex.IsMatch(ver))
            {
                throw new InvalidDataException($"Version number '{version}' is not a valid SemVer format.");
            }

            return ver;
        }
    }
}
