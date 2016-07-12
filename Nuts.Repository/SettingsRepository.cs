using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nuts.Entity;

namespace Nuts.Repository
{
    public class SettingsRepository
    {
        private AppDbContext _db;

        public SettingsRepository() : this(new AppDbContext()) { }

        public SettingsRepository(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<Setting> FindByUserId(long userId)
        {
            return _db.Settings.Where(x => x.User_UserId == userId);
        }
    }
}
