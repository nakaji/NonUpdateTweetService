using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nuts.Entity;

namespace Nuts.Web.ViewModels
{
    public class SettingsIndexViewModel
    {
        public long UserId { get; set; }
        public string ScreetName { get; set; }

        public List<Setting> Settings { get; set; }
    }

    public class SettingsNewViewModel
    {
        public long UserId { get; set; }
        public string ScreetName { get; set; }

        public Setting Setting { get; set; }
    }

    public class Setting
    {
        public int Id { get; set; }

        public string RssUrl { get; set; }
    }
}