using Microsoft.Extensions.Configuration;
using MillerTime.Models.DBModels;

namespace MillerTime.Tests
{
    public class Helpers
    {

        private static readonly string configPath = "C:\\Users\\wmdru\\workspace\\MillerTime.API\\MillerTime.Tests\\testSettings.json";

        public static Video CreateVideo(int userId = 1, string youTubeVideoId = "https://m.youtube.com/watch?v=lalOy8Mbfdc")
        {
            return new Video
            {
                Id = 0,
                UserId = userId,
                YoutubeVideoId = youTubeVideoId
            };
        }

        public static User CreateUser(int id = 0, string username = "TestUserName", string email = "donatello@tmnt.com", bool isAdmin = false, string userPass = "test-pass123#")
        {
            return new User
            {

                Id = id,
                UserName = username,
                Email = email,
                IsAdmin = isAdmin,
                Password = userPass
            };
        }

        public static ConfigurationManager GetConfigFile()
        {
            var config = new ConfigurationManager();
            config.AddJsonFile(configPath, false, true).Build();
            return config;
        }
    }
}
