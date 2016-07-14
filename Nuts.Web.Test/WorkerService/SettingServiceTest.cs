using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nuts.Entity;
using Nuts.Repository;
using Nuts.Web.WorkerService;
using Setting = Nuts.Entity.Setting;

namespace Nuts.Web.Test.WorkerService
{
    [TestClass]
    public class SettingServiceTest
    {
        [TestMethod]
        public void GetSettingsIndexViewModel_UserIdから取得する()
        {
            // Arrange
            var moq = new Mock<IUserRepository>();
            moq.Setup(x => x.GetUserById(100)).Returns(new User()
            {
                UserId = 100,
                ScreenName = "ScreenName",
                Settings = new List<Setting>()
                {
                    new Setting() {RssUrl = "http://example.com/rss1"},
                    new Setting() {RssUrl = "http://example.com/rss2"}
                }
            });
            var sut = new SettingService(moq.Object);

            // Act
            var result = sut.GetSettingsIndexViewModel(100);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(100, result.UserId);
            Assert.AreEqual("ScreenName", result.ScreetName);
            Assert.AreEqual(2, result.Settings.Count);
            Assert.AreEqual("http://example.com/rss1", result.Settings[0].RssUrl);
        }

        [TestMethod]
        public void GetSettingsNewViewModel_空のモデルを返す()
        {
            // Arrange
            var moq = new Mock<IUserRepository>();
            moq.Setup(x => x.GetUserById(100)).Returns(new User()
            {
                UserId = 100,
                ScreenName = "ScreenName",
                Settings = new List<Setting>()
                {
                    new Setting() {RssUrl = "http://example.com/rss1"},
                    new Setting() {RssUrl = "http://example.com/rss2"}
                }
            });
            var sut = new SettingService(moq.Object);

            // Act
            var result = sut.GetSettingsNewViewModel(100);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(100, result.UserId);
            Assert.AreEqual("ScreenName", result.ScreetName);
            Assert.IsNotNull(result.Setting);
            Assert.AreEqual("", result.Setting.RssUrl);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SettingsEditViewModel_存在しないユーザIDが指定された場合はInvalidOperationException()
        {
            // Arrange
            var moq = new Mock<IUserRepository>();
            moq.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(() => null);
            var sut = new SettingService(moq.Object);

            // Act
            var result = sut.GetSettingsEditViewModel(100, 1);

            // Assert
        }

        [TestMethod]
        public void SettingsEditViewModel_存在しない設定の場合はnullを返す()
        {
            // Arrange
            var moq = new Mock<IUserRepository>();
            moq.Setup(x => x.GetUserById(100)).Returns(new User()
            {
                UserId = 100,
                ScreenName = "ScreenName",
                Settings = null
            });
            var sut = new SettingService(moq.Object);

            // Act
            var result = sut.GetSettingsEditViewModel(100, 1);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void SettingsEditViewModel_ユーザIDと設定IDにマッチするものがあった場合()
        {
            // Arrange
            var moq = new Mock<IUserRepository>();
            moq.Setup(x => x.GetUserById(100)).Returns(new User()
            {
                UserId = 100,
                ScreenName = "ScreenName",
                Settings = new List<Setting>()
                {
                    new Setting() {Id = 1, RssUrl = "http://example.com/rss1"},
                    new Setting() {Id = 2, RssUrl = "http://example.com/rss2"}
                }
            });
            var sut = new SettingService(moq.Object);

            // Act
            var result = sut.GetSettingsEditViewModel(100, 1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Setting.Id);
            Assert.AreEqual("http://example.com/rss1", result.Setting.RssUrl);
        }

    }
}
