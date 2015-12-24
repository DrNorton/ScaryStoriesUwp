using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Web.Http;
using Microsoft.VisualBasic;
using ScaryStoriesUniversal.Api;
using ScaryStoriesUwp.Shared.Services;

namespace ScaryStoriesUwp.ServicesImpl
{
    public class StoryDatabaseLoader : IStoryDatabaseLoader
    {
        private readonly IApiService _apiService;
        private readonly ISettingsProvider _settingsProvider;
        private readonly IMessageProvider _messageProvider;

        public StoryDatabaseLoader(IApiService apiService,ISettingsProvider settingsProvider,IMessageProvider messageProvider)
        {
            _apiService = apiService;
            _settingsProvider = settingsProvider;
            _messageProvider = messageProvider;
        }

        public async Task<bool> IsDatabaseDownloaded()
        {
            var item = await ApplicationData.Current.LocalFolder.TryGetItemAsync("stories.db");
            return item != null;
        }

        public async Task<long> DownloadNewDatabase()
        {
            var updateResult=await _apiService.CheckDatabaseUpdate(_settingsProvider.DatabaseVersion);
            if (updateResult.ErrorCode != 0)
            {
                await _messageProvider.ShowCustomOkMessageBox(updateResult.ErrorMessage, "Инфорация");
                return 0;
            }
            else
            {
                var newDatabase=updateResult.Result;
                await Download(newDatabase.PathToDatabase,newDatabase.DbVersion);
                return newDatabase.DbVersion;
            }
        }

        public async Task Download(string url,long version)
        {
            using (var httpClient = new HttpClient())
            {
                var result=await httpClient.GetBufferAsync(new Uri(url, UriKind.RelativeOrAbsolute));
                SaveToIsolatedStorage(version.ToString(), result);
            }
        }

        private static void SaveToIsolatedStorage(string version, IBuffer result)
        {
            //Создаём папку
           using (var isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication())
            {
               

                using (var isolatedStorageFileStream = new IsolatedStorageFileStream(String.Format("{0}_localdatabase.db",version), FileMode.Create, isolatedStorageFile))
                {
                    result.AsStream().CopyTo(isolatedStorageFileStream);
                }
            }
        }

       
    }
}
