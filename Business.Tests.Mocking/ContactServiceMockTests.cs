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
            // Arrange
            var mockContactRepository = new Mock<IContactRepository>();
            var contactService = new ContactService(mockContactRepository.Object);

            var userId = "123";
            var contacts = new List<Contact>
            {
                new Contact { Name = "John", Lastname = "Doe", PhoneNumber = "123456789" },
                new Contact { Name = "Jane", Lastname = "Smith", PhoneNumber = "987654321" }
            };

            mockContactRepository.Setup(repo => repo.ReadContactsForUser(userId)).Returns(contacts);

            // Act
            var result = contactService.GetAllContacts(userId);

            // Assert
            Assert.Equal(contacts, result);
            mockContactRepository.Verify(repo => repo.ReadContactsForUser(userId), Times.Once);
        }

        [Fact]
        public void CreateContact_ShouldCallWriteContactsOnRepository()
        {
            // Arrange
            var mockContactRepository = new Mock<IContactRepository>();
            var contactService = new ContactService(mockContactRepository.Object);

            var userId = "123";
            var contact = new Contact { Name = "John", Lastname = "Doe", PhoneNumber = "123456789" };

            mockContactRepository.Setup(repo => repo.ReadContactsForUser(userId)).Returns(new List<Contact>());

            // Act
            contactService.CreateContact(userId, contact);

            // Assert
            mockContactRepository.Verify(repo => repo.WriteContactsForUser(userId, It.IsAny<List<Contact>>()), Times.Once);
        }

        [Fact]
        public void UpdateContact_ShouldCallWriteContactsOnRepository()
        {
            // Arrange
            var mockContactRepository = new Mock<IContactRepository>();
            var contactService = new ContactService(mockContactRepository.Object);

            var userId = "123";
            var contact = new Contact { Id = "1", Name = "Updated", Lastname = "Contact" };
            var contacts = new List<Contact>
            {
                new Contact { Id = "1", Name = "John", Lastname = "Doe", PhoneNumber = "123456789" }
            };

            mockContactRepository.Setup(repo => repo.ReadContactsForUser(userId)).Returns(contacts);

            // Act
            contactService.UpdateContact(userId, contact);

            // Assert
            mockContactRepository.Verify(repo => repo.WriteContactsForUser(userId, It.IsAny<List<Contact>>()), Times.Once);
        }

        [Fact]
        public void DeleteContact_ShouldCallWriteContactsOnRepository()
        {
            // Arrange
            var mockContactRepository = new Mock<IContactRepository>();
            var contactService = new ContactService(mockContactRepository.Object);

            var userId = "123";
            var contactId = "1";
            var contacts = new List<Contact>
            {
                new Contact { Id = contactId, Name = "John", Lastname = "Doe", PhoneNumber = "123456789" }
            };

            mockContactRepository.Setup(repo => repo.ReadContactsForUser(userId)).Returns(contacts);

            // Act
            contactService.DeleteContact(userId, contactId);

            // Assert
            mockContactRepository.Verify(repo => repo.WriteContactsForUser(userId, It.IsAny<List<Contact>>()), Times.Once);
        }

        [Fact]
        public void AddFavorite_ShouldCallWriteFavoritesOnRepository()
        {
            // Arrange
            var mockContactRepository = new Mock<IContactRepository>();
            var contactService = new ContactService(mockContactRepository.Object);

            var userId = "123";
            var favoriteContact = new FavoriteContact { Id = "1", Name = "Favorite John" };

            mockContactRepository.Setup(repo => repo.ReadFavoritesForUser(userId)).Returns(new List<FavoriteContact>());

            // Act
            contactService.AddFavorite(userId, favoriteContact);

            // Assert
            mockContactRepository.Verify(repo => repo.WriteFavoritesForUser(userId, It.IsAny<List<FavoriteContact>>()), Times.Once);
        }

        [Fact]
        public void UpdateFavorite_ShouldCallWriteFavoritesOnRepository()
        {
            // Arrange
            var mockContactRepository = new Mock<IContactRepository>();
            var contactService = new ContactService(mockContactRepository.Object);

            var userId = "123";
            var favoriteContact = new FavoriteContact { Id = "1", Name = "Updated Favorite" };
            var favorites = new List<FavoriteContact>
            {
                new FavoriteContact { Id = "1", Name = "Favorite John" }
            };

            mockContactRepository.Setup(repo => repo.ReadFavoritesForUser(userId)).Returns(favorites);

            // Act
            contactService.UpdateFavorite(userId, favoriteContact);

            // Assert
            mockContactRepository.Verify(repo => repo.WriteFavoritesForUser(userId, It.IsAny<List<FavoriteContact>>()), Times.Once);
        }

        [Fact]
        public void GetAllFavorites_ShouldReturnFavoritesFromRepository()
        {
            // Arrange
            var mockContactRepository = new Mock<IContactRepository>();
            var contactService = new ContactService(mockContactRepository.Object);

            var userId = "123";
            var favorites = new List<FavoriteContact>
            {
                new FavoriteContact { Id = "1", Name = "Favorite John" },
                new FavoriteContact { Id = "2", Name = "Favorite Jane" }
            };

            mockContactRepository.Setup(repo => repo.ReadFavoritesForUser(userId)).Returns(favorites);

            // Act
            var result = contactService.GetAllFavorites(userId);

            // Assert
            Assert.Equal(favorites, result);
            mockContactRepository.Verify(repo => repo.ReadFavoritesForUser(userId), Times.Once);
        }

        [Fact]
        public void RemoveFavorite_ShouldCallWriteFavoritesOnRepository()
        {
            // Arrange
            var mockContactRepository = new Mock<IContactRepository>();
            var contactService = new ContactService(mockContactRepository.Object);

            var userId = "123";
            var favoriteId = "1";
            var favorites = new List<FavoriteContact>
            {
                new FavoriteContact { Id = favoriteId, Name = "Favorite John" }
            };

            mockContactRepository.Setup(repo => repo.ReadFavoritesForUser(userId)).Returns(favorites);

            // Act
            contactService.RemoveFavorite(userId, favoriteId);

            // Assert
            mockContactRepository.Verify(repo => repo.WriteFavoritesForUser(userId, It.IsAny<List<FavoriteContact>>()), Times.Once);
        }
    }
}
