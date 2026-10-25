using System;
using System.IO;
using System.IO.Compression;
using UnityEditor;
using UnityEngine;
using Koturn.ExportTools.Editor.Json;


namespace Koturn.ExportTools.Editor
{
    /// <summary>
    /// Export menu class.
    /// </summary>
    [System.Runtime.InteropServices.Guid("d05829ac-6f8a-9a94-8bbd-eb961bd8bf09")]
    public static class ExportMenu
    {
        /// <summary>
        /// Specifies the file path for the export configuration JSON file.
        /// </summary>
        private const string ExportConfigFilePath = "ExportConfig.json";

        /// <summary>
        /// Last export directory path.
        /// </summary>
        private static string _lastExportDirectoryPath = string.Empty;


        /// <summary>
        /// Read configuration file and export packages.
        /// </summary>
        [MenuItem("Assets/koturn/Export Packages", false, 9000)]
        private static void ExportPackages()
        {
            var exportDirPath = EditorUtility.SaveFolderPanel(
                "Select export directory",
                Directory.Exists(_lastExportDirectoryPath) ? _lastExportDirectoryPath : Application.dataPath,
                string.Empty);
            if (string.IsNullOrEmpty(exportDirPath))
            {
                return;
            }
            _lastExportDirectoryPath = exportDirPath;

            var jsonPath = AssetPathToAbsPath(ExportConfigFilePath);
            if (!File.Exists(jsonPath))
            {
                Debug.LogError("Configuration json file is not exists: " + jsonPath);
                return;
            }

            var exportConfig = ExportConfig.LoadFromJsonFile(jsonPath);
            foreach (var package in exportConfig.Packages)
            {
                var ver = GetVersionNumber(package);
                if (!string.IsNullOrEmpty(package.UnityPackageBaseName))
                {
                    ExportAsUnityPackage(
                        Path.Combine(exportDirPath, $"{package.UnityPackageBaseName}-{ver}.unitypackage"),
                        package.Assets,
                        package.DependAssets);
                }
                if (!string.IsNullOrEmpty(package.PackageName))
                {
                    ExportAsZipArchive(
                        Path.Combine(exportDirPath, $"{package.PackageName}-{ver}.zip"),
                        package.Assets);
                }
            }
        }

        /// <summary>
        /// Menu validation method for <see cref="ExportPackages"/>.
        /// </summary>
        /// <returns>True if <see cref="ExportPackages"/> works, otherwise false.</returns>
        [MenuItem("Assets/koturn/Export Packages", true)]
        private static bool ValidateExportPackages()
        {
            return File.Exists(ExportConfigFilePath);
        }

        /// <summary>
        /// Retrieves the version number from the export configuration or associated asset file.
        /// </summary>
        /// <param name="exportConfig">The export configuration containing version information.</param>
        /// <returns>Version number string. Returns "0.0.0" if no version is found.</returns>
        private static string GetVersionNumber(PackageInformation package)
        {
            var version = package.Version;
            if (!string.IsNullOrEmpty(version))
            {
                return version;
            }

            var assetPath = AssetDatabase.GUIDToAssetPath(package.VersionFrom);
            if (assetPath.Length != 0)
            {
                string ver = null;
                switch (Path.GetExtension(assetPath).ToLowerInvariant())
                {
                    case ".cs":
                        ver = VersionHelper.GetVersionFromAssemblyInfoCs(assetPath);
                        break;
                    case ".dll":
                        ver = VersionHelper.GetVersionFromDotNetDll(assetPath);
                        break;
                    case ".json":
                        ver = VersionHelper.GetVersionFromPackageJson(assetPath);
                        break;
                    case ".asmdef":
                        ver = VersionHelper.GetVersionFromAssemblyDefinition(assetPath);
                        break;
                }
                if (ver != null)
                {
                    return ver;
                }
            }

            Debug.LogWarning("Version number not found");

            return "0.0.0";
        }

        /// <summary>
        /// Export asset and dependent assets as unitypackage file.
        /// </summary>
        /// <param name="unityPackagePath">Unitypackage file path for export.</param>
        /// <param name="assetPath">Target asset path.</param>
        /// <param name="dependAssetPaths">Dependent asset paths.</param>
        private static void ExportAsUnityPackage(string unityPackagePath, AssetFileInfo[] assets, AssetFileInfo[] dependAssets)
        {
            if (!unityPackagePath.EndsWith(".unitypackage"))
            {
                unityPackagePath += ".unitypackage";
            }

            AssetDatabase.ExportPackage(
                ToAssetPaths(ConcatArray(assets, dependAssets)),
                unityPackagePath,
                ExportPackageOptions.Recurse);

            Debug.Log("Exported " + unityPackagePath);
        }

        /// <summary>
        /// Concatinate two arrays.
        /// </summary>
        /// <param name="array1">First array.</param>
        /// <param name="array2">Second array.</param>
        /// <returns>Concatinated array.</returns>
        private static T[] ConcatArray<T>(T[] array1, T[] array2)
        {
            var length1 = array1 == null ? 0 : array1.Length;
            var length2 = array2 == null ? 0 : array2.Length;

            var newArray = new T[length1 + length2];

            if (length1 > 0)
            {
                Array.Copy(array1, 0, newArray, 0, length1);
            }
            if (length2 > 0)
            {
                Array.Copy(array2, 0, newArray, length1, length2);
            }

            return newArray;
        }

