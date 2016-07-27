using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Nuts.Service
{
    public static class RssReader
    {
        public static SyndicationFeed GetFeed(string url)
        {
            using (var reader = XmlReader.Create(url))
            {
                return SyndicationFeed.Load(reader);
            }
        }
    }
}
