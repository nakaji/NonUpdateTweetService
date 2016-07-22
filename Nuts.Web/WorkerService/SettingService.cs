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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        SettingsNewViewModel GetSettingsNewViewModel();

        void AddNewSetting(long userId, SettingsNewViewModel model);

        SettingsEditViewModel GetSettingsEditViewModel(long userId, int settingId);

        void EditSetting(long userId, SettingsNewViewModel model); //ToDo:SettingsEditViewModelの間違い
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
                Settings = user.Settings.Select(x => new Setting() { Id = x.Id, RssUrl = x.RssUrl }).ToList()
            };

            return model;
        }

        public SettingsNewViewModel GetSettingsNewViewModel()
        {
            return new SettingsNewViewModel();
        }

        public void AddNewSetting(long userId, SettingsNewViewModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (string.IsNullOrEmpty(model.RssUrl)) throw new ArgumentException(nameof(model.RssUrl) + " is null or empty");

            var repository = new SettingsRepository();

            var setting = new Entity.Setting()
            {
                RssUrl = model.RssUrl,
                UserUserId = userId,
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
                Id = setting.Id,
                RssUrl = setting.RssUrl,
            };

            return model;
        }

        public void EditSetting(long userId, SettingsNewViewModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (string.IsNullOrEmpty( model.RssUrl)) throw new ArgumentException(nameof(model.RssUrl) + " is null or empty");

            var repository = new SettingsRepository();

            var setting = new Entity.Setting()
            {
                Id = model.Id,
                RssUrl = model.RssUrl,
                UserUserId = userId,
            };

            repository.Save(setting);
        }
    }
}