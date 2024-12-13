using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business.CoreFiles.Helpers.Users
{
    public static class PasswordHash
    {

            /// <summary>
            /// Hashes the provided plain text password using SHA256.
            /// </summary>
            /// <param name="password">The plain text password to hash.</param>
            /// <returns>A base64-encoded string representing the hashed password.</returns>
            public static string HashPassword(string password)
            {
                using (var sha256 = SHA256.Create())
                {
                    byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    return Convert.ToBase64String(bytes);
                }
            }


            /// <summary>
            /// Verifies that the provided plain text password matches the hashed password.
            /// </summary>
            /// <param name="plainPassword">The plain text password to verify.</param>
            /// <param name="hashedPassword">The hashed password to compare against.</param>
            /// <returns>True if the passwords match; otherwise, false.</returns>
            public static bool VerifyPassword(string plainPassword, string hashedPassword)
            {
                string hashedInput = HashPassword(plainPassword);
                return hashedInput == hashedPassword;
            }
        
    }
}
