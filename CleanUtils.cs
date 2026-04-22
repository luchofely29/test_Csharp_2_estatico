using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Empresa
{
    /// <summary>
    /// Provides static utility methods for common operations.
    /// This class cannot be instantiated.
    /// </summary>
    public static class CleanUtils
    {
        /// <summary>Gets the application version.</summary>
        public const string Version = "1.0";

        /// <summary>
        /// Returns the sum of two integers.
        /// </summary>
        /// <param name="firstValue">The first operand.</param>
        /// <param name="secondValue">The second operand.</param>
        /// <returns>The arithmetic sum of both values.</returns>
        public static int CalculateSum(int firstValue, int secondValue)
        {
            return firstValue + secondValue;
        }

        /// <summary>
        /// Reads and returns the content of a file as a UTF-8 string.
        /// </summary>
        /// <param name="filePath">
        /// The absolute or relative path to the file.
        /// </param>
        /// <returns>The file content as a string.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="filePath"/> is null or whitespace.
        /// </exception>
        /// <exception cref="IOException">
        /// Thrown when the file cannot be read.
        /// </exception>
        public static string ReadFileContent(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException(
                    "File path must not be null or empty.",
                    nameof(filePath));
            }

            return File.ReadAllText(filePath, Encoding.UTF8);
        }

        /// <summary>
        /// Writes content to a file using UTF-8 encoding.
        /// </summary>
        /// <param name="filePath">The destination file path.</param>
        /// <param name="content">The string content to write.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="filePath"/> is null or whitespace.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="content"/> is null.
        /// </exception>
        public static void WriteFileContent(string filePath, string content)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException(
                    "File path must not be null or empty.",
                    nameof(filePath));
            }

            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            File.WriteAllText(filePath, content, Encoding.UTF8);
        }

        /// <summary>
        /// Compares two strings in constant time to prevent timing attacks.
        /// </summary>
        /// <param name="first">The first string to compare.</param>
        /// <param name="second">The second string to compare.</param>
        /// <returns>
        /// <see langword="true"/> if both strings are equal;
        /// otherwise <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when either argument is null.
        /// </exception>
        public static bool SecureCompare(string first, string second)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            using SHA256 sha256 = SHA256.Create();
            byte[] firstHash = sha256.ComputeHash(
                Encoding.UTF8.GetBytes(first));
            byte[] secondHash = sha256.ComputeHash(
                Encoding.UTF8.GetBytes(second));
            return CryptographicOperations.FixedTimeEquals(
                firstHash, secondHash);
        }

        /// <summary>
        /// Retrieves the API key from the <c>APP_API_KEY</c>
        /// environment variable.
        /// </summary>
        /// <returns>The API key value.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the environment variable is not set or is empty.
        /// </exception>
        public static string GetApiKey()
        {
            string? apiKey = Environment.GetEnvironmentVariable(
                "APP_API_KEY");

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new InvalidOperationException(
                    "APP_API_KEY environment variable is not set.");
            }

            return apiKey;
        }
    }
}
