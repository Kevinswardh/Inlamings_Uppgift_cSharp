using Business.CoreFiles.Models.Users;
using Business.Services;
using Business.Logic._2_Repositories;
using Business.CoreFiles.Factory;
using Business.Interfaces.IUser;
using Xunit;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Business.CoreFiles.Models.Users.Roles;

namespace Business.Tests
{
    public class UserServiceTests
    {
        private const string TestFilePath = "test_users.json";
        private UserService _userService;

        public UserServiceTests()
        {
            if (File.Exists(TestFilePath))
            {
                File.Delete(TestFilePath);
            }

            var jsonRepository = new JsonRepository(TestFilePath, new ExampleUserCreate());
            var userRepository = new UserRepository(jsonRepository);
            _userService = new UserService(userRepository, new UserCreate());
        }

        [Fact]
        public void CreateUser_ShouldCreateUserSuccessfully()
        {
            // Arrange: Förbered data för en ny användare.
            var name = "John";
            var lastname = "Doe";
            var email = "john.doe@example.com";
            var password = "password123";
            var role = "Default";

            // Act: Skapa en ny användare med ovanstående data.
            _userService.CreateUser(name, lastname, email, password, role);
            var createdUser = _userService.ReadUserByEmail(email);

            // Assert: Kontrollera att användaren skapades korrekt och att alla fält är korrekta.
            Assert.NotNull(createdUser);
            Assert.Equal(name, createdUser.Name);
            Assert.Equal(lastname, createdUser.Lastname);
            Assert.Equal(email, createdUser.Email);
        }

        [Fact]
        public void CreateUser_WithBaseUser_ShouldCreateUserSuccessfully()
        {
            // Arrange: Skapa en instans av DefaultUser med testdata.
            var user = new DefaultUser
            {
                Id = "1",
                Name = "Jane",
                Lastname = "Smith",
                Email = "jane.smith@example.com"
            };

            // Act: Lägg till användaren i systemet.
            _userService.CreateUser(user);
            var createdUser = _userService.ReadUserByEmail("jane.smith@example.com");

            // Assert: Verifiera att användaren har skapats och att fälten matchar.
            Assert.NotNull(createdUser);
            Assert.Equal("Jane", createdUser.Name);
            Assert.Equal("Smith", createdUser.Lastname);
        }

        [Fact]
        public void ReadUser_ShouldReturnUserById()
        {
            // Arrange: Skapa och spara en ny användare i systemet.
            var user = new DefaultUser
            {
                Id = "123",
                Name = "Alice",
                Lastname = "Brown",
                Email = "alice.brown@example.com"
            };
            _userService.CreateUser(user);

            // Act: Hämta användaren med dess ID.
            var retrievedUser = _userService.ReadUser("123");

            // Assert: Kontrollera att rätt användare hämtades och att fälten är korrekta.
            Assert.NotNull(retrievedUser);
            Assert.Equal("Alice", retrievedUser.Name);
        }

        [Fact]
        public void UpdateUser_ShouldUpdateExistingUser()
        {
            // Arrange: Skapa en användare och uppdatera dess namn.
            var user = new DefaultUser
            {
                Id = "1",
                Name = "Charlie",
                Lastname = "Green",
                Email = "charlie.green@example.com"
            };
            _userService.CreateUser(user);
            user.Name = "CharlieUpdated";

            // Act: Uppdatera användaren i systemet.
            _userService.UpdateUser(user);
            var updatedUser = _userService.ReadUserByEmail("charlie.green@example.com");

            // Assert: Kontrollera att användarens namn har uppdaterats korrekt.
            Assert.NotNull(updatedUser);
            Assert.Equal("CharlieUpdated", updatedUser.Name);
        }

        [Fact]
        public void DeleteUser_ShouldRemoveUser()
        {
            // Arrange: Skapa en användare som ska tas bort.
            var user = new DefaultUser
            {
                Id = "2",
                Name = "David",
                Lastname = "White",
                Email = "david.white@example.com"
            };
            _userService.CreateUser(user);

            // Act: Ta bort användaren från systemet.
            _userService.DeleteUser(user);
            var deletedUser = _userService.ReadUserByEmail("david.white@example.com");

            // Assert: Kontrollera att användaren inte längre finns i systemet.
            Assert.Null(deletedUser);
        }

        [Fact]
        public void ReadAllUsers_ShouldReturnAllUsers()
        {
            // Arrange: Skapa två användare i systemet.
            var user1 = new DefaultUser { Id = "1", Name = "Emma", Email = "emma@example.com" };
            var user2 = new DefaultUser { Id = "2", Name = "Frank", Email = "frank@example.com" };
            _userService.CreateUser(user1);
            _userService.CreateUser(user2);

            // Act: Hämta alla användare i systemet.
            var users = _userService.ReadAllUsers();

            // Assert: Kontrollera att alla användare hämtades korrekt och att deras data matchar.
            Assert.Equal(2, users.Count);
            Assert.Contains(users, u => u.Email == "emma@example.com");
            Assert.Contains(users, u => u.Email == "frank@example.com");
        }

        [Fact]
        public void EnsureExampleUserExists_ShouldCreateExampleUserIfNotExists()
        {
            // Act: Kontrollera att exempelanvändaren skapas om den inte redan finns.
            _userService.EnsureExampleUserExists();
            var exampleUser = _userService.ReadUserByEmail("x@x.xx");

            // Assert: Verifiera att exempelanvändaren har skapats korrekt.
            Assert.NotNull(exampleUser);
            Assert.Equal("ExampleName", exampleUser.Name);
        }

        [Fact]
        public void EnsureExampleUserExists_ShouldNotDuplicateExampleUser()
        {
            // Act: Anropa funktionen två gånger för att skapa exempelanvändaren.
            _userService.EnsureExampleUserExists();
            _userService.EnsureExampleUserExists();
            var users = _userService.ReadAllUsers();

            // Assert: Kontrollera att endast en instans av exempelanvändaren finns.
            Assert.Single(users.Where(u => u.Email == "x@x.xx"));
        }
    }
}
