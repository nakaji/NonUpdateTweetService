using System;
using System.Linq;
using Nuts.Repository;
using Nuts.Web.ViewModels;
using WebGrease.Css.Extensions;

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

        void DeleteSetting(long userId, int settingId);
    }

    public class SettingService: ISettingService
    {
        private readonly ISettingsRepository _settingsRepository;

        public SettingService() : this(new SettingsRepository())
        {
        }

        public SettingService(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }

        public SettingsIndexViewModel GetSettingsIndexViewModel(long userId)
        {
            var settings = _settingsRepository.FindByUserId(userId).Select(x => new Setting() {Id = x.Id, RssUrl = x.RssUrl}).ToSafeReadOnlyCollection();

            var model = new SettingsIndexViewModel(settings);

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
            var setting = _settingsRepository.FindByUserId(userId)?.FirstOrDefault(x => x.Id == settingId);
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

            var setting = new Entity.Setting()
            {
                Id = model.Id,
                RssUrl = model.RssUrl,
                UserUserId = userId,
            };

            _settingsRepository.Save(setting);
        }

        public void DeleteSetting(long userId, int settingId)
        {
            var setting = _settingsRepository.FindByUserId(userId)?.FirstOrDefault(x => x.Id == settingId); ;
            if (setting == null) return;

            _settingsRepository.Delete(setting);
        }
    }
}