        /// <summary>
        /// Get asset paths from <see cref="AssetFileInfo"/>.
        /// </summary>
        /// <param name="assets">Asset file path information.</param>
        /// <returns>Asset paths.</returns>
        private static string[] ToAssetPaths(AssetFileInfo[] assets)
        {
            var count = 0;
            foreach (var asset in assets)
            {
                if (asset.RelativePaths == null || asset.RelativePaths.Length == 0)
                {
                    count++;
                }
                else
                {
                    count += asset.RelativePaths.Length;
                }
            }
            var assetPaths = new string[count];

            var index = 0;
            foreach (var asset in assets)
            {
                var basePath = asset.BasePath;

                if (asset.RelativePaths == null || asset.RelativePaths.Length == 0)
                {
                    assetPaths[index] = basePath;
                    index++;
                    continue;
                }
                foreach (var relativePath in asset.RelativePaths)
                {
                    if (!basePath.EndsWith("/"))
                    {
                        basePath += "/";
                    }
                    assetPaths[index] = basePath + relativePath;
                    index++;
                }
            }

            return assetPaths;
        }

        /// <summary>
        /// Export asset as zip archive for VPM.
        /// </summary>
        /// <param name="zipFilePath">Zip file path for export.</param>
        /// <param name="assets">Asset file path infomation array.</param>
        private static void ExportAsZipArchive(string zipFilePath, AssetFileInfo[] assets)
        {
            File.Delete(zipFilePath);

            using (var zipArchive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
            {
                foreach (var asset in assets)
                {
                    var baseAssetPath = asset.BasePath;
                    if (!baseAssetPath.EndsWith("/"))
                    {
                        baseAssetPath += "/";
                    }
                    var absBasePath = AssetPathToAbsPath(baseAssetPath);

                    if (asset.RelativePaths == null || asset.RelativePaths.Length == 0)
                    {
                        foreach (var filePath in Directory.EnumerateFiles(absBasePath, "*", SearchOption.AllDirectories))
                        {
                            WriteToArchive(
                                zipArchive,
                                filePath,
                                filePath.Substring(absBasePath.Length).Replace("\\", "/"));
                        }
                    }
                    else
                    {
                        foreach (var relativePath in asset.RelativePaths)
                        {
                            var absPath = Path.Combine(absBasePath, relativePath);
                            var metaFilePath = absPath + ".meta";
                            if (File.Exists(absPath))
                            {
                                WriteToArchive(
                                    zipArchive,
                                    absPath,
                                    absPath.Substring(absBasePath.Length).Replace("\\", "/"));
                                WriteToArchive(
                                    zipArchive,
                                    metaFilePath,
                                    metaFilePath.Substring(absBasePath.Length).Replace("\\", "/"));
                            }
                            else if (Directory.Exists(absPath))
                            {
                                WriteToArchive(
                                    zipArchive,
                                    metaFilePath,
                                    metaFilePath.Substring(absBasePath.Length).Replace("\\", "/"));
                                foreach (var filePath in Directory.EnumerateFiles(absPath, "*", SearchOption.AllDirectories))
                                {
                                    WriteToArchive(
                                        zipArchive,
                                        filePath,
                                        filePath.Substring(absBasePath.Length).Replace("\\", "/"));
                                }
                            }
                            else
                            {
                                Debug.LogError("File or directory not found: " + absPath);
                            }
                        }
                    }
                }
            }

            Debug.Log("Exported " + zipFilePath);
        }

        /// <summary>
        /// Write file data as zip archibe.
        /// </summary>
        /// <param name="zipArchive">Zip archive to write.</param>
        /// <param name="filePath">File path.</param>
        /// <param name="entryName">Zip entry name.</param>
        private static void WriteToArchive(ZipArchive zipArchive, string filePath, string entryName)
        {
#if NET6_0_OR_GREATER
            const System.IO.Compression.CompressionLevel compLevel = System.IO.Compression.CompressionLevel.SmallestSize;
#else
            const System.IO.Compression.CompressionLevel compLevel = System.IO.Compression.CompressionLevel.Optimal;
#endif  // NET6_0_OR_GREATER
            var data = File.ReadAllBytes(filePath);
            var entry = zipArchive.CreateEntry(entryName, compLevel);
            using (var zs = entry.Open())
            {
                zs.Write(data, 0, data.Length);
            }
        }


        /// <summary>
        /// Convert from Assets path to Absolute path.
        /// </summary>
        /// <param name="assetPath">Target asset path.</param>
        /// <returns>Absolute path converte from <paramref name="assetPath"/>.</returns>
        private static string AssetPathToAbsPath(string assetPath)
        {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
            return assetPath.Replace("Assets", Application.dataPath).Replace("/", "\\");
#else
            return assetPath.Replace("Assets", Application.dataPath);
#endif  // UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
        }
    }
}
