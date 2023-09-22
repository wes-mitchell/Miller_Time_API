using MillerTime.API.Models.DBModels;

namespace MillerTime.API.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task CreateUser(User user);

        bool AuthenticateUser(User user);
    }
}
