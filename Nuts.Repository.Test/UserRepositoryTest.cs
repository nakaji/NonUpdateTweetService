using System;
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
    public class UserRepositoryTest
    {
        private Mock<AppDbContext> _moqDb;

        [TestInitialize]
        public void SetUp()
        {
            var data = new List<User>()   {
                new User { UserId=100 },
                new User { UserId=200 },
            }.AsQueryable();

            var dbSetMock = new Mock<IDbSet<User>>();
            dbSetMock.Setup(m => m.Provider).Returns(data.Provider);
            dbSetMock.Setup(m => m.Expression).Returns(data.Expression);
            dbSetMock.Setup(m => m.ElementType).Returns(data.ElementType);
            dbSetMock.Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _moqDb = new Mock<AppDbContext>();
            _moqDb.Setup(x => x.Users).Returns(dbSetMock.Object);


            DatabaseInitializer.MigrateDatabaseToLatestVersion();
            using (var db = new AppDbContext())
            {
                db.Users.ForEach(u => db.Users.Remove(u));
                db.SaveChanges();
            }
        }

        [TestMethod]
        public void GetUserById_存在しない場合はnull()
        {
            // Arrange
            var sut = new UserRepository(_moqDb.Object);

            // Act
            var result = sut.GetUserById(9999);

            // Assert
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void GetUserById_UserIdで検索()
        {
            // Arrange
            var sut = new UserRepository(_moqDb.Object);

            // Act
            var result = sut.GetUserById(100);

            // Assert
            Assert.AreEqual(100, result.UserId);
        }

        [TestMethod]
        public void Save_ユーザー情報を保存する()
        {
            // Arrange
            var user = new User() { UserId = 100 };
            var sut = new UserRepository();

            // Act
            sut.Save(user);

            // Assert
        }

        [TestMethod]
        public void Save_同じUserIdのデータがあれば上書き()
        {
            // Arrange
            var user = new User() {  UserId = 100 };
            var sut = new UserRepository();

            // Act
            sut.Save(user);
            user.ScreenName = "updated name";
            sut.Save(user);
            var result = sut.GetUserById(user.UserId);

            // Assert
            Assert.AreEqual("updated name", result.ScreenName);
        }
    }
}
