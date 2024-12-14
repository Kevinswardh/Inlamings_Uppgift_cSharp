using Business.CoreFiles.Factory.Interfaces;
using Business.CoreFiles.Models.Users;
using Business.CoreFiles.Models.Users.Roles;
using System;
using System.Collections.Generic;
using Business.CoreFiles.Models.Contacts;

namespace Business.CoreFiles.Factory
{
    public class ExampleUserCreate : IExampleUserCreate
    {
        public BaseUser CreateExampleUser()
        {
            var user = new DefaultUser
            {
                Id = Guid.NewGuid().ToString(),
                Name = "ExampleName",
                Lastname = "ExampleLastname",
                Email = "x@x.xx",
                Contacts = GetExampleContacts(),
                Favorites = GetExampleFavorites()
            };

            // Set a default password
            user.SetPassword("123456");

            return user;
        }

        // Method to create example contacts
        private List<Contact> GetExampleContacts()
        {
            return new List<Contact>
            {
                new Contact
                {
                    Id = 1,
                    Name = "John",
                    Lastname = "Doe",
                    PhoneNumber = "+1234567890",
                    Address = "123 Main Street",
                    Email = "johndoe@example.com",
                    Notes = "Work contact"
                },
                new Contact
                {
                    Id = 2,
                    Name = "Jane",
                    Lastname = "Smith",
                    PhoneNumber = "+0987654321",
                    Address = "456 Elm Street",
                    Email = "janesmith@example.com",
                    Notes = "Friend"
                }
            };
        }

        // Method to create example favorite contacts
        private List<FavoriteContact> GetExampleFavorites()
        {
            return new List<FavoriteContact>
            {
                new FavoriteContact
                {
                    Id = 1,
                    Name = "Alice",
                    Lastname = "Johnson",
                    PhoneNumber = "+1112233445",
                    Address = "789 Maple Avenue",
                    Email = "alicejohnson@example.com",
                    Notes = "Family member",
                    Favorite = "Favorite"
                }
            };
        }
    }
}
