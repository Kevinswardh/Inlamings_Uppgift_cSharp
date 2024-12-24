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
    /// <summary>
    /// Enhetstester för UserService.
    /// </summary>
    public class UserServiceTests
    {
        private const string TestFilePath = "test_users.json";
        private UserService _userService;

        /// <summary>
        /// Konstruktor som initierar UserService och rensar testfilen före varje test.
        /// </summary>
        public UserServiceTests()
        {
            // Rensa testfilen före varje test
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
            // Arrange
            string name = "John";
            string lastname = "Doe";
            string email = "john.doe@example.com";
            string password = "password123";
            string role = "Default";

            // Act
            _userService.CreateUser(name, lastname, email, password, role);
            var createdUser = _userService.ReadUserByEmail(email);

            // Assert
            Assert.NotNull(createdUser);
            Assert.Equal(name, createdUser.Name);
            Assert.Equal(lastname, createdUser.Lastname);
            Assert.Equal(email, createdUser.Email);
        }

        [Fact]
        public void CreateUser_WithBaseUser_ShouldCreateUserSuccessfully()
        {
            // Arrange
            var user = new DefaultUser
            {
                Id = "1",
                Name = "Jane",
                Lastname = "Smith",
                Email = "jane.smith@example.com"
            };

            // Act
            _userService.CreateUser(user);
            var createdUser = _userService.ReadUserByEmail("jane.smith@example.com");

            // Assert
            Assert.NotNull(createdUser);
            Assert.Equal("Jane", createdUser.Name);
            Assert.Equal("Smith", createdUser.Lastname);
        }

        [Fact]
        public void ReadUser_ShouldReturnUserById()
        {
            // Arrange
            var user = new DefaultUser
            {
                Id = "123",
                Name = "Alice",
                Lastname = "Brown",
                Email = "alice.brown@example.com"
            };
            _userService.CreateUser(user);

            // Act
            var retrievedUser = _userService.ReadUser("123");

            // Assert
            Assert.NotNull(retrievedUser);
            Assert.Equal("Alice", retrievedUser.Name);
        }

        [Fact]
        public void UpdateUser_ShouldUpdateExistingUser()
        {
            // Arrange
            var user = new DefaultUser
            {
                Id = "1",
                Name = "Charlie",
                Lastname = "Green",
                Email = "charlie.green@example.com"
            };
            _userService.CreateUser(user);

            // Act
            user.Name = "CharlieUpdated";
            _userService.UpdateUser(user);
            var updatedUser = _userService.ReadUserByEmail("charlie.green@example.com");

            // Assert
            Assert.NotNull(updatedUser);
            Assert.Equal("CharlieUpdated", updatedUser.Name);
        }

        [Fact]
        public void DeleteUser_ShouldRemoveUser()
        {
            // Arrange
            var user = new DefaultUser
            {
                Id = "2",
                Name = "David",
                Lastname = "White",
                Email = "david.white@example.com"
            };
            _userService.CreateUser(user);

            // Act
            _userService.DeleteUser(user);
            var deletedUser = _userService.ReadUserByEmail("david.white@example.com");

            // Assert
            Assert.Null(deletedUser);
        }

        [Fact]
        public void ReadAllUsers_ShouldReturnAllUsers()
        {
            // Arrange
            var user1 = new DefaultUser { Id = "1", Name = "Emma", Email = "emma@example.com" };
            var user2 = new DefaultUser { Id = "2", Name = "Frank", Email = "frank@example.com" };
            _userService.CreateUser(user1);
            _userService.CreateUser(user2);

            // Act
            var users = _userService.ReadAllUsers();

            // Assert
            Assert.Equal(2, users.Count);
            Assert.Contains(users, u => u.Email == "emma@example.com");
            Assert.Contains(users, u => u.Email == "frank@example.com");
        }

        [Fact]
        public void EnsureExampleUserExists_ShouldCreateExampleUserIfNotExists()
        {
            // Act
            _userService.EnsureExampleUserExists();
            var exampleUser = _userService.ReadUserByEmail("x@x.xx");

            // Assert
            Assert.NotNull(exampleUser);
            Assert.Equal("ExampleName", exampleUser.Name);
        }
    }
}
