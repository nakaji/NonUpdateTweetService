﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Nuts.Entity;

namespace Nuts.Web.ViewModels
{
    public class SettingsIndexViewModel
    {
        public SettingsIndexViewModel(ReadOnlyCollection<Setting> settings)
        {
            Settings = settings;
        }

        public ReadOnlyCollection<Setting> Settings { get; private set; }
    }

    public class Setting
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}は必須です。")]
        public string RssUrl { get; set; }
    }

    public class SettingsNewViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}は必須です。")]
        public string RssUrl { get; set; }
    }

    public class SettingsEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}は必須です。")]
        public string RssUrl { get; set; }
    }

    public class SettingsDeleteViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}は必須です。")]
        public string RssUrl { get; set; }
    }
}