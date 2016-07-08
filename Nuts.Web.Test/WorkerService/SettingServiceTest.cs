using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nuts.Web.ViewModels;
using Nuts.Web.WorkerService;

namespace Nuts.Web.Test.WorkerService
{
    [TestClass]
    public class SettingServiceTest
    {
        [TestMethod]
        public void GetSettingsIndexViewModel_UserIdから取得する()
        {
            // Arrange
            var sut = new SettingService();

            // Act
            var restult = sut.GetSettingsIndexViewModel(100);

            // Assert
            Assert.IsNotNull(restult);
        }
    }
}
