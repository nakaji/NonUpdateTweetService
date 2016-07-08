﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nuts.Web.WorkerService;

namespace Nuts.Web.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        public ActionResult Index()
        {
            var service = new SettingService();
            var model = service.GetSettingsIndexViewModel(104183931);
            return View(model);
        }
    }
}