﻿using Microsoft.Extensions.Configuration;
using MillerTime.API.Models.DBModels;

namespace MillerTime.Tests
{
    public class Helpers
    {
        public static Video CreateVideo(int userId = 1, string embedUrl = "https://m.youtube.com/watch?v=lalOy8Mbfdc")
        {
            return new Video
            {
                Id = 0,
                UserId = userId,
                EmbedUrl = embedUrl
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
                UserPassword = userPass
            };
        }

        public static ConfigurationManager GetConfigFile()
        {
            var config = new ConfigurationManager();
            config.AddJsonFile("C:\\Users\\wmdru\\workspace\\MillerTime.API\\MillerTime.Tests\\testSettings.json",
                false, true).Build();
            return config;
        }
    }
}
