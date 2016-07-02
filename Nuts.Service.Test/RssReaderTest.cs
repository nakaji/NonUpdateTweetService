using System;
using System.ServiceModel.Syndication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nuts.Service.Test
{
    [TestClass]
    public class RssReaderTest
    {
        [TestMethod]
        public void URLからRSSを取得する()
        {
            // Arrange
            var reader = new RssReader();
            var url = "http://blog.nakajix.jp/feed";

            // Act
            SyndicationFeed result = reader.GetFeed(url);

            // Assert
            Assert.AreEqual("なか日記", result.Title.Text);
        }
    }
}
