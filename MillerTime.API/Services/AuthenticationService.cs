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
            user.UserPassword = hashPassword(user.UserPassword);
            await _userRepository.AddUser(user);
        }

        public bool AuthenticateUser(User user)
        {
            //TODO: Update the DB to only allow 1:1 UserNames
            var dbUser = _userRepository.GetUserByUserName(user.UserName);
            if (dbUser == null)
            {
                throw new Exception($"A user was not found with the UserName {user.UserName}. Confirm the information you provided is correct.");
            }
            var authenticated = verifyUserPassword(user.UserPassword, dbUser.UserPassword);
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
