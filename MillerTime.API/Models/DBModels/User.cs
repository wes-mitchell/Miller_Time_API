namespace MillerTime.API.Models.DBModels
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirebaseId { get; set; }
    }
}
