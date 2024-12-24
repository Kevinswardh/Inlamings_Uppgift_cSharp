using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business.CoreFiles.Helpers.Users
{
    /// <summary>
    /// En hjälpklass för att hasha lösenord och verifiera dem med SHA256.
    /// </summary>
    public static class PasswordHash
    {
        /// <summary>
        /// Hashar det angivna lösenordet i klartext med hjälp av SHA256.
        /// </summary>
        /// <param name="password">Lösenordet i klartext som ska hash-kodas.</param>
        /// <returns>En base64-kodad sträng som representerar det hashade lösenordet.</returns>
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        /// <summary>
        /// Verifierar att det angivna lösenordet i klartext matchar det hashade lösenordet.
        /// </summary>
        /// <param name="plainPassword">Lösenordet i klartext som ska verifieras.</param>
        /// <param name="hashedPassword">Det hashade lösenordet att jämföra med.</param>
        /// <returns>True om lösenorden matchar; annars false.</returns>
        public static bool VerifyPassword(string plainPassword, string hashedPassword)
        {
            string hashedInput = HashPassword(plainPassword);
            return hashedInput == hashedPassword;
        }
    }
}
