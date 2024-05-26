using MillerTime.Models.DBModels;
using MillerTime.DAL.Repositories.Interfaces;
using MillerTime.Services.Interfaces;

namespace MillerTime.Services
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
            video.EmbedUrl = FormatVideoURL(video.EmbedUrl);
            return await _videoRepository.AddVideo(video);
        }

        public string FormatVideoURL(string url)
        {
            var requiredIncomingFormat = "youtube.com/watch?v=";
            var youtubeVideoIdLength = 11;
            if (url.Contains(requiredIncomingFormat))
            {
                var videoId = url.Split("=")[1];
                if (videoId.Count() == youtubeVideoIdLength)
                {
                    var embedURL = $"https://www.youtube.com/embed/{videoId}";
                    return embedURL;
                }
                throw new Exception($"The video ID {videoId} does not match the current Youtube format.");
            }
            throw new Exception($"The video url format is incorrect for {url}. Please submit a valid format");
        }

    }
}



