using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nuts.Repository;
using Nuts.Web.ViewModels;

namespace Nuts.Web.WorkerService
{
    public class SettingService
    {
        private readonly IUserRepository _repository;

        public SettingService() : this(new UserRepository())
        {
        }

        public SettingService(IUserRepository repository)
        {
            _repository = repository;
        }

        public SettingsIndexViewModel GetSettingsIndexViewModel(long userId)
        {
            var user = _repository.GetUserById(userId);

            var model = new SettingsIndexViewModel()
            {
                UserId = user.UserId,
                ScreetName = user.ScreenName,
                Settings = user.Settings.Select(x => new Setting() { Id = x.Id, RssUrl = x.RssUrl }).ToList()
            };

            return model;
        }

        public SettingsNewViewModel GetSettingsNewViewModel(long userId)
        {
            var user = _repository.GetUserById(userId);

            var model = new SettingsNewViewModel()
            {
                UserId = user.UserId,
                ScreetName = user.ScreenName,
                Setting = new Setting() { RssUrl = "" }
            };

            return model;
        }
    }
}