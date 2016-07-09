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
        public void New_RssURLが未入力の場合は同じ画面へ戻る()
        {
            // Arrange
            var sut = new SettingsController();

            // Act
            var model = new SettingsNewViewModel() { Setting = new Setting() { RssUrl = "" } };
            var result = sut.New(model) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["Action"]);
        }
    }
}
