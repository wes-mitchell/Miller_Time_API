namespace MillerTime.Models.DBModels
{
    public class Video
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string YoutubeVideoId { get; set; }

        public bool IsApproved { get; set; }
    }
}
