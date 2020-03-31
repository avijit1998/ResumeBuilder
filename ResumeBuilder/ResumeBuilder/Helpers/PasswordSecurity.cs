using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ResumeBuilder.Helpers
{
    public class PasswordSecurity
    {
        public static string GenerateSalt()
        {
            // Define min and max salt sizes.
            var minSaltSize = 32;
            var maxSaltSize = 64;

            // Generate a random number for the size of the salt.
            var random = new Random();
            var saltSize = random.Next(minSaltSize, maxSaltSize);

            // Allocate a byte array, which will hold the salt.
            var saltBytes = new byte[saltSize];

            // Initialize a random number generator.
            var rng = RandomNumberGenerator.Create();

            // Fill the salt with cryptographically strong byte values.
            //rng.GetNonZeroBytes(saltBytes);
            rng.GetBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }

        /// <summary>
        /// Check if the supplied password is valid
        /// </summary>
        /// <param name="password">The password sent by the user</param>
        /// <param name="passwordSalt">The stored password salt</param>
        /// <param name="hashedPassword">The stored hashed password</param>
        /// <returns></returns>
        public static bool IsValid(string password, string passwordSalt, string hashedPassword)
        {
            using (var hash = SHA512.Create())
            {
                var saltedPlainTextBytes = Encoding.UTF8.GetBytes(password).Concat(Convert.FromBase64String(passwordSalt)).ToArray();
                var hashedBytes = hash.ComputeHash(saltedPlainTextBytes);
                return hashedBytes.SequenceEqual(Convert.FromBase64String(hashedPassword));
            }
        }

        public static string HashPassword(string value, string passwordSalt)
        {
            using (var hash = SHA512.Create())
            {
                var saltedPlainTextBytes = Encoding.UTF8.GetBytes(value).Concat(Convert.FromBase64String(passwordSalt)).ToArray();
                var hashedBytes = hash.ComputeHash(saltedPlainTextBytes);
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}