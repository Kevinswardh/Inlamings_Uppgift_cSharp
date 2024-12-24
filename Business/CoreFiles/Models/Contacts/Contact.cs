using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CoreFiles.Models.Contacts
{
    /// <summary>
    /// Representerar en kontakt med detaljer som namn, telefonnummer, adress och e-post.
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// Kontaktens unika ID.
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Kontaktens förnamn.
        /// </summary>
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Kontaktens efternamn.
        /// </summary>
        [Required(ErrorMessage = "Lastname is required.")]
        public string Lastname { get; set; } = null!;

        /// <summary>
        /// Kontaktens fullständiga namn, kombination av förnamn och efternamn.
        /// </summary>
        public string Fullname => $"{Name} {Lastname}";

        /// <summary>
        /// Kontaktens telefonnummer.
        /// </summary>
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\+?\d{7,15}$", ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; } = null!;

        /// <summary>
        /// Kontaktens adress.
        /// </summary>
        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; } = null!;

        /// <summary>
        /// Kontaktens e-postadress.
        /// </summary>
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }

        /// <summary>
        /// Ytterligare anteckningar för kontakten.
        /// </summary>
        public string? Notes { get; set; }
    }
}
