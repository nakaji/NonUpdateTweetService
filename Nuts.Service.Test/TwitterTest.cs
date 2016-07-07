using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nuts.Service;

namespace Nuts.Service.Test
{
    [TestClass]
    public class TwitterTest
    {
        [TestMethod]
        public void Status_ツイートする()
        {
            // Arrange
            var accessToken = ConfigurationManager.AppSettings["AccessToken"];
            var accessTokenSecret = ConfigurationManager.AppSettings["AccessTokenSecret"];
            var sut = new Twitter(accessToken, accessTokenSecret);

            // Act
            sut.UpdateStatusAsync("てすと").Wait();

            // Assert
        }
    }
}
