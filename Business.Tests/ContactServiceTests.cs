using Business.CoreFiles.Models.Contacts;
using Business.Logic._1_Services.UserService;
using Business.Interfaces.Repositories;
using System.Collections.Generic;
using Xunit;

namespace Business.Tests
{
    public class ContactServiceTests
    {
        private readonly IContactRepository _contactRepository;
        private readonly ContactService _contactService;

        // Konstruktor: Initierar in-memory-repository och ContactService.
        public ContactServiceTests()
        {
            _contactRepository = new InMemoryContactRepository();
            _contactService = new ContactService(_contactRepository);
        }

        [Fact]
        public void CreateContact_ShouldAddContactToUser()
        {
            // Arrange: Skapa användar-ID och kontakt att lägga till.
            var userId = "user1";
            var contact = new Contact { Id = "1", Name = "John", Lastname = "Doe", PhoneNumber = "123456789" };

            // Act: Lägg till kontakten för användaren.
            _contactService.CreateContact(userId, contact);
            var contacts = _contactService.GetAllContacts(userId);

            // Assert: Kontrollera att kontakten har lagts till och har rätt data.
            Assert.Single(contacts);
            Assert.Equal("John", contacts[0].Name);
        }

        [Fact]
        public void GetAllContacts_ShouldReturnEmptyListIfNoContacts()
        {
            // Arrange: Definiera användar-ID utan kontakter.
            var userId = "user1";

            // Act: Hämta alla kontakter för användaren.
            var contacts = _contactService.GetAllContacts(userId);

            // Assert: Kontrollera att listan är tom.
            Assert.Empty(contacts);
        }

        [Fact]
        public void UpdateContact_ShouldUpdateExistingContact()
        {
            // Arrange: Lägg till en kontakt och förbered uppdaterad data.
            var userId = "user1";
            var contact = new Contact { Id = "1", Name = "John", Lastname = "Doe", PhoneNumber = "123456789" };
            _contactService.CreateContact(userId, contact);
            contact.Name = "Jane";
            contact.Lastname = "Smith";

            // Act: Uppdatera kontakten med den nya datan.
            _contactService.UpdateContact(userId, contact);
            var updatedContacts = _contactService.GetAllContacts(userId);

            // Assert: Kontrollera att kontakten uppdaterades korrekt.
            Assert.Single(updatedContacts);
            Assert.Equal("Jane", updatedContacts[0].Name);
        }

        [Fact]
        public void DeleteContact_ShouldRemoveContact()
        {
            // Arrange: Lägg till en kontakt att ta bort.
            var userId = "user1";
            var contact = new Contact { Id = "1", Name = "John", Lastname = "Doe", PhoneNumber = "123456789" };
            _contactService.CreateContact(userId, contact);

            // Act: Ta bort kontakten för användaren.
            _contactService.DeleteContact(userId, contact.Id);
            var contacts = _contactService.GetAllContacts(userId);

            // Assert: Kontrollera att kontakten har tagits bort.
            Assert.Empty(contacts);
        }

        [Fact]
        public void AddFavorite_ShouldAddFavoriteContact()
        {
            // Arrange: Skapa användar-ID och favoritkontakt att lägga till.
            var userId = "user1";
            var favorite = new FavoriteContact { Id = "1", Name = "Alice", Lastname = "Johnson", PhoneNumber = "987654321" };

            // Act: Lägg till favoritkontakten.
            _contactService.AddFavorite(userId, favorite);
            var favorites = _contactService.GetAllFavorites(userId);

            // Assert: Kontrollera att favoritkontakten har lagts till.
            Assert.Single(favorites);
            Assert.Equal("Alice", favorites[0].Name);
        }

        [Fact]
        public void UpdateFavorite_ShouldUpdateExistingFavorite()
        {
            // Arrange: Lägg till en favoritkontakt och förbered uppdaterad data.
            var userId = "user1";
            var favorite = new FavoriteContact { Id = "1", Name = "Alice", Lastname = "Johnson", PhoneNumber = "987654321" };
            _contactService.AddFavorite(userId, favorite);
            favorite.Name = "Updated";
            favorite.Lastname = "Favorite";

            // Act: Uppdatera favoritkontakten.
            _contactService.UpdateFavorite(userId, favorite);
            var favorites = _contactService.GetAllFavorites(userId);

            // Assert: Kontrollera att favoritkontakten uppdaterades korrekt.
            Assert.Single(favorites);
            Assert.Equal("Updated", favorites[0].Name);
        }

        [Fact]
        public void GetAllFavorites_ShouldReturnEmptyListIfNoFavorites()
        {
            // Arrange: Definiera användar-ID utan favoriter.
            var userId = "user1";

            // Act: Hämta alla favoritkontakter för användaren.
            var favorites = _contactService.GetAllFavorites(userId);

            // Assert: Kontrollera att listan är tom.
            Assert.Empty(favorites);
        }

        [Fact]
        public void RemoveFavorite_ShouldRemoveFavoriteContact()
        {
            // Arrange: Lägg till en favoritkontakt att ta bort.
            var userId = "user1";
            var favorite = new FavoriteContact { Id = "1", Name = "Alice", Lastname = "Johnson", PhoneNumber = "987654321" };
            _contactService.AddFavorite(userId, favorite);

            // Act: Ta bort favoritkontakten.
            _contactService.RemoveFavorite(userId, favorite.Id);
            var favorites = _contactService.GetAllFavorites(userId);

            // Assert: Kontrollera att favoritkontakten har tagits bort.
            Assert.Empty(favorites);
        }
    }

    public class InMemoryContactRepository : IContactRepository
    {
        private readonly Dictionary<string, List<Contact>> _contacts = new();
        private readonly Dictionary<string, List<FavoriteContact>> _favorites = new();

        public List<Contact> ReadContactsForUser(string userId)
        {
            if (!_contacts.ContainsKey(userId))
                _contacts[userId] = new List<Contact>();
            return _contacts[userId];
        }

        public void WriteContactsForUser(string userId, List<Contact> contacts)
        {
            _contacts[userId] = contacts;
        }

        public List<FavoriteContact> ReadFavoritesForUser(string userId)
        {
            if (!_favorites.ContainsKey(userId))
                _favorites[userId] = new List<FavoriteContact>();
            return _favorites[userId];
        }

        public void WriteFavoritesForUser(string userId, List<FavoriteContact> favorites)
        {
            _favorites[userId] = favorites;
        }

        public void UpdateFavorite(string userId, FavoriteContact favoriteContact)
        {
            var favorites = ReadFavoritesForUser(userId);
            var existing = favorites.Find(f => f.Id == favoriteContact.Id);
            if (existing != null)
            {
                existing.Name = favoriteContact.Name;
                existing.Lastname = favoriteContact.Lastname;
                existing.PhoneNumber = favoriteContact.PhoneNumber;
            }
        }
    }
}
