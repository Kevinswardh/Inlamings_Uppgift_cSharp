using Business.CoreFiles.Models.Contacts;
using Business.Interfaces.Repositories;
using Business.Logic._1_Services.UserService;
using Moq;
using Xunit;
using System.Collections.Generic;

namespace Business.Tests.Mocking
{
    /// <summary>
    /// Enhetstester för ContactService med hjälp av mock för IContactRepository.
    /// </summary>
    public class ContactServiceMockTests
    {
        [Fact]
        public void GetAllContacts_ShouldReturnContactsFromRepository()
        {
            // Arrange: Mocka IContactRepository och förbered kontaktdata för en användare.
            var mockContactRepository = new Mock<IContactRepository>();
            var contactService = new ContactService(mockContactRepository.Object);

            var userId = "123";
            var contacts = new List<Contact>
            {
                new Contact { Name = "John", Lastname = "Doe", PhoneNumber = "123456789" },
                new Contact { Name = "Jane", Lastname = "Smith", PhoneNumber = "987654321" }
            };

            mockContactRepository.Setup(repo => repo.ReadContactsForUser(userId)).Returns(contacts);

            // Act: Hämta alla kontakter för användaren.
            var result = contactService.GetAllContacts(userId);

            // Assert: Verifiera att kontakterna returneras korrekt och att metoden anropades en gång.
            Assert.Equal(contacts, result);
            mockContactRepository.Verify(repo => repo.ReadContactsForUser(userId), Times.Once);
        }

        [Fact]
        public void CreateContact_ShouldCallWriteContactsOnRepository()
        {
            // Arrange: Mocka IContactRepository och skapa kontaktdata.
            var mockContactRepository = new Mock<IContactRepository>();
            var contactService = new ContactService(mockContactRepository.Object);

            var userId = "123";
            var contact = new Contact { Name = "John", Lastname = "Doe", PhoneNumber = "123456789" };

            mockContactRepository.Setup(repo => repo.ReadContactsForUser(userId)).Returns(new List<Contact>());

            // Act: Skapa en ny kontakt för användaren.
            contactService.CreateContact(userId, contact);

            // Assert: Verifiera att WriteContacts-metoden anropades en gång.
            mockContactRepository.Verify(repo => repo.WriteContactsForUser(userId, It.IsAny<List<Contact>>()), Times.Once);
        }

        [Fact]
        public void UpdateContact_ShouldCallWriteContactsOnRepository()
        {
            // Arrange: Mocka IContactRepository och förbered kontaktdata att uppdatera.
            var mockContactRepository = new Mock<IContactRepository>();
            var contactService = new ContactService(mockContactRepository.Object);

            var userId = "123";
            var contact = new Contact { Id = "1", Name = "Updated", Lastname = "Contact" };
            var contacts = new List<Contact>
            {
                new Contact { Id = "1", Name = "John", Lastname = "Doe", PhoneNumber = "123456789" }
            };

            mockContactRepository.Setup(repo => repo.ReadContactsForUser(userId)).Returns(contacts);

            // Act: Uppdatera kontakten i systemet.
            contactService.UpdateContact(userId, contact);

            // Assert: Verifiera att WriteContacts-metoden anropades en gång.
            mockContactRepository.Verify(repo => repo.WriteContactsForUser(userId, It.IsAny<List<Contact>>()), Times.Once);
        }

        [Fact]
        public void DeleteContact_ShouldCallWriteContactsOnRepository()
        {
            // Arrange: Mocka IContactRepository och förbered kontaktdata att ta bort.
            var mockContactRepository = new Mock<IContactRepository>();
            var contactService = new ContactService(mockContactRepository.Object);

            var userId = "123";
            var contactId = "1";
            var contacts = new List<Contact>
            {
                new Contact { Id = contactId, Name = "John", Lastname = "Doe", PhoneNumber = "123456789" }
            };

            mockContactRepository.Setup(repo => repo.ReadContactsForUser(userId)).Returns(contacts);

            // Act: Ta bort kontakten från systemet.
            contactService.DeleteContact(userId, contactId);

            // Assert: Verifiera att WriteContacts-metoden anropades en gång.
            mockContactRepository.Verify(repo => repo.WriteContactsForUser(userId, It.IsAny<List<Contact>>()), Times.Once);
        }

