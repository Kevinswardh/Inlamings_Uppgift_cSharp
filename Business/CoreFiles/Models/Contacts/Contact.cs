using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CoreFiles.Models.Contacts
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Lastname is required.")]
        public string Lastname { get; set; } = null!;

        public string Fullname => $"{Name} {Lastname}";

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\+?\d{7,15}$", ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }

        /// <summary>
        /// Additional notes for the contact.
        /// </summary>
        public string? Notes { get; set; }

    }
}
