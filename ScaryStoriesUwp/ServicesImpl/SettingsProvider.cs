using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using ScaryStoriesUwp.Shared.Services;
using ScaryStoriesUwp.Shared.Services.Models;

namespace ScaryStoriesUwp.ServicesImpl
{
    public class SettingsProvider : ISettingsProvider
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
    }
}
