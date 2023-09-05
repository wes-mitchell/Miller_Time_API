using MillerTime.API.Context;
using MillerTime.API.Models.DBModels;
using MillerTime.API.Repositories.Interfaces;

namespace MillerTime.API.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        readonly MillerTimeContext _mtContext;

        public UserRepository(IConfiguration configuration, MillerTimeContext context) : base(configuration, context)
        {
            _mtContext = context;
        }

        public List<User> GetAllUsers()
        {
            var users = _mtContext.Users.ToList();
            return users;
        }

        public User GetUserById(int userId) 
        {
            var user = _mtContext.Users.Where(x => x.Id == userId).FirstOrDefault();
            return user;
        }

        public async Task<User> AddUser(User user)
        { 
            _mtContext.Users.Add(user);
            await _mtContext.SaveChangesAsync();
            return user;
        }

    }
}
