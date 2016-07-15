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
        private ISettingService _service;

        public SettingsController() : this(new SettingService())
        {
        }

        public SettingsController(ISettingService service)
        {
            _service = service;
        }

        // GET: Settings
        public ActionResult Index()
        {
            var userId = long.Parse(Session["UserId"].ToString());
            var model = _service.GetSettingsIndexViewModel(userId);
            return View(model);
        }

        // GET: Settings/New
        public ActionResult New()
        {
            var userId = long.Parse(Session["UserId"].ToString());
            var model = _service.GetSettingsNewViewModel(userId);
            return View(model);
        }

        // Post: Settings/New
        [HttpPost]
        public ActionResult New(SettingsNewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _service.AddNewSetting(model);

            return RedirectToAction("Index");
        }

        // GET: Settings/Edit/{id}
        public ActionResult Edit(int id)
        {
            var userId = long.Parse(Session["UserId"].ToString());
            var model = _service.GetSettingsEditViewModel(userId, id);

            if (model == null) return HttpNotFound();

            return View(model);
        }
    }
}