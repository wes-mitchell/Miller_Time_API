using Microsoft.AspNetCore.Mvc;

namespace MillerTime.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult CheckHealth()
        {
            return Ok("API is running");
        }
    }
}
