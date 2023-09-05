using MillerTime.API.Models.DBModels;

namespace MillerTime.API.Services.Interfaces
{
    public interface IUserService
    {
        List<User> GetAllUsers();

        User GetUserById(int id);

        Task<User> AddUser(User user);
    }
}
