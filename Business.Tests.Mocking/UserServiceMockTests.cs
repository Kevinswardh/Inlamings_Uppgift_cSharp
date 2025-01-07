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
            // Arrange: Mocka IUserService och förbered data för en ny användare.
            var mockUserService = new Mock<IUserService>();
            var name = "John";
            var lastname = "Doe";
            var email = "john.doe@example.com";
            var password = "password123";
            var role = "Default";

            // Act: Anropa CreateUser-metoden på den mockade tjänsten.
            mockUserService.Object.CreateUser(name, lastname, email, password, role);

            // Assert: Kontrollera att CreateUser-metoden anropades exakt en gång med rätt parametrar.
            mockUserService.Verify(service => service.CreateUser(name, lastname, email, password, role), Times.Once);
        }

        [Fact]
        public void CreateAdmin_ShouldCreateAdminUserSuccessfully()
        {
            // Arrange: Mocka IUserRepository och IUserCreate, och förbered data för att skapa en Admin-användare.
            var mockUserRepository = new Mock<IUserRepository>();
            var mockUserCreateFactory = new Mock<IUserCreate>();
            var userService = new UserService(mockUserRepository.Object, mockUserCreateFactory.Object);

            var name = "John";
            var lastname = "Doe";
            var email = "john.doe@example.com";
            var password = "password123";
            var role = "Admin";

            var expectedUser = new Admin
            {
                Name = name,
                Lastname = lastname,
                Email = email
            };

            mockUserCreateFactory
                .Setup(f => f.CreateUser(name, lastname, email, password, role))
                .Returns(expectedUser);

            // Act: Skapa en Admin-användare med UserService.
            userService.CreateUser(name, lastname, email, password, role);

            // Assert: Verifiera att användaren skapades och sparades korrekt.
            mockUserCreateFactory.Verify(f => f.CreateUser(name, lastname, email, password, role), Times.Once);
            mockUserRepository.Verify(r => r.SaveUser(It.Is<Admin>(u => u.Name == name && u.Role == role)), Times.Once);
        }

        [Fact]
        public void CreateUser_WithBaseUser_ShouldCreateUserSuccessfully()
        {
            // Arrange: Mocka IUserService och skapa en BaseUser-instans.
            var mockUserService = new Mock<IUserService>();
            var user = new DefaultUser { Name = "John", Lastname = "Doe", Email = "john.doe@example.com" };

            // Act: Skapa användaren via CreateUser-metoden.
            mockUserService.Object.CreateUser(user);

            // Assert: Verifiera att CreateUser-metoden anropades en gång med rätt användare.
            mockUserService.Verify(service => service.CreateUser(user), Times.Once);
        }

        [Fact]
        public void ReadUser_ShouldReturnUser()
        {
            // Arrange: Mocka IUserService och konfigurera ReadUser för att returnera en användare.
            var mockUserService = new Mock<IUserService>();
            var userId = "123";
            var user = new DefaultUser { Id = userId, Name = "John", Lastname = "Doe" };

            mockUserService.Setup(service => service.ReadUser(userId)).Returns(user);

            // Act: Hämta användaren med hjälp av ReadUser-metoden.
            var result = mockUserService.Object.ReadUser(userId);

            // Assert: Kontrollera att rätt användare returnerades och att metoden anropades en gång.
            Assert.Equal(user, result);
            mockUserService.Verify(service => service.ReadUser(userId), Times.Once);
        }

        [Fact]
        public void UpdateUser_ShouldUpdateUserSuccessfully()
        {
            // Arrange: Mocka IUserService och skapa en användare att uppdatera.
            var mockUserService = new Mock<IUserService>();
            var user = new DefaultUser { Id = "123", Name = "Updated", Lastname = "User" };

            // Act: Uppdatera användaren med UpdateUser-metoden.
            mockUserService.Object.UpdateUser(user);

            // Assert: Verifiera att UpdateUser-metoden anropades en gång med rätt användare.
            mockUserService.Verify(service => service.UpdateUser(user), Times.Once);
        }

        [Fact]
        public void DeleteUser_ShouldDeleteUserSuccessfully()
        {
            // Arrange: Mocka IUserService och skapa en användare att ta bort.
            var mockUserService = new Mock<IUserService>();
            var user = new DefaultUser { Id = "123", Name = "John", Lastname = "Doe" };

            // Act: Ta bort användaren med DeleteUser-metoden.
            mockUserService.Object.DeleteUser(user);

            // Assert: Verifiera att DeleteUser-metoden anropades en gång med rätt användare.
            mockUserService.Verify(service => service.DeleteUser(user), Times.Once);
        }

        [Fact]
        public void ReadUserByEmail_ShouldReturnUser()
        {
            // Arrange: Mocka IUserService och konfigurera ReadUserByEmail för att returnera en användare.
            var mockUserService = new Mock<IUserService>();
            var email = "john.doe@example.com";
            var user = new DefaultUser { Email = email, Name = "John", Lastname = "Doe" };

            mockUserService.Setup(service => service.ReadUserByEmail(email)).Returns(user);

            // Act: Hämta användaren med hjälp av ReadUserByEmail-metoden.
            var result = mockUserService.Object.ReadUserByEmail(email);

            // Assert: Kontrollera att rätt användare returnerades och att metoden anropades en gång.
            Assert.Equal(user, result);
            mockUserService.Verify(service => service.ReadUserByEmail(email), Times.Once);
        }

        [Fact]
        public void ReadAllUsers_ShouldReturnAllUsers()
        {
            // Arrange: Mocka IUserService och konfigurera ReadAllUsers för att returnera en lista med användare.
            var mockUserService = new Mock<IUserService>();
            var users = new List<BaseUser>
            {
                new DefaultUser { Name = "John", Lastname = "Doe" },
                new DefaultUser { Name = "Jane", Lastname = "Smith" }
            };

            mockUserService.Setup(service => service.ReadAllUsers()).Returns(users);

            // Act: Hämta alla användare med ReadAllUsers-metoden.
            var result = mockUserService.Object.ReadAllUsers();

            // Assert: Kontrollera att alla användare returnerades korrekt och att metoden anropades en gång.
            Assert.Equal(users, result);
            mockUserService.Verify(service => service.ReadAllUsers(), Times.Once);
        }

        [Fact]
        public void EnsureExampleUserExists_ShouldEnsureUserSuccessfully()
        {
            // Arrange: Mocka IUserService.
            var mockUserService = new Mock<IUserService>();

            // Act: Kontrollera att exempelanvändaren skapas om den inte redan finns.
            mockUserService.Object.EnsureExampleUserExists();

            // Assert: Verifiera att EnsureExampleUserExists-metoden anropades en gång.
            mockUserService.Verify(service => service.EnsureExampleUserExists(), Times.Once);
        }
    }
}
