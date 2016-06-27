using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
