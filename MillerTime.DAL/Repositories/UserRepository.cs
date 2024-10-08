﻿using MillerTime.DAL.Context;
using MillerTime.Models.DBModels;
using MillerTime.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MillerTime.DAL.Repositories
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

        public User GetUserByUserName(string userName) 
        { 
            return _mtContext.Users.Where(x => x.UserName == userName).FirstOrDefault();
        }

        public async Task<User> AddUser(User user)
        { 
            _mtContext.Users.Add(user);
            await _mtContext.SaveChangesAsync();
            return user;
        }

        public (bool UserNameExists, bool EmailExists) CheckUserNameAndEmailExists(User user)
        {
            var userNameExists = GetUserByUserName(user.UserName) != null;
            var emailExists = _mtContext.Users.FirstOrDefault(x => x.Email == user.Email) != null;
            return (userNameExists, emailExists);
        }

    }
}
