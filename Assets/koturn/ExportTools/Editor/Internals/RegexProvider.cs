#if NET9_0_OR_GREATER
#    define SUPPORT_GENERATED_REGEX_PROPERTY
#endif  // NET9_0_OR_GREATER
#if NET7_0_OR_GREATER
#    define SUPPORT_GENERATED_REGEX
#endif  // NET7_0_OR_GREATER

using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;


namespace Koturn.ExportTools.Editor.Internals
{
    /// <summary>
    /// Provides some <see cref="Regex"/> instances.
    /// </summary>
    [System.Runtime.InteropServices.Guid("d61d67db-0b50-aae4-89fd-4e5c87aa3e4f")]
#if SUPPORT_GENERATED_REGEX
    internal static partial class RegexProvider
#else
    internal static class RegexProvider
#endif  // SUPPORT_GENERATED_REGEX
    {
        /// <summary>
        /// Options for <see cref="Regex"/> instances.
        /// </summary>
        private const RegexOptions Options = RegexOptions.Compiled | RegexOptions.CultureInvariant;

        /// <summary>
        /// <see cref="Regex"/> pattern <see cref="string"/> matching <see cref="System.Reflection.AssemblyVersionAttribute"/> declaration.
        /// </summary>
        [StringSyntax(StringSyntaxAttribute.Regex)]
        private const string AssemblyVersionPattern = @"\[\s*assembly:\s*AssemblyVersion\s*(\s*""([^""]+)""\s*)\s*\]";
        /// <summary>
        /// <see cref="Regex"/> pattern <see cref="string"/> matching <see cref="System.Reflection.AssemblyFileVersionAttribute"/> declaration.
        /// </summary>
        [StringSyntax(StringSyntaxAttribute.Regex)]
        private const string AssemblyFileVersionPattern = @"\[\s*assembly:\s*AssemblyFileVersion\s*(\s*""([^""]+)""\s*)\s*\]";
        /// <summary>
        /// <see cref="Regex"/> pattern <see cref="string"/> matching <see cref="System.Reflection.AssemblyInformationalVersionAttribute"/> declaration.
        /// </summary>
        [StringSyntax(StringSyntaxAttribute.Regex)]
        private const string AssemblyInformationalVersionPattern = @"\[\s*assembly:\s*AssemblyInformationalVersion\s*\(\s*""([^""]+)""\s*\)\s*\]";
        /// <summary>
        /// <see cref="Regex"/> pattern <see cref="string"/> matching version number of <see href="https://semver.org/">semantic versioning 2.0.0</see>.
        /// </summary>
        /// <remarks>
        /// <see href="https://semver.org/#is-there-a-suggested-regular-expression-regex-to-check-a-semver-string"/>
        /// </remarks>
        [StringSyntax(StringSyntaxAttribute.Regex)]
        private const string SemVerPattern = @"^(0|[1-9]\d*)\.(0|[1-9]\d*)\.(0|[1-9]\d*)(?:-((?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+([0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$";


        /// <summary>
        /// <see cref="Regex"/> instance matching <see cref="System.Reflection.AssemblyVersionAttribute"/> declaration.
        /// </summary>
#if SUPPORT_GENERATED_REGEX_PROPERTY
        [GeneratedRegex(AssemblyVersionPattern, Options)]
        public static partial Regex AssemblyVersionRegex { get; }
#elif SUPPORT_GENERATED_REGEX
        public static Regex AssemblyVersionRegex => GetAssemblyVersionRegex();
        /// <summary>
        /// Get <see cref="Regex"/> instance matching <see cref="System.Reflection.AssemblyVersionAttribute"/> declaration.
        /// </summary>
        /// <returns><see cref="Regex"/> instance matching identifier.</returns>
        [GeneratedRegex(AssemblyVersionPattern, Options)]
        private static partial Regex GetAssemblyVersionRegex();
#else
        public static Regex AssemblyVersionRegex => _assemblyVersionRegex ?? (_assemblyVersionRegex = new Regex(AssemblyVersionPattern, Options));
        /// <summary>
        /// Cache field of <see cref="AssemblyVersionRegex"/>.
        /// </summary>
        private static Regex _assemblyVersionRegex;
#endif  // SUPPORT_GENERATED_REGEX_PROPERTY

        /// <summary>
        /// <see cref="Regex"/> instance matching <see cref="System.Reflection.AssemblyFileVersionAttribute"/> declaration.
        /// </summary>
#if SUPPORT_GENERATED_REGEX_PROPERTY
        [GeneratedRegex(AssemblyFileVersionPattern, Options)]
        public static partial Regex AssemblyFileVersionRegex { get; }
#elif SUPPORT_GENERATED_REGEX
        public static Regex AssemblyFileVersionRegex => GetAssemblyFileVersionRegex();
        /// <summary>
        /// Get <see cref="Regex"/> instance matching <see cref="System.Reflection.AssemblyFileVersionAttribute"/> declaration.
        /// </summary>
        /// <returns><see cref="Regex"/> instance matching identifier.</returns>
        [GeneratedRegex(AssemblyFileVersionPattern, Options)]
        private static partial Regex GetAssemblyFileVersionRegex();
#else
        public static Regex AssemblyFileVersionRegex => _assemblyFileVersionRegex ?? (_assemblyFileVersionRegex = new Regex(AssemblyFileVersionPattern, Options));
        /// <summary>
        /// Cache field of <see cref="AssemblyFileVersionRegex"/>.
        /// </summary>
        private static Regex _assemblyFileVersionRegex;
#endif  // SUPPORT_GENERATED_REGEX_PROPERTY

        /// <summary>
        /// <see cref="Regex"/> instance matching <see cref="System.Reflection.AssemblyInformationalVersionAttribute"/> declaration.
        /// </summary>
#if SUPPORT_GENERATED_REGEX_PROPERTY
        [GeneratedRegex(AssemblyInformationalVersionPattern, Options)]
        public static partial Regex AssemblyInformationalVersionRegex { get; }
#elif SUPPORT_GENERATED_REGEX
        public static Regex AssemblyInformationalVersionRegex => GetAssemblyInformationalVersionRegex();
        /// <summary>
        /// Get <see cref="Regex"/> instance matching <see cref="System.Reflection.AssemblyInformationalVersionAttribute"/> declaration.
        /// </summary>
        /// <returns><see cref="Regex"/> instance matching identifier.</returns>
        [GeneratedRegex(AssemblyInformationalVersionPattern, Options)]
        private static partial Regex GetAssemblyInformationalVersionRegex();
#else
        public static Regex AssemblyInformationalVersionRegex => _assemblyInformationalVersionRegex ?? (_assemblyInformationalVersionRegex = new Regex(AssemblyInformationalVersionPattern, Options));
        /// <summary>
        /// Cache field of <see cref="AssemblyInformationalVersionRegex"/>.
        /// </summary>
        private static Regex _assemblyInformationalVersionRegex;
#endif  // SUPPORT_GENERATED_REGEX_PROPERTY

        /// <summary>
        /// <see cref="Regex"/> instance matching version number of <see href="https://semver.org/">semantic versioning 2.0.0</see>.
        /// </summary>
#if SUPPORT_GENERATED_REGEX_PROPERTY
        [GeneratedRegex(SemVerPattern, Options)]
        public static partial Regex SemVerRegex { get; }
#elif SUPPORT_GENERATED_REGEX
        public static Regex SemVerRegex => GetSemVerRegex();
        /// <summary>
        /// Get <see cref="Regex"/> instance matching version number of <see href="https://semver.org/">semantic versioning 2.0.0</see>.
        /// </summary>
        /// <returns><see cref="Regex"/> instance matching version number of semantic versioning 2.0.0.</returns>
        [GeneratedRegex(SemVerPattern, Options)]
        private static partial Regex GetSemVerRegex();
#else
        public static Regex SemVerRegex => _semVerRegex ?? (_semVerRegex = new Regex(SemVerPattern, Options));
        /// <summary>
        /// Cache field of <see cref="SemVerRegex"/>.
        /// </summary>
        private static Regex _semVerRegex;
#endif  // SUPPORT_GENERATED_REGEX_PROPERTY
    }
}
