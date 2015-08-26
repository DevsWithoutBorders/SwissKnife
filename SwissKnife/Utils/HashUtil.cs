using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SwissKnife.Ext;

namespace SwissKnife.Utils
{
    public class HashUtil
    {
        /// <summary>
        /// Compute the Hash for a Stream
        /// </summary>
        /// <param name="stream">Stream to compute hash for</param>
        /// <param name="hashAlgorithm">Algorithm to use</param>
        /// <returns></returns>
        public static string ComputeHash(Stream stream, HashAlgorithmType hashAlgorithm = HashAlgorithmType.SHA1)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            byte[] bytes = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(bytes, 0, (int)stream.Length);

            return HashUtil.ComputeHash(bytes, hashAlgorithm);
        }

        /// <summary>
        /// Compute the Hash for the contents of a File
        /// </summary>
        /// <param name="path">Full path of the File to compute hash for</param>
        /// <param name="hashAlgorithm">Algorithm to use</param>
        /// <returns></returns>
        public static string ComputeHashForFile(string path, HashAlgorithmType hashAlgorithm = HashAlgorithmType.SHA1)
        {
            if (path.IsNullOrWhiteSpace())
                throw new ArgumentNullException("path", "IsNullOrWhiteSpace");

            if (!File.Exists(path))
                throw new FileNotFoundException("At path: ".FormatSafe(path));

            byte[] fileBytes;
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                fileBytes = new byte[fs.Length];
                fs.Read(fileBytes, 0, (int)fs.Length);
            }

            return HashUtil.ComputeHash(fileBytes, hashAlgorithm);
        }

        /// <summary>
        /// Compute the Hash for a Byte Array
        /// </summary>
        /// <param name="stream">Byte Array to compute hash for</param>
        /// <param name="hashAlgorithm">Algorithm to use</param>
        /// <returns></returns>
        public static string ComputeHash(byte[] bytes, HashAlgorithmType hashAlgorithm = HashAlgorithmType.SHA1)
        {
            if (bytes == null)
                throw new ArgumentNullException("bytes");

            // Initialize appropriate hashing algorithm class.
            HashAlgorithm hash;
            switch (hashAlgorithm)
            {
                case HashAlgorithmType.MD5:
                    hash = new MD5CryptoServiceProvider();
                    break;
                case HashAlgorithmType.SHA1:
                    hash = new SHA1Managed();
                    break;
                case HashAlgorithmType.SHA256:
                    hash = new SHA256Managed();
                    break;
                case HashAlgorithmType.SHA384:
                    hash = new SHA384Managed();
                    break;
                case HashAlgorithmType.SHA512:
                    hash = new SHA512Managed();
                    break;
                default:
                    throw new NotImplementedException("HashAlgorithm is not yet implemented.");
            }

            // Compute hash value of our plain text.
            byte[] hashBytes = hash.ComputeHash(bytes);

            // Convert result into a string.
            string hashValue = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

            // Return the result.
            return hashValue;
        }

        /// <summary>
        /// Compute the Hash for a Text
        /// </summary>
        /// <param name="text">The text to compute the hash for</param>
        /// <param name="hashAlgorithm">Algorithm to use</param>
        /// <returns>Returns the hash.</returns>
        public static string ComputeHash(string text, HashAlgorithmType hashAlgorithm = HashAlgorithmType.SHA1)
        {
            if (text == null)
                throw new ArgumentNullException("text");

            // Convert text into a byte array.            
            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            return HashUtil.ComputeHash(textBytes, hashAlgorithm);
        }

        /// <summary>
        /// Compare the Hash of the supplied Source and check if it equals the supplied hash (casing will be ignored)
        /// </summary>
        /// <param name="originalText">The text to compute the hash for</param>
        /// <param name="relatedHash">The hash to compare with the hash that's now compupted for originalText</param>
        /// <param name="hashAlgorithm">Algorithm to use</param>
        /// <returns>Returns true if the hashed matches; otherwise false.</returns>
        public static bool IsValidHash(string originalText, string relatedHash, HashAlgorithmType hashAlgorithm = HashAlgorithmType.SHA1)
        {
            return ComputeHash(originalText, hashAlgorithm).Equals(relatedHash, StringComparison.OrdinalIgnoreCase);
        }
    }

    /// <summary>
    /// The available hash types.
    /// </summary>
    public enum HashAlgorithmType
    {
        /// <summary>
        /// MD5 hash type.
        /// </summary>
        MD5,
        /// <summary>
        /// SHA-1 hash type.
        /// </summary>
        SHA1,
        /// <summary>
        /// SHA-256 hash type.
        /// </summary>
        SHA256,
        /// <summary>
        /// SHA-384 hash type.
        /// </summary>
        SHA384,
        /// <summary>
        /// SHA-512 hash type.
        /// </summary>
        SHA512
    }
}

