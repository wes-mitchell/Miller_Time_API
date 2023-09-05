using MillerTime.API.Context;
using MillerTime.API.Models.DBModels;
using MillerTime.API.Repositories.Interfaces;

namespace MillerTime.API.Repositories
{
    public class VideoRepository : BaseRepository, IVideoRepository
    {
        readonly MillerTimeContext _mtContext;

        public VideoRepository(IConfiguration configuration, MillerTimeContext context) : base(configuration, context) 
        {
            _mtContext = context;
        }

        public List<Video> GetAllVideos()
        {
            var videos = _mtContext.Videos.ToList();
            return videos;
        }

        public async Task<Video> AddVideo(Video video) 
        { 
            _mtContext.Videos.Add(video);
            await _mtContext.SaveChangesAsync();
            return video;
        }

    }
}
