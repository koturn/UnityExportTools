using System;
using System.IO;
using UnityEngine;


namespace Koturn.ExportTools.Editor.Json
{
    /// <summary>
    /// Version number class.
    /// </summary>
    [Serializable]
    [System.Runtime.InteropServices.Guid("7fcf5b83-cc5a-e094-8959-d5e230905cad")]
    internal sealed class AssemblyDefinition
    {
        /// <summary>
        /// Assembly name.
        /// </summary>
        public string Name => name;

#pragma warning disable IDE0044 // Add readonly modifier
        /// <summary>
        /// Backing field of <see cref="Name"/>.
        /// </summary>
        [SerializeField]
        private string name;
#pragma warning restore IDE0044 // Add readonly modifier

        /// <summary>
        /// Hidden ctor.
        /// </summary>
        [Obsolete("Should not be instanciated directly")]
        private AssemblyDefinition()
        {
        }

        /// <summary>
        /// Create instance from specified json file.
        /// </summary>
        /// <param name="filePath">File path to json file.</param>
        /// <returns>Created <see cref="AssemblyDefinition"/> instance.</returns>
        /// <exception cref="FileNotFoundException">Thrown when specified file is not found.</exception>
        /// <exception cref="ArgumentNullException">Thrown when instance is not created.</exception>
        public static AssemblyDefinition LoadFromJsonFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found: " + filePath, filePath);
            }
            return JsonUtility.FromJson<AssemblyDefinition>(File.ReadAllText(filePath));
        }
    }
}
