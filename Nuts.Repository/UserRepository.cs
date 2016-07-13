using Nuts.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nuts.Repository
{
    public interface IUserRepository
    {
        User GetUserById(long userId);
        void Save(User user);
    }

    public class UserRepository : IUserRepository
    {
        private AppDbContext _db;

        public UserRepository() : this(new AppDbContext()) { }

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public User GetUserById(long userId)
        {
            return _db.Users.Include("Settings").FirstOrDefault(x => x.UserId == userId);
        }

        public void Save(User user)
        {
            var u = GetUserById(user.UserId);
            if (u == null)
            {
                _db.Users.Add(user);
            }
            else
            {
                u.ScreenName = user.ScreenName;
                u.AccessToken = user.AccessToken;
                u.AccessTokenSecret = user.AccessTokenSecret;
                u.Settings = user.Settings;
            }
            _db.SaveChanges();
        }
    }
}
