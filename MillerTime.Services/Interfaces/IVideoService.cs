using MillerTime.Models.DBModels;

namespace MillerTime.Services.Interfaces
{
    public interface IVideoService
    {
        List<Video> GetAllVideos();

        Task<Video> AddVideo(Video video);

        string FormatVideoURL(string url);
    }
}
