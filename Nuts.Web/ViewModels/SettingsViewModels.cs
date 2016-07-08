using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nuts.Entity;

namespace Nuts.Web.ViewModels
{
    public class SettingsIndexViewModel
    {
        public string UserId { get; set; }
        public string ScreetName { get; set; }

        public List<Setting> Settings { get; set; }
    }
}