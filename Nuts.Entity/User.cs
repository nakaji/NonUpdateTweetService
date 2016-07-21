using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Nuts.Entity
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long UserId { get; set; }
        public string ScreenName { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }

        public ICollection<Setting> Settings { get; set; }
    }
}
