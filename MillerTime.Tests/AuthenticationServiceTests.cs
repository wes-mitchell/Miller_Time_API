using MillerTime.API.Models.DBModels;
using MillerTime.API.Repositories.Interfaces;
using MillerTime.API.Services;
using MillerTime.API.Services.Interfaces;
using Moq;
using Xunit;

namespace MillerTime.Tests
{
    public class AuthenticationServiceTests
    {
        private readonly IAuthenticationService _classBeingTested;
        private readonly Mock<IUserRepository> _mockUserRepo;
        public AuthenticationServiceTests()
        {
            _mockUserRepo = new Mock<IUserRepository>();
            _classBeingTested = new AuthenticationService(_mockUserRepo.Object);
        }

        [Fact]
        public async void CreateUer_AddsUser_Successful()
        {
            var user = Helpers.CreateUser(userPass: "TestPassword123#");

            _mockUserRepo.Setup(prop => prop.CheckUserNameAndEmailExists(It.Is<User>(
                x => x.Id == 0 && x.Password == "TestPassword123#"))).Returns((false, false));

            await _classBeingTested.CreateUser(user);

            var expectedUserName = "TestUserName";
            var expectedIsAdmin = false;
            var expectedEmail = "donatello@tmnt.com";
            var expectedId = 0;

            _mockUserRepo.Verify(prop => prop.AddUser(It.Is<User>(x => x.Id == expectedId &&
                x.UserName == expectedUserName &&
                x.IsAdmin == expectedIsAdmin &&
                x.Email == expectedEmail &&
                x.Password != "TestPassword123#")), Times.Once);
        }

        [Fact]
        public async void CreateUer_UserNameExists_Throws()
        {
            var user = Helpers.CreateUser(username: "SupaFly");
            _mockUserRepo.Setup(prop => prop.CheckUserNameAndEmailExists(It.IsAny<User>())).Returns((true, false));
            var exception = await Assert.ThrowsAsync<Exception>(() => _classBeingTested.CreateUser(user));
            var expectExceptionMessage = $"The username SupaFly already exists.";
            Assert.Contains(expectExceptionMessage, exception.Message);
        }

        [Fact]
        public async void CreateUer_EmailExists_Throws()
        {
            var user = Helpers.CreateUser(email: "EmailExists@realcoolguy.com");
            _mockUserRepo.Setup(prop => prop.CheckUserNameAndEmailExists(It.IsAny<User>())).Returns((false, true));
            var exception = await Assert.ThrowsAsync<Exception>(() => _classBeingTested.CreateUser(user));
            var expectExceptionMessage = "There is already a user registered with the email EmailExists@realcoolguy.com.";
            Assert.Contains(expectExceptionMessage, exception.Message);
        }

        [Fact]
        public void AuthenticateUser_UserReturnsNull_Throws()
        {
            var passedUser = Helpers.CreateUser(username: "Leonardo");
            _mockUserRepo.Setup(prop => prop.GetUserByUserName(It.IsAny<string>())).Returns((User)null);
            var exception = Assert.ThrowsAny<Exception>(() => _classBeingTested.AuthenticateUser(passedUser));
            var expectExceptionMessage = $"A user was not found with the UserName Leonardo. Confirm the information you provided is correct.";
            Assert.Contains(expectExceptionMessage, exception.Message);
        }

        [Fact]
        public void AuthenticateUser_PasswordsMatch_ReturnsTrue()
        {
            var password = "ThisIsAStrongPassword12345";
            var encryptedPass = BCrypt.Net.BCrypt.HashPassword(password);
            var passedUser = Helpers.CreateUser(userPass: password);
            var returnedUser = Helpers.CreateUser(userPass: encryptedPass);
            _mockUserRepo.Setup(prop => prop.GetUserByUserName(It.IsAny<string>())).Returns(returnedUser);
            bool results = _classBeingTested.AuthenticateUser(passedUser);
            Assert.True(results);
        }

        [Fact]
        public void AuthenticateUser_InvalidPassword_ReturnsFalse()
        {
            var password = "ThisIsAStrongPassword12345";
            var encryptedPass = BCrypt.Net.BCrypt.HashPassword(password);
            var passedUser = Helpers.CreateUser(userPass: "NotTheSamePassword");
            var returnedUser = Helpers.CreateUser(userPass: encryptedPass);
            _mockUserRepo.Setup(prop => prop.GetUserByUserName(It.IsAny<string>())).Returns(returnedUser);
            bool results = _classBeingTested.AuthenticateUser(passedUser);
            Assert.False(results);
        }

    }
}
