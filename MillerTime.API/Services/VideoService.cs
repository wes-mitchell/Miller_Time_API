using MillerTime.API.Models.DBModels;
using MillerTime.API.Repositories.Interfaces;
using MillerTime.API.Services.Interfaces;

namespace MillerTime.API.Services
{
    public class VideoService : IVideoService
    {
        private readonly IVideoRepository _videoRepository;

        public VideoService(IVideoRepository videoRepository) 
        { 
            _videoRepository = videoRepository;
        }

        public List<Video> GetAllVideos() 
        {
            return _videoRepository.GetAllVideos();
        }

        public async Task<Video> AddVideo(Video video) 
        {
            return await _videoRepository.AddVideo(video);
        }

    }
}
