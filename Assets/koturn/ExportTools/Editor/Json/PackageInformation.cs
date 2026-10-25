using System;
using UnityEngine;


namespace Koturn.ExportTools.Editor.Json
{
    /// <summary>
    /// Package information.
    /// </summary>
    [Serializable]
    [System.Runtime.InteropServices.Guid("95cfa36a-709f-fd94-eaec-b2cf557bdfb1")]
    internal sealed class PackageInformation
    {
        /// <summary>
        /// Get version number.
        /// </summary>
        public string Version => version;
        /// <summary>
        /// Get version source.
        /// </summary>
        public string VersionFrom => versionFrom;
        /// <summary>
        /// Unity package name for export.
        /// </summary>
        public string UnityPackageBaseName => unityPackageBaseName;
        /// <summary>
        /// VPM name.
        /// </summary>
        public string PackageName => packageName;
        /// <summary>
        /// Asset path array.
        /// </summary>
        public AssetFileInfo[] Assets => assets;
        /// <summary>
        /// Asset path array.
        /// </summary>
        public AssetFileInfo[] DependAssets => dependAssets;

#pragma warning disable IDE0044 // Add readonly modifier
        /// <summary>
        /// Backing field of <see cref="Version"/>.
        /// </summary>
        [SerializeField]
        private string version;
        /// <summary>
        /// Version number source.
        /// </summary>
        [SerializeField]
        private string versionFrom;
        /// <summary>
        /// Backing field of <see cref="UnityPackageBaseName"/>.
        /// </summary>
        [SerializeField]
        private string unityPackageBaseName;
        /// <summary>
        /// Backing field of <see cref="PackageName"/>.
        /// </summary>
        [SerializeField]
        private string packageName;
        /// <summary>
        /// Backing field of <see cref="Assets"/>.
        /// </summary>
        [SerializeField]
        private AssetFileInfo[] assets;
        /// <summary>
        /// Backing field of <see cref="DependAssets"/>.
        /// </summary>
        [SerializeField]
        private AssetFileInfo[] dependAssets;
#pragma warning restore IDE0044 // Add readonly modifier

        /// <summary>
        /// Hidden ctor.
        /// </summary>
        [Obsolete("Should not be instanciated directly")]
        private PackageInformation()
        {
        }
    }
}
