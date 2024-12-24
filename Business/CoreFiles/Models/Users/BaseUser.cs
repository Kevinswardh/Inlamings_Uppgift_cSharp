using Business.CoreFiles.Helpers.Users;
using Business.CoreFiles.Models.Contacts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CoreFiles.Models.Users
{
    /// <summary>
    /// Abstrakt klass som representerar en användare och innehåller gemensamma egenskaper för alla användartyper.
    /// </summary>
    public abstract class BaseUser
    {
        /// <summary>
        /// Användarens unika ID (GUID).
        /// </summary>
        [Key]
        public string Id { get; set; } // string för att använda GUID

        /// <summary>
        /// Användarens förnamn.
        /// </summary>
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        [RegularExpression(@"^[\p{L}\s'-]{2,50}$", ErrorMessage = "Name must be between 2 and 50 letters and can contain spaces, hyphens, and apostrophes.")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Användarens efternamn.
        /// </summary>
        [Required(ErrorMessage = "Lastname is required.")]
        [StringLength(50, ErrorMessage = "Lastname cannot be longer than 50 characters.")]
        [RegularExpression(@"^[\p{L}\s'-]{2,50}$", ErrorMessage = "Lastname must be between 2 and 50 letters and can contain spaces, hyphens, and apostrophes.")]
        public string Lastname { get; set; } = null!;

        /// <summary>
        /// Fullständigt namn som kombinerar förnamn och efternamn.
        /// </summary>
        public string Fullname => $"{Name} {Lastname}";

        /// <summary>
        /// Användarens e-postadress.
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = null!;

        /// <summary>
        /// Lösenordsproperty för att lagra det hashade lösenordet.
        /// </summary>
        [JsonProperty("Password")] // Gör lösenordet serialiserbart med JSON
        protected string Password { get; private set; } = null!;



        /// <summary>
        /// Lista över användarens kontakter.
        /// </summary>
        public List<Contact> Contacts { get; set; } = new List<Contact>();  // Initialisera med en tom lista av kontakter

        /// <summary>
        /// Lista över användarens favoritkontakter.
        /// </summary>
        public List<FavoriteContact> Favorites { get; set; } = new List<FavoriteContact>();  // Initialisera med en tom lista av favoriter

        /// <summary>
        /// Abstrakt egenskap för att definiera användarens roll.
        /// </summary>
        public abstract string Role { get; }

        /// <summary>
        /// Virtuell egenskap för användartyp som kan överskrivas av underklasser.
        /// </summary>
        public virtual string UserType => "No User type was given.";

        /// <summary>
        /// Sätter användarens lösenord genom att hasha det angivna lösenordet.
        /// </summary>
        /// <param name="plainPassword">Lösenord i klartext som ska hashats och lagras.</param>
        public void SetPassword(string plainPassword)
        {
            Password = PasswordHash.HashPassword(plainPassword);
        }
        /// <summary>
        /// Validerar användarens lösenord.
        /// </summary>
        public bool ValidatePassword(string plainPassword)
        {
            string hashedInput = PasswordHash.HashPassword(plainPassword);
            Console.WriteLine($"Input hash: {hashedInput}");
            Console.WriteLine($"Stored hash: {Password}");
            return hashedInput == Password;
        }

    }
}
