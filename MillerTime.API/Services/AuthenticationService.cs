using MillerTime.API.Models.DBModels;
using MillerTime.API.Repositories.Interfaces;
using MillerTime.API.Services.Interfaces;

namespace MillerTime.API.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateUser(User user)
        {
            var (userNameExists, emailExists) = _userRepository.CheckUserNameAndEmailExists(user);
            if (userNameExists)
            {
                throw new Exception($"The username {user.UserName} already exists.");
            }
            if (emailExists)
            {
                throw new Exception($"There is already a user registered with the email {user.Email}.");
            }
            user.Password = hashPassword(user.Password);
            await _userRepository.AddUser(user);
        }

        public bool AuthenticateUser(User user)
        {
            var dbUser = _userRepository.GetUserByUserName(user.UserName);
            if (dbUser == null)
            {
                throw new Exception($"A user was not found with the UserName {user.UserName}. Confirm the information you provided is correct.");
            }
            var authenticated = verifyUserPassword(user.Password, dbUser.Password);
            return authenticated;
        }

        private string hashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool verifyUserPassword(string userPassword, string dbHashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(userPassword, dbHashedPassword);
        }

    }
}
