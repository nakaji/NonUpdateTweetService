using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
            // Arrange
            Database.SetInitializer(new DropCreateDatabaseAlways<AppDbContext>());
            var db = new AppDbContext();

            // Act
            db.Users.ToListAsync().Wait();
        }

        [TestMethod]
        public void マイグレーションするように初期化する()
        {
            // Arrange

            // Act
            DatabaseInitializer.MigrateDatabaseToLatestVersion();
            var db = new AppDbContext();

            // Assert
            db.Users.ToListAsync().Wait();

        }

        [TestMethod]
        public void ユーザー取得時に設定も一緒に持ってくる()
        {
            // Arrange
            Database.SetInitializer(new DropCreateDatabaseAlways<AppDbContext>());
            var db = new AppDbContext();

            db.Users.Add(new User()
            {
                UserId = 100,
                Settings = new List<Setting>()
                {
                    new Setting() {RssUrl = "http://example.com/rss"}
                }
            });
            db.SaveChanges();

            // Act
            var user = db.Users.Include("Settings").FirstOrDefault(x => x.UserId == 100);

            // Assert
            Assert.AreEqual("http://example.com/rss", user.Settings.First().RssUrl);

        }
    }
}
