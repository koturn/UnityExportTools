using System;
using System.IO;
using UnityEngine;


namespace Koturn.ExportTools.Editor.Json
{
    /// <summary>
    /// Version number class.
    /// </summary>
    [Serializable]
    [System.Runtime.InteropServices.Guid("3d8df853-b4ce-61a4-c8b4-b6ac86e572b9")]
    internal sealed class PackageVersion
    {
        /// <summary>
        /// Version number string.
        /// </summary>
        public string Version => version;

#pragma warning disable IDE0044 // Add readonly modifier
        /// <summary>
        /// Backing field of <see cref="Version"/>.
        /// </summary>
        [SerializeField]
        private string version;
#pragma warning restore IDE0044 // Add readonly modifier

        /// <summary>
        /// Hidden ctor.
        /// </summary>
        [Obsolete("Should not be instanciated directly")]
        private PackageVersion()
        {
        }

        /// <summary>
        /// Create instance from specified json file.
        /// </summary>
        /// <param name="filePath">File path to json file.</param>
        /// <returns>Created <see cref="PackageVersion"/> instance.</returns>
        /// <exception cref="FileNotFoundException">Thrown when specified file is not found.</exception>
        /// <exception cref="ArgumentNullException">Thrown when instance is not created.</exception>
        public static PackageVersion LoadFromJsonFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found: " + filePath, filePath);
            }
            return JsonUtility.FromJson<PackageVersion>(File.ReadAllText(filePath));
        }
    }
}
