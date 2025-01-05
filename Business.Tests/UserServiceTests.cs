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
            var name = "John";
            var lastname = "Doe";
            var email = "john.doe@example.com";
            var password = "password123";
            var role = "Default";

            _userService.CreateUser(name, lastname, email, password, role);
            var createdUser = _userService.ReadUserByEmail(email);

            Assert.NotNull(createdUser);
            Assert.Equal(name, createdUser.Name);
            Assert.Equal(lastname, createdUser.Lastname);
            Assert.Equal(email, createdUser.Email);
        }

        [Fact]
        public void CreateUser_WithBaseUser_ShouldCreateUserSuccessfully()
        {
            var user = new DefaultUser
            {
                Id = "1",
                Name = "Jane",
                Lastname = "Smith",
                Email = "jane.smith@example.com"
            };

            _userService.CreateUser(user);
            var createdUser = _userService.ReadUserByEmail("jane.smith@example.com");

            Assert.NotNull(createdUser);
            Assert.Equal("Jane", createdUser.Name);
            Assert.Equal("Smith", createdUser.Lastname);
        }

        [Fact]
        public void ReadUser_ShouldReturnUserById()
        {
            var user = new DefaultUser
            {
                Id = "123",
                Name = "Alice",
                Lastname = "Brown",
                Email = "alice.brown@example.com"
            };
            _userService.CreateUser(user);

            var retrievedUser = _userService.ReadUser("123");

            Assert.NotNull(retrievedUser);
            Assert.Equal("Alice", retrievedUser.Name);
        }

        [Fact]
        public void UpdateUser_ShouldUpdateExistingUser()
        {
            var user = new DefaultUser
            {
                Id = "1",
                Name = "Charlie",
                Lastname = "Green",
                Email = "charlie.green@example.com"
            };
            _userService.CreateUser(user);

            user.Name = "CharlieUpdated";
            _userService.UpdateUser(user);
            var updatedUser = _userService.ReadUserByEmail("charlie.green@example.com");

            Assert.NotNull(updatedUser);
            Assert.Equal("CharlieUpdated", updatedUser.Name);
        }

        [Fact]
        public void DeleteUser_ShouldRemoveUser()
        {
            var user = new DefaultUser
            {
                Id = "2",
                Name = "David",
                Lastname = "White",
                Email = "david.white@example.com"
            };
            _userService.CreateUser(user);

            _userService.DeleteUser(user);
            var deletedUser = _userService.ReadUserByEmail("david.white@example.com");

            Assert.Null(deletedUser);
        }

        [Fact]
        public void ReadAllUsers_ShouldReturnAllUsers()
        {
            var user1 = new DefaultUser { Id = "1", Name = "Emma", Email = "emma@example.com" };
            var user2 = new DefaultUser { Id = "2", Name = "Frank", Email = "frank@example.com" };
            _userService.CreateUser(user1);
            _userService.CreateUser(user2);

            var users = _userService.ReadAllUsers();

            Assert.Equal(2, users.Count);
            Assert.Contains(users, u => u.Email == "emma@example.com");
            Assert.Contains(users, u => u.Email == "frank@example.com");
        }

        [Fact]
        public void EnsureExampleUserExists_ShouldCreateExampleUserIfNotExists()
        {
            _userService.EnsureExampleUserExists();
            var exampleUser = _userService.ReadUserByEmail("x@x.xx");

            Assert.NotNull(exampleUser);
            Assert.Equal("ExampleName", exampleUser.Name);
        }

        [Fact]
        public void EnsureExampleUserExists_ShouldNotDuplicateExampleUser()
        {
            _userService.EnsureExampleUserExists();
            _userService.EnsureExampleUserExists();
            var users = _userService.ReadAllUsers();

            Assert.Single(users.Where(u => u.Email == "x@x.xx"));
        }
    }
}
