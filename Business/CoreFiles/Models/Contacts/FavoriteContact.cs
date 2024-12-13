using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CoreFiles.Models.Contacts
{
    public class FavoriteContact : Contact
    {
        /// <summary>
        /// The Favorite property as a string, indicates that this is a favorite contact.
        /// </summary>
        public string Favorite { get; set; } = "Favorite";

    }
}