using Microsoft.AspNetCore.Mvc;
using MillerTime.Models.DBModels;
using MillerTime.Services.Interfaces;

namespace MillerTime.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService _videoService;

        public VideoController(IVideoService videoService) 
        { 
            _videoService = videoService;
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("GetAllVideos")]
        public IActionResult GetAllVideos() 
        {
            return Ok(_videoService.GetAllVideos());
        }

        [HttpPost]
        [Produces("application/json")]
        [Route("AddVideo")]
        public async Task<Video> AddVideo(Video video) 
        {
            return await _videoService.AddVideo(video);
        }

    }
}
