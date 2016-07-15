using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nuts.Repository;
using Nuts.Web.ViewModels;

namespace Nuts.Web.WorkerService
{
    public interface ISettingService
    {
        SettingsIndexViewModel GetSettingsIndexViewModel(long userId);
        SettingsNewViewModel GetSettingsNewViewModel(long userId);
        void AddNewSetting(SettingsNewViewModel model);
        SettingsEditViewModel GetSettingsEditViewModel(long userId, int settingId);
    }

    public class SettingService: ISettingService
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

        public void AddNewSetting(SettingsNewViewModel model)
        {
            var repository = new SettingsRepository();

            var setting = new Entity.Setting()
            {
                RssUrl = model.Setting.RssUrl,
                UserUserId = model.UserId,
            };

            repository.Save(setting);
        }

        public SettingsEditViewModel GetSettingsEditViewModel(long userId, int settingId)
        {
            var user = _repository.GetUserById(userId);
            if (user == null) throw new InvalidOperationException();

            var setting = user.Settings?.FirstOrDefault(x => x.Id == settingId);
            if (setting == null) return null;

            var model = new SettingsEditViewModel()
            {
                UserId = user.UserId,
                ScreetName = user.ScreenName,
                Setting = new Setting() { Id = setting.Id, RssUrl = setting.RssUrl },
            };

            return model;
        }
    }
}