        [Fact]
        public void AddFavorite_ShouldCallWriteFavoritesOnRepository()
        {
            // Arrange: Mocka IContactRepository och förbered favoritdata.
            var mockContactRepository = new Mock<IContactRepository>();
            var contactService = new ContactService(mockContactRepository.Object);

            var userId = "123";
            var favoriteContact = new FavoriteContact { Id = "1", Name = "Favorite John" };

            mockContactRepository.Setup(repo => repo.ReadFavoritesForUser(userId)).Returns(new List<FavoriteContact>());

            // Act: Lägg till favoritkontakten i systemet.
            contactService.AddFavorite(userId, favoriteContact);

            // Assert: Verifiera att WriteFavorites-metoden anropades en gång.
            mockContactRepository.Verify(repo => repo.WriteFavoritesForUser(userId, It.IsAny<List<FavoriteContact>>()), Times.Once);
        }

        [Fact]
        public void UpdateFavorite_ShouldCallWriteFavoritesOnRepository()
        {
            // Arrange: Mocka IContactRepository och förbered favoritdata att uppdatera.
            var mockContactRepository = new Mock<IContactRepository>();
            var contactService = new ContactService(mockContactRepository.Object);

            var userId = "123";
            var favoriteContact = new FavoriteContact { Id = "1", Name = "Updated Favorite" };
            var favorites = new List<FavoriteContact>
            {
                new FavoriteContact { Id = "1", Name = "Favorite John" }
            };

            mockContactRepository.Setup(repo => repo.ReadFavoritesForUser(userId)).Returns(favorites);

            // Act: Uppdatera favoritkontakten i systemet.
            contactService.UpdateFavorite(userId, favoriteContact);

            // Assert: Verifiera att WriteFavorites-metoden anropades en gång.
            mockContactRepository.Verify(repo => repo.WriteFavoritesForUser(userId, It.IsAny<List<FavoriteContact>>()), Times.Once);
        }

        [Fact]
        public void GetAllFavorites_ShouldReturnFavoritesFromRepository()
        {
            // Arrange: Mocka IContactRepository och förbered favoritdata.
            var mockContactRepository = new Mock<IContactRepository>();
            var contactService = new ContactService(mockContactRepository.Object);

            var userId = "123";
            var favorites = new List<FavoriteContact>
            {
                new FavoriteContact { Id = "1", Name = "Favorite John" },
                new FavoriteContact { Id = "2", Name = "Favorite Jane" }
            };

            mockContactRepository.Setup(repo => repo.ReadFavoritesForUser(userId)).Returns(favorites);

            // Act: Hämta alla favoritkontakter för användaren.
            var result = contactService.GetAllFavorites(userId);

            // Assert: Verifiera att favoritkontakterna returnerades korrekt och att metoden anropades en gång.
            Assert.Equal(favorites, result);
            mockContactRepository.Verify(repo => repo.ReadFavoritesForUser(userId), Times.Once);
        }

        [Fact]
        public void RemoveFavorite_ShouldCallWriteFavoritesOnRepository()
        {
            // Arrange: Mocka IContactRepository och förbered favoritdata att ta bort.
            var mockContactRepository = new Mock<IContactRepository>();
            var contactService = new ContactService(mockContactRepository.Object);

            var userId = "123";
            var favoriteId = "1";
            var favorites = new List<FavoriteContact>
            {
                new FavoriteContact { Id = favoriteId, Name = "Favorite John" }
            };

            mockContactRepository.Setup(repo => repo.ReadFavoritesForUser(userId)).Returns(favorites);

            // Act: Ta bort favoritkontakten från systemet.
            contactService.RemoveFavorite(userId, favoriteId);

            // Assert: Verifiera att WriteFavorites-metoden anropades en gång.
            mockContactRepository.Verify(repo => repo.WriteFavoritesForUser(userId, It.IsAny<List<FavoriteContact>>()), Times.Once);
        }
    }
}
