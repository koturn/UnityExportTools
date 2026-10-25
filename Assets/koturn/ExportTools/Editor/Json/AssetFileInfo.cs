using System;
using UnityEngine;


namespace Koturn.ExportTools.Editor.Json
{
    /// <summary>
    /// Asset file path information.
    /// </summary>
    [Serializable]
    [System.Runtime.InteropServices.Guid("0cfec46d-e0b0-a644-f8c4-da5c1e20d64c")]
    internal sealed class AssetFileInfo
    {
        /// <summary>
        /// Base asset path.
        /// </summary>
        public string BasePath => basePath;
        /// <summary>
        /// Relative asset path.
        /// </summary>
        public string[] RelativePaths => relativePaths;

#pragma warning disable IDE0044 // Add readonly modifier
        /// <summary>
        /// Backing field of <see cref="BasePath"/>.
        /// </summary>
        [SerializeField]
        private string basePath;
        /// <summary>
        /// Backing field of <see cref="RelativePaths"/>.
        /// </summary>
        [SerializeField]
        private string[] relativePaths;
#pragma warning restore IDE0044 // Add readonly modifier

        /// <summary>
        /// Hidden ctor.
        /// </summary>
        [Obsolete("Should not be instanciated directly")]
        private AssetFileInfo()
        {
        }
    }
}
