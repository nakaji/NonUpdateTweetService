using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nuts.Entity;

namespace Nuts.Repository
{
    public class SettingsRepository
    {
        private AppDbContext _db;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:スコープを失う前にオブジェクトを破棄")]
        public SettingsRepository() : this(new AppDbContext()) { }

        public SettingsRepository(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<Setting> FindByUserId(long userId)
        {
            return _db.Settings.Where(x => x.UserUserId == userId);
        }

        public void Save(Setting setting)
        {
            _db.Settings.AddOrUpdate(setting);
            _db.SaveChanges();
        }
    }
}
