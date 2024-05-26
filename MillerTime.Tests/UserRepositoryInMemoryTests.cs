using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MillerTime.DAL.Context;
using MillerTime.Models.DBModels;
using MillerTime.DAL.Repositories;
using MillerTime.DAL.Repositories.Interfaces;
using Xunit;

namespace MillerTime.Tests
{
    [Collection("Sequential")]
    public class UserRepositoryInMemoryTests : IDisposable
    {
        private readonly IUserRepository _classBeingTested;
        private readonly DbContextOptions<MillerTimeContext> _contextOptions;
        private readonly MillerTimeContext _context;

        public UserRepositoryInMemoryTests()
        {
            var services = new ServiceCollection();
            _contextOptions = new DbContextOptionsBuilder<MillerTimeContext>()
                .UseSqlite("DataSource=file::memory:?cached=shared;Pooling=False").Options;
            _context = new MillerTimeContext(_contextOptions);
            _classBeingTested = new UserRepository(Helpers.GetConfigFile(), _context);
            _context.Database.OpenConnection();
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _context.Database.CloseConnection();
            _context.Dispose();
        }

        [Fact]
        public void GetAllUsers_ReturnsAllUsers_Correct()
        {
            var users = new List<User>
            {
                {
                    Helpers.CreateUser()
                },
                {
                    Helpers.CreateUser(2, "TestUserTwo", "test@email.com", true, "passTwo")
                }
            };
            _context.AddRange(users);
            _context.SaveChanges();

            var actualUsers = _classBeingTested.GetAllUsers();
            var expectedCount = 2;
            var actualCount = actualUsers.Count();
            var expectedUserOne = users[0];
            var expectedUserTwo = users[1];
            var actualUserOne = actualUsers[0];
            var actualUserTwo = actualUsers[1];
            Assert.Equal(expectedCount, actualCount);
            Assert.Equal(expectedUserOne, actualUserOne);
            Assert.Equal(expectedUserTwo, actualUserTwo);
        }

        [Fact]
        public void GetUserById_GetsCorrectUser_Success()
        {
            var anyId = 456789258;
            var user = Helpers.CreateUser(anyId);
            _context.Users.Add(user);
            _context.SaveChanges();
            var actualUser = _classBeingTested.GetUserById(anyId);
            var expectedUser = new User 
            { 
                Id = 456789258, Email = "donatello@tmnt.com", IsAdmin = false, UserName = "TestUserName", Password ="test-pass123#" 
            };

            Assert.Equal(expectedUser.Id, actualUser.Id);
            Assert.Equal(expectedUser.Email, actualUser.Email);
            Assert.Equal(expectedUser.UserName, actualUser.UserName);
            Assert.Equal(expectedUser.Password, actualUser.Password);
            Assert.False(actualUser.IsAdmin);
        }

        [Fact]
        public void GetUserByUserName_GetsCorrectUser_Success()
        {
            var uniqueUserName = "Michaelangelo";
            var user = Helpers.CreateUser(id: 3, username: uniqueUserName);
            _context.Users.Add(user);
            _context.SaveChanges();
            var actualUser = _classBeingTested.GetUserByUserName(uniqueUserName);
            var expectedUser = new User
            {
                Id = 3,
                Email = "donatello@tmnt.com",
                IsAdmin = false,
                UserName = "Michaelangelo",
                Password = "test-pass123#"
            };

            Assert.Equal(expectedUser.Id, actualUser.Id);
            Assert.Equal(expectedUser.Email, actualUser.Email);
            Assert.Equal(expectedUser.UserName, actualUser.UserName);
            Assert.Equal(expectedUser.Password, actualUser.Password);
            Assert.False(actualUser.IsAdmin);
        }

        [Fact]
        public void AddUser_SavesUserToDB_Successful()
        {
            var userName = "NewUserTest";
            var email = "test@email.com";
            var password = "test-pass";
            var user = Helpers.CreateUser(username: userName, email: email, userPass: password);
            _classBeingTested.AddUser(user);
            var userExists = _context.Users.First(user => user.Id == 1 && 
                user.UserName == userName && 
                user.Email == email &&
                user.Password == password &&
                user.IsAdmin == false) 
                != null;
            Assert.True(userExists);
        }

        [Fact]
        public void CheckUserNameAndEmailExists_UserNameAndEmailExists_ReturnsTrue()
        {
            var passedInUser = Helpers.CreateUser(username: "UserExists", email: "EmailExists");
            var existingUser = Helpers.CreateUser(username: "UserExists", email: "EmailExists");
            _context.Add(existingUser);
            _context.SaveChangesAsync();
            var (actualUserNameExists, actualEmailExists) = _classBeingTested.CheckUserNameAndEmailExists(passedInUser);
            Assert.True(actualUserNameExists);
            Assert.True(actualEmailExists);
        }

        [Fact]
        public void CheckUserNameAndEmailExists_UserNameAndEmailDontExists_ReturnsFalse()
        {
            var passedInUser = Helpers.CreateUser(username: "UserDoesntExists", email: "EmailDoesntExists");
            var existingUser = Helpers.CreateUser();
            _context.Add(existingUser);
            _context.SaveChangesAsync();
            var (actualUserNameExists, actualEmailExists) = _classBeingTested.CheckUserNameAndEmailExists(passedInUser);
            Assert.False(actualUserNameExists);
            Assert.False(actualEmailExists);
        }
    }
}
