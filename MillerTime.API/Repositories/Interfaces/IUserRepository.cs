using MillerTime.API.Models.DBModels;

namespace MillerTime.API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();

        User GetUserById(int id);

    }
}
