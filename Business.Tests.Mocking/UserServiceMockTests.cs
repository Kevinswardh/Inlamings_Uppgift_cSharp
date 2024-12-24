using Business.Services;
using Business.CoreFiles.Models.Users;
using Business.Interfaces.IUser;
using Business.CoreFiles.Models.Contacts;
using Business.Interfaces.Repositories;
using Business.Logic._1_Services.UserService;
using Business.CoreFiles.Models.Users.Roles;
using Moq;
using Xunit;
using System.Collections.Generic;

namespace Business.Tests.Mocking
{
    /// <summary>
    /// Enhetstester för UserService och ContactService med hjälp av mockade repositories.
    /// </summary>
    public class UserServiceMockTests
    {
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
        public void ReadUser_ShouldReturnUserFromRepository()
        {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var userService = new UserService(mockUserRepository.Object, null);

            var userId = "123";
            var user = new DefaultUser { Id = userId, Name = "Test", Lastname = "User" };

            // Ställ in mocken att returnera en användare för det givna användar-ID:t
            mockUserRepository.Setup(repo => repo.GetUser(userId)).Returns(user);

            // Act
            var result = userService.ReadUser(userId);

            // Assert
            Assert.Equal(user, result);
            mockUserRepository.Verify(repo => repo.GetUser(userId), Times.Once);
        }

        [Fact]
        public void UpdateContact_ShouldCallWriteContactsOnRepository()
        {
            // Arrange
            var mockContactRepository = new Mock<IContactRepository>();
            var contactService = new ContactService(mockContactRepository.Object);

            var userId = "123";
            var contact = new Contact { Id = 1, Name = "Updated", Lastname = "Contact" };
            var contacts = new List<Contact>
            {
                new Contact { Id = 1, Name = "John", Lastname = "Doe", PhoneNumber = "123456789" }
            };

            // Ställ in mocken att returnera en lista med kontakter
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
            var contactId = 1;
            var contacts = new List<Contact>
            {
                new Contact { Id = contactId, Name = "John", Lastname = "Doe", PhoneNumber = "123456789" }
            };

            // Ställ in mocken att returnera en lista med kontakter
            mockContactRepository.Setup(repo => repo.ReadContactsForUser(userId)).Returns(contacts);

            // Act
            contactService.DeleteContact(userId, contactId);

            // Assert
            mockContactRepository.Verify(repo => repo.WriteContactsForUser(userId, It.IsAny<List<Contact>>()), Times.Once);
        }

        [Fact]
        public void ReadUserByEmail_ShouldReturnUserFromRepository()
        {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var userService = new UserService(mockUserRepository.Object, null);

            var email = "test@example.com";
            var user = new DefaultUser { Email = email, Name = "Test", Lastname = "User" };

            // Ställ in mocken att returnera en användare för den givna e-postadressen
            mockUserRepository.Setup(repo => repo.GetUserByEmail(email)).Returns(user);

            // Act
            var result = userService.ReadUserByEmail(email);

            // Assert
            Assert.Equal(user, result);
            mockUserRepository.Verify(repo => repo.GetUserByEmail(email), Times.Once);
        }

        [Fact]
        public void ReadAllUsers_ShouldReturnUsersFromRepository()
        {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var userService = new UserService(mockUserRepository.Object, null);

            var users = new List<BaseUser>
            {
                new DefaultUser { Name = "John", Lastname = "Doe" },
                new DefaultUser { Name = "Jane", Lastname = "Smith" }
            };

            // Ställ in mocken att returnera en lista med användare
            mockUserRepository.Setup(repo => repo.GetAllUsers()).Returns(users);

            // Act
            var result = userService.ReadAllUsers();

            // Assert
            Assert.Equal(users, result);
            mockUserRepository.Verify(repo => repo.GetAllUsers(), Times.Once);
        }
    }
}
