using MillerTime.API.Models.DBModels;

namespace MillerTime.API.Services.Interfaces
{
    public interface IVideoService
    {
        List<Video> GetAllVideos();

        Task<Video> AddVideo(Video video);
    }
}
