namespace Boilerplate.AspNetCore.TagHelpers
{
    using System;

    /// <summary>
    /// Cryptographic hashing algorithms available for use with Sub-resource Integrity (SRI).
    /// </summary>
    [Flags]
    public enum SubresourceIntegrityHashAlgorithm
    {
        /// <summary>
        /// The SHA256 cryptographic hashing algorithm.
        /// </summary>
        SHA256 = 1,

        /// <summary>
        /// The SHA384 cryptographic hashing algorithm.
        /// </summary>
        SHA384 = 2,

        /// <summary>
        /// The SHA512 cryptographic hashing algorithm.
        /// </summary>
        SHA512 = 4
    }
}
