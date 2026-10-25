using System;
using System.IO;
using UnityEngine;


namespace Koturn.ExportTools.Editor.Json
{
    /// <summary>
    /// Entire json data.
    /// </summary>
    [System.Runtime.InteropServices.Guid("3d9c83f9-ae16-3b14-a991-f440c1680ae8")]
    internal sealed class ExportConfig
    {
        /// <summary>
        /// Package infomation array.
        /// </summary>
        public PackageInformation[] Packages => packages;

#pragma warning disable IDE0044 // Add readonly modifier
        /// <summary>
        /// Backing field of <see cref="Packages"/>.
        /// </summary>
        [SerializeField]
        private PackageInformation[] packages;
#pragma warning restore IDE0044 // Add readonly modifier

        /// <summary>
        /// Hidden ctor.
        /// </summary>
        [Obsolete("Should not be instanciated directly")]
        private ExportConfig()
        {
        }

        /// <summary>
        /// Create instance from specified json file.
        /// </summary>
        /// <param name="filePath">File path to json file.</param>
        /// <returns>Created <see cref="ExportConfig"/> instance.</returns>
        /// <exception cref="FileNotFoundException">Thrown when specified file is not found.</exception>
        /// <exception cref="ArgumentNullException">Thrown when instance is not created.</exception>
        public static ExportConfig LoadFromJsonFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found: " + filePath, filePath);
            }
            return JsonUtility.FromJson<ExportConfig>(File.ReadAllText(filePath));
        }
    }
}
