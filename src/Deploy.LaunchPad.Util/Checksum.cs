// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-21-2023
// ***********************************************************************
// <copyright file="Checksum.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Util
{
    /// <summary>
    /// Class Checksum.
    /// </summary>
    [Serializable]
    public partial class Checksum
    {
        /// <summary>
        /// File's content hash in MD5 16-byte fixed format (to facilitate file verification).
        /// This property may be null if the caller did not actually run the hash function and apply the result.
        /// </summary>
        /// <value>The MD5 hash.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public byte[] Md5Hash { get; set; }

        /// <summary>
        /// File's content hash in SHA-256  32-byte fixed format (to facilitate file verification).
        /// This property may be null if the caller did not actually run the hash function and apply the result.
        /// </summary>
        /// <value>The sha256 hash.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public byte[] Sha256Hash { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Checksum"/> class.
        /// </summary>
        public Checksum()
        {

        }

        /// <summary>
        /// Gets the MD5 hash.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>System.String.</returns>
        public virtual string GetMd5Hash(string input)
        {
            byte[] hash = GetMd5HashAsBytes(input);
            string checksum = BitConverter.ToString(hash);
            return checksum;
        }

        /// <summary>
        /// Gets the MD5 hash.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>System.String.</returns>
        public virtual string GetMd5Hash(byte[] input)
        {
            byte[] hash = GetMd5HashAsBytes(input);
            string checksum = BitConverter.ToString(hash);
            return checksum;
        }

        /// <summary>
        /// Gets the MD5 hash as bytes.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>System.Byte[].</returns>
        public virtual byte[] GetMd5HashAsBytes(string input)
        {
            byte[] inputAsBytes = Encoding.UTF8.GetBytes(input);
            var hash = GetMd5HashAsBytes(inputAsBytes);
            return hash;
        }

        /// <summary>
        /// Gets the MD5 hash as bytes.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>System.Byte[].</returns>
        public virtual byte[] GetMd5HashAsBytes(byte[] input)
        {
            Guard.Against<ArgumentException>(input == null, "Input cannot be null.");
            Guard.Against<ArgumentException>(input.Length != 16, "MD5 byte input must be exactly 16 bytes.");

            MD5 hashAlgorithm = MD5.Create();
            var hash = hashAlgorithm.ComputeHash(input);
            return hash;
        }

        /// <summary>
        /// Gets the sha256 hash.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>System.String.</returns>
        public virtual string GetSha256Hash(string input)
        {
            var hash = GetSha256HashAsBytes(input);
            string checksum = Convert.ToHexString(hash);
            return checksum;
        }


        /// <summary>
        /// Gets the sha256 hash.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>System.String.</returns>
        public virtual string GetSha256Hash(byte[] input)
        {
            var hash = GetSha256HashAsBytes(input);
            string checksum = Convert.ToHexString(hash);
            return checksum;
        }

        /// <summary>
        /// Gets the sha256 hash as bytes.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>System.Byte[].</returns>
        public virtual byte[] GetSha256HashAsBytes(string input)
        {
            byte[] inputAsBytes = Encoding.UTF8.GetBytes(input);
            var hash = GetSha256HashAsBytes(inputAsBytes);
            return hash;
        }

        /// <summary>
        /// Gets the sha256 hash as bytes.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>System.Byte[].</returns>
        public virtual byte[] GetSha256HashAsBytes(byte[] input)
        {
            Guard.Against<ArgumentException>(input == null, "Input cannot be null.");
           // Guard.Against<ArgumentException>(input.Length != 32, "SHA256 byte input must be exactly 32 bytes.");

            SHA256 hashAlgorithm = SHA256.Create();
            var hash = hashAlgorithm.ComputeHash(input);
            return hash;
        }

    }
}
