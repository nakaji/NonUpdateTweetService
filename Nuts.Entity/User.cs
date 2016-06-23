using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nuts.Entity
{
    public class User
    {
        public int Id { get; set; }
        public Twitter Twitter { get; set; }
    }

    public class Twitter
    {
        public long UserId { get; set; }
        public string ScreenName { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }
    }
}
