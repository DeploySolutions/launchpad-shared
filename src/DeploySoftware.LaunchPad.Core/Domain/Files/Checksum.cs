using DeploySoftware.LaunchPad.Core.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.Domain
{
    [Serializable]
    public partial class Checksum
    {
        /// <summary>
        /// File's content hash in MD5 16-byte fixed format (to facilitate file verification).
        /// This property may be null if the caller did not actually run the hash function and apply the result.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public byte[] Md5Hash { get; set; }

        /// <summary>
        /// File's content hash in SHA-256  32-byte fixed format (to facilitate file verification).
        /// This property may be null if the caller did not actually run the hash function and apply the result.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public byte[] Sha256Hash { get; set; }

        
        public Checksum()
        {

        }

        public virtual string GetMd5Hash(string input, string algName = "")
        {
            byte[] hash = GetMd5HashAsBytes(input, algName);
            string checksum = BitConverter.ToString(hash);
            return checksum;
        }

        public virtual string GetMd5Hash(byte[] input, string algName = "")
        {
            byte[] hash = GetMd5HashAsBytes(input, algName);
            string checksum = BitConverter.ToString(hash);
            return checksum;
        }

        public virtual byte[] GetMd5HashAsBytes(string input, string algName = "")
        {
            byte[] inputAsBytes = Encoding.UTF8.GetBytes(input);
            var hash = GetMd5HashAsBytes(inputAsBytes, algName);
            return hash;
        }

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

        public virtual string GetSha256Hash(string input, string algName = "")
        {
            var hash = GetSha256HashAsBytes(input, algName);
            string checksum = Convert.ToHexString(hash);
            return checksum;
        }


        public virtual string GetSha256Hash(byte[] input, string algName = "")
        {
            var hash = GetSha256HashAsBytes(input, algName);
            string checksum = Convert.ToHexString(hash);
            return checksum;
        }

        public virtual byte[] GetSha256HashAsBytes(string input, string algName = "")
        {
            byte[] inputAsBytes = Encoding.UTF8.GetBytes(input);
            var hash = GetSha256HashAsBytes(inputAsBytes, algName);
            return hash;
        }

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
