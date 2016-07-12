using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nuts.Entity.Test
{
    [TestClass]
    public class SettingTest
    {
        [TestMethod]
        public void UserIDをセットしてデータを追加する()
        {
            // Arrange
            long userId = 200;
            Database.SetInitializer(new DropCreateDatabaseAlways<AppDbContext>());
            var db = new AppDbContext();
            var user = new User() { UserId = userId, ScreenName = nameof(UserIDをセットしてデータを追加する) };
            db.Users.Add(user);
            db.SaveChanges();

            // Act
            var setting = new Setting() { RssUrl = "http://example.com/rss", UserUserId = userId };
            db.Settings.Add(setting);
            db.SaveChanges();

            // Assert
            var result = db.Settings.Where(x => x.UserUserId == userId).ToList();
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("http://example.com/rss", result[0].RssUrl);
        }
    }
}
