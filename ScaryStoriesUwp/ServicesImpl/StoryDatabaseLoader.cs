using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using ScaryStoriesUwp.Shared.Services;

namespace ScaryStoriesUwp.ServicesImpl
{
    public class StoryDatabaseLoader : IStoryDatabaseLoader
    {
        public async Task<bool> IsDatabaseDownloaded(string fileName)
        {
            var item = await ApplicationData.Current.LocalFolder.TryGetItemAsync(fileName);
            return item != null;
        }

        public async Task CopyDatabase()
        {
            bool isDatabaseExisting = false;
            try
            {
                StorageFile storageFile = await ApplicationData.Current.LocalFolder.GetFileAsync("stories.db");
                isDatabaseExisting = true;
            }
            catch
            {
                isDatabaseExisting = false;
            }

            if (!isDatabaseExisting)
            {
                StorageFile databaseFile = await Package.Current.InstalledLocation.GetFileAsync("stories.db");
                await databaseFile.CopyAsync(ApplicationData.Current.LocalFolder);
            }
        }
    }
}
