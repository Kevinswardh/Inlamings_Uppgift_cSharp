using Business.Services;
using Business.CoreFiles.Models.Contacts;
using Business.Interfaces.Repositories;
using Moq;
using Xunit;
using System.Collections.Generic;
using Business.Logic._1_Services.UserService;

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

            // Ställ in mocken att returnera kontakterna för det givna användar-ID:t
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

            // Se till att ReadContactsForUser returnerar en tom lista
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

            // Se till att ReadContactsForUser returnerar en lista med kontakter
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

            // Se till att ReadContactsForUser returnerar en lista med kontakter
            mockContactRepository.Setup(repo => repo.ReadContactsForUser(userId)).Returns(contacts);

            // Act
            contactService.DeleteContact(userId, contactId);

            // Assert
            mockContactRepository.Verify(repo => repo.WriteContactsForUser(userId, It.IsAny<List<Contact>>()), Times.Once);
        }
    }
}
