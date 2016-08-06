using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nuts.Entity;

namespace Nuts.Repository
{
    public interface ISettingsRepository
    {
        IQueryable<Setting> FindByUserId(long userId);
        void Save(Setting setting);
        void Delete(Setting setting);
        IQueryable<Setting> All();
    }

    public class SettingsRepository: ISettingsRepository
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

        public void Delete(Setting setting)
        {
            _db.Settings.Remove(setting);
            _db.SaveChanges();
        }

        public IQueryable<Setting> All()
        {
            return _db.Settings.Include(nameof(User));
        }

    }
}
