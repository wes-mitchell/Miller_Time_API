using MillerTime.API.Models.DBModels;

namespace MillerTime.Tests
{
    public class Helpers
    {
        public static Video CreateVideo(int userId = 1, string embedUrl = "https://m.youtube.com/watch?v=lalOy8Mbfdc") {
            return new Video
            {
                Id = 0,
                UserId = userId,
                EmbedUrl = embedUrl
            };
        }
    }
}
