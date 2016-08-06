using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Nuts.Repository;
using Nuts.Service;

namespace Nuts.DailyTweetJob
{
    class Program
    {
        static void Main()
        {
            var repository = new SettingsRepository();
            var settings = repository.All();

            foreach (var setting in settings)
            {
                var rss = RssReader.GetFeed(setting.RssUrl);
                var day = (DateTime.Now - rss.LastUpdatedTime).Days;
                if (day >= 1)
                {
                    var lastUpdateDate = rss.LastUpdatedTime.Date.ToString("yyyy/MM/dd");
                    var twitter = new Twitter(setting.User.AccessToken, setting.User.AccessTokenSecret);
                    twitter.UpdateStatusAsync($"丸{day}日間更新してません 最終更新日：{lastUpdateDate}" + DateTime.Now.ToString(CultureInfo.InvariantCulture)).Wait();
                }
            }
        }
    }
}
