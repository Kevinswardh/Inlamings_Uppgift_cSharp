using Business.CoreFiles.Helpers.Users;
using Business.CoreFiles.Models.Contacts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CoreFiles.Models.Users
{
    public abstract class BaseUser
    {
        [Key]
        public string Id { get; set; } // string för att guid

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        [RegularExpression(@"^[\p{L}\s'-]{2,50}$", ErrorMessage = "Name must be between 2 and 50 letters and can contain spaces, hyphens, and apostrophes.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Lastname is required.")]
        [StringLength(50, ErrorMessage = "Lastname cannot be longer than 50 characters.")]
        [RegularExpression(@"^[\p{L}\s'-]{2,50}$", ErrorMessage = "Lastname must be between 2 and 50 letters and can contain spaces, hyphens, and apostrophes.")]
        public string Lastname { get; set; } = null!;

        public string Fullname => $"{Name} {Lastname}";

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = null!;

        // Protected lösenordsproperty för att skydda åtkomst
        protected string Password { get; set; } = null!;

        /// <summary>
        /// List of user's contacts.
        /// </summary>
        public List<Contact> Contacts { get; set; } = new List<Contact>();  // Initialize with new List<Contact>()

        /// <summary>
        /// List of user's favorite contacts.
        /// </summary>
        public List<FavoriteContact> Favorites { get; set; } = new List<FavoriteContact>();  // Initialize with new List<FavoriteContact>()

        /// <summary>
        /// Abstract property for defining the user's role.
        /// </summary>
        public abstract string Role { get; }

        /// <summary>
        /// Virtuell egenskap för användartyp (kan överskrivas av underklasser)
        /// </summary>
        public virtual string UserType => "No User type was given.";

        /// <summary>
        /// Sets the user's password by hashing the provided plain text password.
        /// </summary>
        /// <param name="plainPassword">The plain text password to hash and store.</param>
        public void SetPassword(string plainPassword)
        {
            Password = PasswordHash.HashPassword(plainPassword);
        }

        /// <summary>
        /// Verifies that the provided plain text password matches the stored hashed password.
        /// </summary>
        /// <param name="plainPassword">The plain text password to verify against the stored hashed password.</param>
        /// <returns>True if the provided plain password matches the stored hashed password; otherwise, false.</returns>
        public bool VerifyPassword(string plainPassword)
        {
            return PasswordHash.VerifyPassword(plainPassword, Password);
        }

    }
}
