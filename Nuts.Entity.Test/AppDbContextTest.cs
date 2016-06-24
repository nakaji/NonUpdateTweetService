using System;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nuts.Entity.Migrations;

namespace Nuts.Entity.Test
{
    [TestClass]
    public class AppDbContextTest
    {
        [TestMethod]
        public void データベースを初期化する()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<AppDbContext>());

            var db = new AppDbContext();
            var result = db.Users.ToListAsync().Result;
        }
    }
}
