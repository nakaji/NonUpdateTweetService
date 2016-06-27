using System;
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
        [TestMethod]
        public void GetUserByTwitterUserId_存在しない場合はnull()
        {
            // Arrange
            var sut = new UserRepository();

            // Act
            var result = sut.GetUserByTwitterUserId(100);

            // Assert
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void GetUserByTwitterUserId_UserIdで検索()
        {
            // Arrange
            var data = new List<User>()   {
                new User { Id = 1, Twitter=new Twitter() {UserId=100} },
                new User { Id = 2, Twitter=new Twitter() {UserId=200} },
            }.AsQueryable();

            var dbSetMock = new Mock<IDbSet<User>>();
            dbSetMock.Setup(m => m.Provider).Returns(data.Provider);
            dbSetMock.Setup(m => m.Expression).Returns(data.Expression);
            dbSetMock.Setup(m => m.ElementType).Returns(data.ElementType);
            dbSetMock.Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var moqDb = new Mock<AppDbContext>();
            moqDb.Setup(x => x.Users).Returns(dbSetMock.Object);

            var sut = new UserRepository(moqDb.Object);

            // Act
            var result = sut.GetUserByTwitterUserId(100);

            // Assert
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual(1, result.Id);

        }
    }
}
