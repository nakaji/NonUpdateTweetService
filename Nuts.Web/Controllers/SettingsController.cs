using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nuts.Web.ViewModels;
using Nuts.Web.WorkerService;

namespace Nuts.Web.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        public ActionResult Index()
        {
            var userId = long.Parse(Session["UserId"].ToString());
            var service = new SettingService();
            var model = service.GetSettingsIndexViewModel(userId);
            return View(model);
        }

        public ActionResult New()
        {
            var userId = long.Parse(Session["UserId"].ToString());
            var service = new SettingService();
            var model = service.GetSettingsNewViewModel(userId);
            return View(model);
        }
    }
}