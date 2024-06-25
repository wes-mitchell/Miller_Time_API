using Microsoft.AspNetCore.Mvc;
using MillerTime.Models.DBModels;
using MillerTime.Services.Interfaces;

namespace MillerTime.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetUserById(int userId)
        {
            return Ok(_userService.GetUserById(userId));
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<User> AddUser(User user)
        {
            return await _userService.AddUser(user);
        }
    }
}
