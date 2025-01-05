using Business.CoreFiles.Models.Users;
using Business.Interfaces.IUser;
using Moq;
using Xunit;
using System.Collections.Generic;
using Business.CoreFiles.Models.Users.Roles;
using Business.CoreFiles.Factory.Interfaces;
using Business.Services;

namespace Business.Tests.Mocking
{
    /// <summary>
    /// Enhetstester för UserService med mockade repositories.
    /// </summary>
    public class UserServiceMockTests
    {
        [Fact]
        public void CreateDefault_ShouldCreateUserSuccessfully()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();

            var name = "John";
            var lastname = "Doe";
            var email = "john.doe@example.com";
            var password = "password123";
            var role = "Default";

            // Act
            mockUserService.Object.CreateUser(name, lastname, email, password, role);

            // Assert
            mockUserService.Verify(service => service.CreateUser(name, lastname, email, password, role), Times.Once);
        }

        [Fact]
        public void CreateAdmin_ShouldCreateAdminUserSuccessfully()
        {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var mockUserCreateFactory = new Mock<IUserCreate>();

            var userService = new UserService(mockUserRepository.Object, mockUserCreateFactory.Object);

            var name = "John";
            var lastname = "Doe";
            var email = "john.doe@example.com";
            var password = "password123";
            var role = "Admin";

            // Skapa förväntad användare (Admin)
            var expectedUser = new Admin
            {
                Name = name,
                Lastname = lastname,
                Email = email,
               
            };

            // Mocka fabrikens skapande av användare
            mockUserCreateFactory
                .Setup(f => f.CreateUser(name, lastname, email, password, role))
                .Returns(expectedUser);

            // Act
            userService.CreateUser(name, lastname, email, password, role);

            // Assert
            mockUserCreateFactory.Verify(f => f.CreateUser(name, lastname, email, password, role), Times.Once);
            mockUserRepository.Verify(r => r.SaveUser(It.Is<Admin>(u => u.Name == name && u.Role == role)), Times.Once);
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
