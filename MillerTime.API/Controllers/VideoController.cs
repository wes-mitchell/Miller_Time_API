﻿using Microsoft.AspNetCore.Mvc;
using MillerTime.API.Models.DBModels;
using MillerTime.API.Services.Interfaces;

namespace MillerTime.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public IActionResult GetAllVideos() {
            return Ok(_videoService.GetAllVideos());
        }

        [HttpPost]
        [Produces("application/json")]
        [Route("AddVideo")]
        public async Task AddVideo(Video video) {
            await _videoService.AddVideo(video);
        }

    }
}
