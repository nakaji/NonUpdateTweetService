using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nuts.Entity;
using Nuts.Repository;

namespace Nuts.Web.WorkerService
{
    public class AccountService
    {
        public void Save(long userId, string screenName, string accessToken, string accessTokenSecret)
        {
            var repo = new UserRepository();
            repo.Save(new User
            {
                Twitter = new Twitter()
                {
                    UserId = userId,
                    ScreenName = screenName,
                    AccessToken = accessToken,
                    AccessTokenSecret = accessTokenSecret
                }

            });
        }
    }
}