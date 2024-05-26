using MillerTime.Models.DBModels;

namespace MillerTime.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task CreateUser(User user);

        bool AuthenticateUser(User user);
    }
}
