﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required(ErrorMessage = "{0}は必須です。")]
        public string RssUrl { get; set; }
    }
}