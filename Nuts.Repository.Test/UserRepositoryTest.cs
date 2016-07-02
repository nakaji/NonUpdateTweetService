using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nuts.Entity;

namespace Nuts.Repository.Test
{
    [TestClass]
    public class UserRepositoryTest
    {
        private static Mock<IDbSet<User>> GetMockSet(IEnumerable<User> data)
        {
            var queriableData = data.AsQueryable();

            var mock = new Mock<IDbSet<User>>();
            mock.Setup(m => m.Provider).Returns(queriableData.Provider);
            mock.Setup(m => m.Expression).Returns(queriableData.Expression);
            mock.Setup(m => m.ElementType).Returns(queriableData.ElementType);
            mock.Setup(m => m.GetEnumerator()).Returns(queriableData.GetEnumerator());

            return mock;
        }

        [TestMethod]
        public void GetUserById_存在しない場合はnull()
        {
            // Arrange
            var mockSet = GetMockSet(new List<User>());
            var moqDb = new Mock<AppDbContext>();
            moqDb.Setup(x => x.Users).Returns(mockSet.Object);

            var sut = new UserRepository(moqDb.Object);

            // Act
            var result = sut.GetUserById(9999);

            // Assert
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void GetUserById_UserIdで検索()
        {
            // Arrange
            var data = new List<User>()
            {
                new User {UserId = 100},
            };
            var mockSet = GetMockSet(data);
            var moqDb = new Mock<AppDbContext>();
            moqDb.Setup(x => x.Users).Returns(mockSet.Object);

            var sut = new UserRepository(moqDb.Object);

            // Act
            var result = sut.GetUserById(100);

            // Assert
            Assert.AreEqual(100, result.UserId);
        }

        [TestMethod]
        public void Save_ユーザー情報を保存する()
        {
            // Arrange
            var mockSet = GetMockSet(new List<User>());
            var moqDb = new Mock<AppDbContext>();
            moqDb.Setup(x => x.Users).Returns(mockSet.Object);

            var sut = new UserRepository(moqDb.Object);

            // Act
            var user = new User() { UserId = 999 };
            sut.Save(user);

            // Assert
            mockSet.Verify(x => x.Add(It.IsAny<User>()), Times.Once);
            moqDb.Verify(x => x.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void Save_同じUserIdのデータがあれば上書き()
        {
            // Arrange
            var data = new List<User>()
            {
                new User {UserId = 100,  ScreenName = "New User"},
            };
            var mockSet = GetMockSet(data);
            var moqDb = new Mock<AppDbContext>();
            moqDb.Setup(x => x.Users).Returns(mockSet.Object);

            var sut = new UserRepository();

            // Act
            var user = new User() {UserId = 100, ScreenName = "Updated User" };
            sut.Save(user);
            var result = sut.GetUserById(user.UserId);

            // Assert
            Assert.AreEqual("Updated User", result.ScreenName);
        }
    }
}
