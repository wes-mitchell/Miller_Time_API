using Microsoft.AspNetCore.Mvc;
using MillerTime.API.Models.DBModels;
using MillerTime.API.Services.Interfaces;

namespace MillerTime.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthenticationController(IAuthenticationService authService)
        {
            _authService = authService;

        }

        [HttpPost]
        [Produces("application/json")]
        [Route("CreateUser")]
        public async Task CreateUser(User user)
        {
            await _authService.CreateUser(user);
        }

        [HttpPost]
        [Produces("application/json")]
        [Route("AuthenticateUser")]
        public bool AuthenticateUser(User user) {
            return _authService.AuthenticateUser(user);
        }

    }
}
