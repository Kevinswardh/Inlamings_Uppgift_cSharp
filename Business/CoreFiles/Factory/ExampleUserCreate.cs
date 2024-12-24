using Business.CoreFiles.Factory.Interfaces;
using Business.CoreFiles.Models.Users;
using Business.CoreFiles.Models.Users.Roles;
using System;
using System.Collections.Generic;
using Business.CoreFiles.Models.Contacts;

namespace Business.CoreFiles.Factory
{
    /// <summary>
    /// En fabriksklass för att skapa en exempelanvändare med kontakter och favoriter.
    /// </summary>
    public class ExampleUserCreate : IExampleUserCreate
    {
        /// <summary>
        /// Skapar en exempelanvändare med fördefinierade värden för namn, e-post, kontakter och favoriter.
        /// </summary>
        /// <returns>Den skapade exempelanvändaren som en instans av <see cref="BaseUser"/>.</returns>
        public BaseUser CreateExampleUser()
        {
            var user = new DefaultUser
            {
                Id = Guid.NewGuid().ToString(),  // Genererar ett unikt GUID för användaren
                Name = "ExampleName",
                Lastname = "ExampleLastname",
                Email = "x@x.xx",
                Contacts = GetExampleContacts(),  // Hämtar en lista med exempelkontakter
                Favorites = GetExampleFavorites()  // Hämtar en lista med exempel-favoritkontakter
            };

            // Sätter ett standardlösenord för användaren
            user.SetPassword("123456");

            return user;  // Returnerar den skapade exempelanvändaren
        }

        /// <summary>
        /// Skapar en lista med exempelkontakter.
        /// </summary>
        /// <returns>En lista med fördefinierade <see cref="Contact"/>-objekt.</returns>
        private List<Contact> GetExampleContacts()
        {
            return new List<Contact>
            {
                new Contact
                {
                    Id = "1",
                    Name = "John",
                    Lastname = "Doe",
                    PhoneNumber = "+1234567890",
                    Address = "123 Main Street",
                    Email = "johndoe@example.com",
                    Notes = "Work contact"  // Anteckning om kontakten
                },
                new Contact
                {
                    Id = "2",
                    Name = "Jane",
                    Lastname = "Smith",
                    PhoneNumber = "+0987654321",
                    Address = "456 Elm Street",
                    Email = "janesmith@example.com",
                    Notes = "Friend"  // Anteckning om kontakten
                }
            };
        }

        /// <summary>
        /// Skapar en lista med exempel-favoritkontakter.
        /// </summary>
        /// <returns>En lista med fördefinierade <see cref="FavoriteContact"/>-objekt.</returns>
        private List<FavoriteContact> GetExampleFavorites()
        {
            return new List<FavoriteContact>
            {
                new FavoriteContact
                {
                    Id = "1",
                    Name = "Alice",
                    Lastname = "Johnson",
                    PhoneNumber = "+1112233445",
                    Address = "789 Maple Avenue",
                    Email = "alicejohnson@example.com",
                    Notes = "Family member",  // Anteckning om favoritkontakten
                    Favorite = "Favorite"  // Favoritstatus
                }
            };
        }
    }
}
