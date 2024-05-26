using MillerTime.Models.DBModels;

namespace MillerTime.DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();

        User GetUserById(int id);

        User GetUserByUserName(string userName);

        Task<User> AddUser(User user);
        (bool UserNameExists, bool EmailExists) CheckUserNameAndEmailExists(User user);
    }
}
