using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreTweet;

namespace Nuts.Service
{
    public class Twitter
    {
        private Tokens _tokens;

        public Twitter(string accessToken, string accessTokenSecret)
        {
            var consumerKey = ConfigurationManager.AppSettings["ConsumerKey"];
            var consumerSecret = ConfigurationManager.AppSettings["ConsumerSecret"];

            _tokens = Tokens.Create(consumerKey, consumerSecret, accessToken, accessTokenSecret);
        }

        public async Task UpdateStatusAsync(string message)
        {
            await _tokens.Statuses.UpdateAsync(status => message);
        }

    }
}
