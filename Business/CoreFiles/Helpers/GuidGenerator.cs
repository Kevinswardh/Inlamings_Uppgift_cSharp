using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CoreFiles.Helpers
{
    /// <summary>
    /// En hjälpklass för att generera unika identifierare (GUID).
    /// </summary>
    public static class GuidGenerator
    {
        /// <summary>
        /// Genererar ett nytt unikt identifierar (GUID) som en sträng.
        /// </summary>
        /// <returns>En strängrepresentation av ett nytt GUID.</returns>
        public static string GenerateGuid()
        {
            return Guid.NewGuid().ToString();  // Genererar ett nytt GUID och konverterar det till en sträng.
        }
    }
}
