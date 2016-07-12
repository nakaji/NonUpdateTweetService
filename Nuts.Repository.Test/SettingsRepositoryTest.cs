using System;
using System.Text;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
                new Setting() {Id = 1, User_UserId = 100},
                new Setting() {Id = 2, User_UserId = 100},
                new Setting() {Id = 3, User_UserId = 300},
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
    }
}
