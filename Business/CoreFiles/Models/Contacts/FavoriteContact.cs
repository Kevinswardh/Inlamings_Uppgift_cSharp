using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CoreFiles.Models.Contacts
{
    /// <summary>
    /// Representerar en favoritkontakt som är en utökning av klassen Contact.
    /// </summary>
    public class FavoriteContact : Contact
    {
        /// <summary>
        /// Anger att detta är en favoritkontakt.
        /// </summary>
        public string Favorite { get; set; } = "Favorite";
    }
}
