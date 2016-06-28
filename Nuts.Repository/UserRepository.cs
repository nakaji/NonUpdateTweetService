using Nuts.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nuts.Repository
{
    public class UserRepository
    {
        private AppDbContext _db;

        public UserRepository() : this(new AppDbContext()) { }

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public User GetUserByTwitterUserId(long userId)
        {
            return _db.Users.FirstOrDefault(x => x.Twitter.UserId == userId);
        }

        public void Save(User user)
        {
            _db.Users.Add(user);
        }
    }
}
