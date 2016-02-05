using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Storage;
using ScaryStoriesUwp.Annotations;
using ScaryStoriesUwp.Shared.Services;
using ScaryStoriesUwp.Shared.Services.Models;

namespace ScaryStoriesUwp.ServicesImpl
{
    public class SettingsProvider : ISettingsProvider,INotifyPropertyChanged
    {
        private ApplicationDataContainer _roamingSettings;
        

        public SettingsProvider()
        {
            _roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
        }

        public bool IsOffline
        {
            get { return StorageHelper.GetCompositeSetting<bool>("IsOffline"); }
            set
            {
                StorageHelper.SetCompositeSetting<bool>("IsOffline", value);
                OnPropertyChanged();
            }
        }

        public long DatabaseVersion
        {
            get
            {
                var settings = StorageHelper.GetCompositeSetting<long>("DbVersion", 0);
              
                return settings;
            }
            set
            {
                StorageHelper.SetCompositeSetting<long>("DbVersion", value);
            }
        }

        public TextInfoSettings TextSettings
        {
            get
            {
                var settings= StorageHelper.GetCompositeSetting<TextInfoSettings>("TextSettings",null);
                if (settings == null)
                {
                    TextSettings = CreateDefaultSettings();
                }
                return settings;
            }
            set
            {
                StorageHelper.SetCompositeSetting<TextInfoSettings>("TextSettings", value);
            }
        }

        public void Save()
        {
           
        }

        private TextInfoSettings CreateDefaultSettings()
        {
            return new TextInfoSettings() {Font = "Segoe UI",LineHeight = 27,Size = 20};
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
