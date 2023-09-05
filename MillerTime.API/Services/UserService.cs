using MillerTime.API.Models.DBModels;
using MillerTime.API.Repositories.Interfaces;
using MillerTime.API.Services.Interfaces;

namespace MillerTime.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) { 
            _userRepository = userRepository;
        }

        public List<User> GetAllUsers() { 
            return _userRepository.GetAllUsers();
        }

        public User GetUserById(int userId) { 
            return _userRepository.GetUserById(userId);
        }

    }
}
