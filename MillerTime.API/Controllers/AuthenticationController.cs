using Microsoft.AspNetCore.Mvc;
using MillerTime.Models.DBModels;
using MillerTime.Services.Interfaces;

namespace MillerTime.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthenticationController(IAuthenticationService authService)
        {
            _authService = authService;

        }

        [HttpPost]
        [Produces("application/json")]
        public async Task CreateUser(User user)
        {
            await _authService.CreateUser(user);
        }

        [HttpPost]
        [Produces("application/json")]
        public bool AuthenticateUser(User user) {
            return _authService.AuthenticateUser(user);
        }

    }
}
