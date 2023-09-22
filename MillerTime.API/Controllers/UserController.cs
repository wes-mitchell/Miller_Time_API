using Microsoft.AspNetCore.Mvc;
using MillerTime.API.Models.DBModels;
using MillerTime.API.Services.Interfaces;

namespace MillerTime.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("GetUserById")]
        public IActionResult GetUserById(int userId)
        {
            return Ok(_userService.GetUserById(userId));
        }

        [HttpPost]
        [Produces("application/json")]
        [Route("AddUser")]
        public async Task<User> AddUser(User user)
        {
            return await _userService.AddUser(user);
        }
    }
}
