using MillerTime.API.Models.DBModels;

namespace MillerTime.API.Services.Interfaces
{
    public interface IVideoService
    {
        List<Video> GetAllVideos();

        Task AddVideo(Video video);
    }
}
