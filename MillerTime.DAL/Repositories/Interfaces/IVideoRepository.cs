using MillerTime.Models.DBModels;

namespace MillerTime.DAL.Repositories.Interfaces
{
    public interface IVideoRepository
    {
        List<Video> GetAllVideos();

        Task<Video> AddVideo(Video video);
    }
}
