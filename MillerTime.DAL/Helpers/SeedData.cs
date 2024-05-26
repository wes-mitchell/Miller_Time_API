using CsvHelper;
using Microsoft.EntityFrameworkCore;
using MillerTime.DAL.Context;
using MillerTime.Models.DBModels;
using System.Globalization;

namespace MillerTime.DAL.Helpers
{
    public static class SeedData
    {
        private static readonly string userSeedFilePath = "../MillerTime.DAL/Helpers/SeedDataCSVs/user_seed_data.csv";
        private static readonly string videoSeedFilePath = "../MillerTime.DAL/Helpers/SeedDataCSVs/video_seed_data.csv";
        private static readonly string userTableName = "MillerTime.dbo.Users";
        private static readonly string videoTableName = "MillerTime.dbo.Videos";
        static MillerTimeContext context;
        public static List<User> Users = new List<User>();
        public static List<Video> Videos = new List<Video>();

        public static async Task Seed(MillerTimeContext Context)
        {
            context = Context;
            await context.Database.MigrateAsync();
            await removeSeedData();
            await seedAllData();
        }

        private static async Task seedAllData()
        {
            var usersSeeded = getDataForClassAsync(userSeedFilePath, Users);
            var videosSeeded = getDataForClassAsync(videoSeedFilePath, Videos);
            await saveDBContextChanges();
        }

        private async static Task removeSeedData()
        {
            await context.Database.ExecuteSqlRawAsync("DELETE FROM MillerTime.dbo.Users");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM MillerTime.dbo.Videos");
        }

        private static bool getDataForClassAsync<T>(string FilePath, List<T> DataList) where T : class
        {
            using var streamReader = new StreamReader(FilePath);
            using var csvReader = new CsvReader(streamReader, CultureInfo.CurrentCulture);
            var records = csvReader.GetRecords<T>().ToList();
            DataList.AddRange(records);
            return DataList.Any();
        }

        private static async Task saveDBContextChanges()
        {
            if (Users.Any())
            {
                context.Users.AddRange(Users);
                using var transaction = context.Database.BeginTransaction();
                await context.Database.ExecuteSqlRawAsync($@"SET IDENTITY_INSERT {userTableName} ON");
                await context.SaveChangesAsync();
                await context.Database.ExecuteSqlRawAsync($@"SET IDENTITY_INSERT {userTableName} OFF");
                await transaction.CommitAsync();
            }

            if (Videos.Any())
            {
                context.Videos.AddRange(Videos);
                using var transaction = context.Database.BeginTransaction();
                await context.Database.ExecuteSqlRawAsync($@"SET IDENTITY_INSERT {videoTableName} ON");
                await context.SaveChangesAsync();
                await context.Database.ExecuteSqlRawAsync($@"SET IDENTITY_INSERT {videoTableName} OFF");
                await transaction.CommitAsync();
            }
        }
    }
}
