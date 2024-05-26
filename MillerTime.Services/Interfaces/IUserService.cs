using MillerTime.Models.DBModels;

namespace MillerTime.Services.Interfaces
{
    public interface IUserService
    {
        List<User> GetAllUsers();

        User GetUserById(int id);

        Task<User> AddUser(User user);
    }
}
