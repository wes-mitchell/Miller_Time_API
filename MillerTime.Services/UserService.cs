using MillerTime.Models.DBModels;
using MillerTime.DAL.Repositories.Interfaces;
using MillerTime.Services.Interfaces;

namespace MillerTime.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) 
        { 
            _userRepository = userRepository;
        }

        public List<User> GetAllUsers() 
        { 
            return _userRepository.GetAllUsers();
        }

        public User GetUserById(int userId) 
        { 
            return _userRepository.GetUserById(userId);
        }

        public async Task<User> AddUser(User user) 
        {
            return await _userRepository.AddUser(user);
        }

    }
}
