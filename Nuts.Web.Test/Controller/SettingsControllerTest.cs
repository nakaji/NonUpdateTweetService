using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nuts.Web.Controllers;
using Nuts.Web.ViewModels;

namespace Nuts.Web.Test.Controller
{
    [TestClass]
    public class SettingsControllerTest
    {
        [TestMethod]
        public void New_モデルがエラーの場合は元の画面へ戻る()
        {
            // Arrange
            var sut = new SettingsController();
            sut.ModelState.Clear();
            
            // Act
            sut.ModelState.AddModelError("RssUrl", "必須ですわ");
            var result = sut.New(new SettingsNewViewModel()) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);
        }
    }
}
