using Business.CoreFiles.Models.Contacts;
using Business.Logic._1_Services.UserService;
using Business.Interfaces.Repositories;
using System.Collections.Generic;
using Xunit;

namespace Business.Tests
{
    /// <summary>
    /// Enhetstester för ContactService.
    /// </summary>
    public class ContactServiceTests
    {
        private readonly IContactRepository _contactRepository;
        private readonly ContactService _contactService;

        /// <summary>
        /// Konstruktor som initierar ContactRepository och ContactService för tester.
        /// </summary>
        public ContactServiceTests()
        {
            // Skapa en instans av InMemoryContactRepository och ContactService
            _contactRepository = new InMemoryContactRepository();
            _contactService = new ContactService(_contactRepository);
        }

        [Fact]
        public void CreateContact_ShouldAddContactToUser()
        {
            // Arrange
            var userId = "user1";
            var contact = new Contact { Id = 1, Name = "John", Lastname = "Doe", PhoneNumber = "123456789" };

            // Act
            _contactService.CreateContact(userId, contact);
            var contacts = _contactService.GetAllContacts(userId);

            // Assert
            Assert.Single(contacts);
            Assert.Equal("John", contacts[0].Name);
            Assert.Equal("Doe", contacts[0].Lastname);
        }

        [Fact]
        public void UpdateContact_ShouldUpdateExistingContact()
        {
            // Arrange
            var userId = "user1";
            var contact = new Contact { Id = 1, Name = "John", Lastname = "Doe", PhoneNumber = "123456789" };
            _contactService.CreateContact(userId, contact);

            // Act
            contact.Name = "Jane";
            contact.Lastname = "Smith";
            _contactService.UpdateContact(userId, contact);
            var updatedContacts = _contactService.GetAllContacts(userId);

            // Assert
            Assert.Single(updatedContacts);
            Assert.Equal("Jane", updatedContacts[0].Name);
            Assert.Equal("Smith", updatedContacts[0].Lastname);
        }

        [Fact]
        public void DeleteContact_ShouldRemoveContact()
        {
            // Arrange
            var userId = "user1";
            var contact = new Contact { Id = 1, Name = "John", Lastname = "Doe", PhoneNumber = "123456789" };
            _contactService.CreateContact(userId, contact);

            // Act
            _contactService.DeleteContact(userId, contact.Id);
            var contacts = _contactService.GetAllContacts(userId);

            // Assert
            Assert.Empty(contacts);
        }

        [Fact]
        public void AddFavorite_ShouldAddFavoriteContact()
        {
            // Arrange
            var userId = "user1";
            var favorite = new FavoriteContact { Id = 1, Name = "Alice", Lastname = "Johnson", PhoneNumber = "987654321" };

            // Act
            _contactService.AddFavorite(userId, favorite);
            var favorites = _contactService.GetAllFavorites(userId);

            // Assert
            Assert.Single(favorites);
            Assert.Equal("Alice", favorites[0].Name);
            Assert.Equal("Johnson", favorites[0].Lastname);
        }

        [Fact]
        public void RemoveFavorite_ShouldRemoveFavoriteContact()
        {
            // Arrange
            var userId = "user1";
            var favorite = new FavoriteContact { Id = 1, Name = "Alice", Lastname = "Johnson", PhoneNumber = "987654321" };
            _contactService.AddFavorite(userId, favorite);

            // Act
            _contactService.RemoveFavorite(userId, favorite.Id);
            var favorites = _contactService.GetAllFavorites(userId);

            // Assert
            Assert.Empty(favorites);
        }
    }

    /// <summary>
    /// Enkel in-memory implementation av IContactRepository för tester.
    /// </summary>
    public class InMemoryContactRepository : IContactRepository
    {
        private readonly Dictionary<string, List<Contact>> _contacts = new();
        private readonly Dictionary<string, List<FavoriteContact>> _favorites = new();

        /// <summary>
        /// Läser alla kontakter för en specifik användare.
        /// </summary>
        public List<Contact> ReadContactsForUser(string userId)
        {
            if (!_contacts.ContainsKey(userId))
                _contacts[userId] = new List<Contact>();

            return _contacts[userId];
        }

        /// <summary>
        /// Skriver uppdaterade kontakter för en specifik användare.
        /// </summary>
        public void WriteContactsForUser(string userId, List<Contact> contacts)
        {
            _contacts[userId] = contacts;
        }

        /// <summary>
        /// Läser alla favoriter för en specifik användare.
        /// </summary>
        public List<FavoriteContact> ReadFavoritesForUser(string userId)
        {
            if (!_favorites.ContainsKey(userId))
                _favorites[userId] = new List<FavoriteContact>();

            return _favorites[userId];
        }

        /// <summary>
        /// Skriver uppdaterade favoriter för en specifik användare.
        /// </summary>
        public void WriteFavoritesForUser(string userId, List<FavoriteContact> favorites)
        {
            _favorites[userId] = favorites;
        }
    }
}
