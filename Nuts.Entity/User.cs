using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Nuts.Entity
{
    public class User
    {
        public int Id { get; set; }
        public Twitter Twitter { get; set; }
    }

    public class Twitter
    {
        [Index(IsUnique =true)]
        public long UserId { get; set; }
        public string ScreenName { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }
    }
}
