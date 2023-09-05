using MillerTime.API.Models.DBModels;

namespace MillerTime.API.Repositories.Interfaces
{
    public interface IVideoRepository
    {
        List<Video> GetAllVideos();

        Task<Video> AddVideo(Video video);
    }
}
