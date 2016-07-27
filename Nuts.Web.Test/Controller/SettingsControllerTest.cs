using System.Web.Mvc;
using System.Web.SessionState;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nuts.Web.Controllers;
using Nuts.Web.Test.Fakes;
using Nuts.Web.ViewModels;
using Nuts.Web.WorkerService;

namespace Nuts.Web.Test.Controller
{
    [TestClass]
    public class SettingsControllerTest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:スコープを失う前にオブジェクトを破棄")]
        [TestMethod]
        public void New_モデルがエラーの場合は元の画面へ戻る()
        {
            // Arrange
            var moq = new Mock<ISettingService>();
            moq.Setup(x => x.GetSettingsEditViewModel(It.IsAny<long>(), It.IsAny<int>())).Returns(() => null);
            var sut = new SettingsController(moq.Object);
            var sessionItems = new SessionStateItemCollection();
            sessionItems["UserId"] = 100;
            sut.ControllerContext = new FakeControllerContext(sut, sessionItems);
            sut.ModelState.Clear();

            // Act
            sut.ModelState.AddModelError("RssUrl", "必須ですわ");
            var result = sut.New(new SettingsNewViewModel()) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:スコープを失う前にオブジェクトを破棄")]
        [TestMethod]
        public void Edit_該当するデータがなければ404_NotFound()
        {
            // Arrange
            var moq = new Mock<ISettingService>();
            moq.Setup(x => x.GetSettingsEditViewModel(It.IsAny<long>(), It.IsAny<int>())).Returns(() => null);
            var sut = new SettingsController(moq.Object);
            var sessionItems = new SessionStateItemCollection();
            sessionItems["UserId"] = 100;
            sut.ControllerContext = new FakeControllerContext(sut, sessionItems);

            // Act
            var result = sut.Edit(1) as HttpStatusCodeResult;

            // Assert
            Assert.AreEqual(404, result.StatusCode);
        }
    }
}
