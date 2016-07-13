using System;
using System.Text;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Castle.Core.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nuts.Entity;

namespace Nuts.Repository.Test
{
    [TestClass]
    public class SettingsRepositoryTest
    {
        private static Mock<IDbSet<Setting>> GetMockSet(IEnumerable<Setting> data)
        {
            var queriableData = data.AsQueryable();

            var mock = new Mock<IDbSet<Setting>>();
            mock.Setup(m => m.Provider).Returns(queriableData.Provider);
            mock.Setup(m => m.Expression).Returns(queriableData.Expression);
            mock.Setup(m => m.ElementType).Returns(queriableData.ElementType);
            mock.Setup(m => m.GetEnumerator()).Returns(queriableData.GetEnumerator());

            return mock;
        }

        [TestMethod]
        public void Save_UserIdを指定して取得する()
        {
            // Arrange
            var data =new List<Setting>()
            {
                new Setting() {Id = 1, UserUserId = 100},
                new Setting() {Id = 2, UserUserId = 100},
                new Setting() {Id = 3, UserUserId = 300},
            };
            var mockSet = GetMockSet(data);
            var moqDb = new Mock<AppDbContext>();
            moqDb.Setup(x => x.Settings).Returns(mockSet.Object);
            var sut = new SettingsRepository(moqDb.Object);

            // Act
            var result = sut.FindByUserId(100);
            
            // Assert
            Assert.AreEqual(2,result.Count());
        }

        [TestMethod]
        public void Save_UserIdを指定して追加()
        {
            // Arrange
            var db = new AppDbContext();
            db.Settings.ForEach(x => db.Settings.Remove(x));
            db.Users.ForEach(x => db.Users.Remove(x));
            db.SaveChanges();

            var userRepository = new UserRepository();
            var newUser = new User()
            {
                UserId = 200,
                Settings = new List<Setting>()
                {
                    new Setting() {RssUrl = "http://example.com/rss"}
                }
            };
            userRepository.Save(newUser);
            var sut = new SettingsRepository();

            // Act
            sut.Save(new Setting() { RssUrl = "url1", UserUserId = 200 });
            sut.Save(new Setting() { RssUrl = "url2", UserUserId = 200 });
            sut.Save(new Setting() { RssUrl = "url3", UserUserId = 200 });

            // Assert
            var result = sut.FindByUserId(200);
            Assert.AreEqual(4, result.Count());
        }

        [TestMethod]
        public void Save_UserIdを指定して更新()
        {
            // Arrange
            var db = new AppDbContext();
            db.Settings.ForEach(x => db.Settings.Remove(x));
            db.Users.ForEach(x => db.Users.Remove(x));
            db.SaveChanges();

            var userRepository = new UserRepository();
            var newUser = new User()
            {
                UserId = 201,
                Settings = new List<Setting>()
                {
                    new Setting() {RssUrl = "http://example.com/rss"}
                }
            };
            userRepository.Save(newUser);
            var sut = new SettingsRepository();

            // Act
            var data = sut.FindByUserId(201).First();
            data.RssUrl = "http://example.com/Updated";
            sut.Save(data);

            // Assert
            var result = sut.FindByUserId(201).First();
            Assert.AreEqual("http://example.com/Updated", result.RssUrl);
        }
    }
}
