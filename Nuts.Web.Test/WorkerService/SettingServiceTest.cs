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
    }
}
