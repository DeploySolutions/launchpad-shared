// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-21-2023
// ***********************************************************************
// <copyright file="Checksum.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Util
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
        /// <param name="algName">Name of the alg.</param>
        /// <returns>System.String.</returns>
        public virtual string GetMd5Hash(string input, string algName = "")
        {
            byte[] hash = GetMd5HashAsBytes(input, algName);
            string checksum = BitConverter.ToString(hash);
            return checksum;
        }

        /// <summary>
        /// Gets the MD5 hash.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="algName">Name of the alg.</param>
        /// <returns>System.String.</returns>
        public virtual string GetMd5Hash(byte[] input, string algName = "")
        {
            byte[] hash = GetMd5HashAsBytes(input, algName);
            string checksum = BitConverter.ToString(hash);
            return checksum;
        }

        /// <summary>
        /// Gets the MD5 hash as bytes.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="algName">Name of the alg.</param>
        /// <returns>System.Byte[].</returns>
        public virtual byte[] GetMd5HashAsBytes(string input, string algName = "")
        {
            byte[] inputAsBytes = Encoding.UTF8.GetBytes(input);
            var hash = GetMd5HashAsBytes(inputAsBytes, algName);
            return hash;
        }

        /// <summary>
        /// Gets the MD5 hash as bytes.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="algName">Name of the alg.</param>
        /// <returns>System.Byte[].</returns>
        public virtual byte[] GetMd5HashAsBytes(byte[] input, string algName = "")
        {
            Guard.Against<ArgumentException>(input == null, "Input cannot be null.");
            Guard.Against<ArgumentException>(input.Length != 16, "MD5 byte input must be exactly 16 bytes.");

            MD5 hashAlgorithm = null;
            if (!string.IsNullOrEmpty(algName))
            {
                hashAlgorithm = MD5.Create(algName);
            }
            else
            {
                hashAlgorithm = MD5.Create();
            }
            var hash = hashAlgorithm.ComputeHash(input);
            return hash;
        }

        /// <summary>
        /// Gets the sha256 hash.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="algName">Name of the alg.</param>
        /// <returns>System.String.</returns>
        public virtual string GetSha256Hash(string input, string algName = "")
        {
            var hash = GetSha256HashAsBytes(input, algName);
            string checksum = Convert.ToHexString(hash);
            return checksum;
        }


        /// <summary>
        /// Gets the sha256 hash.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="algName">Name of the alg.</param>
        /// <returns>System.String.</returns>
        public virtual string GetSha256Hash(byte[] input, string algName = "")
        {
            var hash = GetSha256HashAsBytes(input, algName);
            string checksum = Convert.ToHexString(hash);
            return checksum;
        }

        /// <summary>
        /// Gets the sha256 hash as bytes.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="algName">Name of the alg.</param>
        /// <returns>System.Byte[].</returns>
        public virtual byte[] GetSha256HashAsBytes(string input, string algName = "")
        {
            byte[] inputAsBytes = Encoding.UTF8.GetBytes(input);
            var hash = GetSha256HashAsBytes(inputAsBytes, algName);
            return hash;
        }

        /// <summary>
        /// Gets the sha256 hash as bytes.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="algName">Name of the alg.</param>
        /// <returns>System.Byte[].</returns>
        public virtual byte[] GetSha256HashAsBytes(byte[] input, string algName = "")
        {
            Guard.Against<ArgumentException>(input == null, "Input cannot be null.");
            Guard.Against<ArgumentException>(input.Length != 32, "SHA256 byte input must be exactly 32 bytes.");

            SHA256 hashAlgorithm = null;
            if (!string.IsNullOrEmpty(algName))
            {
                hashAlgorithm = SHA256.Create(algName);
            }
            else
            {
                hashAlgorithm = SHA256.Create();
            }
            var hash = hashAlgorithm.ComputeHash(input);
            return hash;
        }

    }
}
