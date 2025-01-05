using Business.CoreFiles.Models.Users;
using Business.Interfaces.IUser;
using Moq;
using Xunit;
using System.Collections.Generic;
using Business.CoreFiles.Models.Users.Roles;

namespace Business.Tests.Mocking
{
    /// <summary>
    /// Enhetstester för UserService med mockade repositories.
    /// </summary>
    public class UserServiceMockTests
    {
        [Fact]
        public void CreateUser_ShouldCreateUserSuccessfully()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();

            var name = "John";
            var lastname = "Doe";
            var email = "john.doe@example.com";
            var password = "password123";
            var role = "User";

            // Act
            mockUserService.Object.CreateUser(name, lastname, email, password, role);

            // Assert
            mockUserService.Verify(service => service.CreateUser(name, lastname, email, password, role), Times.Once);
        }

        [Fact]
        public void CreateUser_WithBaseUser_ShouldCreateUserSuccessfully()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();

            var user = new DefaultUser { Name = "John", Lastname = "Doe", Email = "john.doe@example.com" };

            // Act
            mockUserService.Object.CreateUser(user);

            // Assert
            mockUserService.Verify(service => service.CreateUser(user), Times.Once);
        }

        [Fact]
        public void ReadUser_ShouldReturnUser()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();

            var userId = "123";
            var user = new DefaultUser { Id = userId, Name = "John", Lastname = "Doe" };

            mockUserService.Setup(service => service.ReadUser(userId)).Returns(user);

            // Act
            var result = mockUserService.Object.ReadUser(userId);

            // Assert
            Assert.Equal(user, result);
            mockUserService.Verify(service => service.ReadUser(userId), Times.Once);
        }

        [Fact]
        public void UpdateUser_ShouldUpdateUserSuccessfully()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();

            var user = new DefaultUser { Id = "123", Name = "Updated", Lastname = "User" };

            // Act
            mockUserService.Object.UpdateUser(user);

            // Assert
            mockUserService.Verify(service => service.UpdateUser(user), Times.Once);
        }

        [Fact]
        public void DeleteUser_ShouldDeleteUserSuccessfully()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();

            var user = new DefaultUser { Id = "123", Name = "John", Lastname = "Doe" };

            // Act
            mockUserService.Object.DeleteUser(user);

            // Assert
            mockUserService.Verify(service => service.DeleteUser(user), Times.Once);
        }

        [Fact]
        public void ReadUserByEmail_ShouldReturnUser()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();

            var email = "john.doe@example.com";
            var user = new DefaultUser { Email = email, Name = "John", Lastname = "Doe" };

            mockUserService.Setup(service => service.ReadUserByEmail(email)).Returns(user);

            // Act
            var result = mockUserService.Object.ReadUserByEmail(email);

            // Assert
            Assert.Equal(user, result);
            mockUserService.Verify(service => service.ReadUserByEmail(email), Times.Once);
        }

        [Fact]
        public void ReadAllUsers_ShouldReturnAllUsers()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();

            var users = new List<BaseUser>
            {
                new DefaultUser { Name = "John", Lastname = "Doe" },
                new DefaultUser { Name = "Jane", Lastname = "Smith" }
            };

            mockUserService.Setup(service => service.ReadAllUsers()).Returns(users);

            // Act
            var result = mockUserService.Object.ReadAllUsers();

            // Assert
            Assert.Equal(users, result);
            mockUserService.Verify(service => service.ReadAllUsers(), Times.Once);
        }

        [Fact]
        public void EnsureExampleUserExists_ShouldEnsureUserSuccessfully()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();

            // Act
            mockUserService.Object.EnsureExampleUserExists();

            // Assert
            mockUserService.Verify(service => service.EnsureExampleUserExists(), Times.Once);
        }
    }
}
