using System;
using System.Collections.Generic;
using System.Globalization;
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
            var model = _service.GetSettingsNewViewModel();
            return View(model);
        }

        // Post: Settings/New
        [HttpPost]
        public ActionResult New([Bind(Include = "RssUrl")]SettingsNewViewModel model)
        {
            var userId = long.Parse(Session["UserId"].ToString());

            if (!ModelState.IsValid)
            {
                return View();
            }

            _service.AddNewSetting(userId, model);

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

        // POST: Settings/Edit/{id}
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id, RssUrl")]SettingsNewViewModel model)
        {
            var userId = long.Parse(Session["UserId"].ToString());

            if (!ModelState.IsValid)
            {
                return View();
            }

            _service.EditSetting(userId, model);

            return RedirectToAction("Index");
        }

        // GET: Settings/Delete/{id}
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var userId = long.Parse(Session["UserId"].ToString());

            var model = _service.GetSettingsEditViewModel(userId, id);

            if (model == null) return HttpNotFound();

            return View(model);
        }

        // POST: Settings/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var userId = long.Parse(Session["UserId"].ToString());

            _service.DeleteSetting(userId, id);

            TempData["Message"] = "削除しました。";

            return RedirectToAction("Index");
        }

    }
}