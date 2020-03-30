using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace ResumeBuilder.Helpers
{
    public class PasswordSecurity
    {
        /// <summary>
        /// Return a newly generated salt.
        /// </summary>
        public static byte[] GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[32];

                rng.GetBytes(randomNumber);

                return randomNumber;

            }
        }

        /// <summary>
        /// If the two SHA1 hashes are the same, returns true.
        /// Otherwise returns false.
        /// </summary>
        /// <param name="savedPasswordBytes"></param>
        /// <param name="enteredPasswordBytes"></param>
        public static bool MatchSHA(byte[] savedPasswordBytes, byte[] enteredPasswordBytes)
        {
            bool result = false;
            if (savedPasswordBytes != null && enteredPasswordBytes != null)
            {
                if (savedPasswordBytes.Length == enteredPasswordBytes.Length)
                {
                    result = true;
                    for (int i = 0; i < savedPasswordBytes.Length; i++)
                    {
                        if (savedPasswordBytes[i] != enteredPasswordBytes[i])
                        {
                            result = false;
                            break;
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// Returns the SHA256 hash of the password with the help of salt.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns>Byte Array of Hashed Password</returns>
        public static byte[] ComputeHMAC_SHA256(byte[] password, byte[] salt)
        {
            using (var hmac = new HMACSHA256(salt))
            {
                return hmac.ComputeHash(password);
            }
        }
    }
}