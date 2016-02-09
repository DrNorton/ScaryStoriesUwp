using System;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.Web.Http;
using ScaryStoriesUniversal.Api;
using ScaryStoriesUwp.Shared.Services;

namespace ScaryStoriesUwp.ServicesImpl
{
    public class StoryDatabaseLoader : IStoryDatabaseLoader
    {
        private readonly IApiService _apiService;
        private readonly ISettingsProvider _settingsProvider;
        private readonly IMessageProvider _messageProvider;
        private BackgroundDownloader _backgroundDownloader;
        private DownloadOperation downloadOperation;
        private IProgressDownloadReceiver _progressDownloadReceiver;
       


        public StoryDatabaseLoader(IApiService apiService,ISettingsProvider settingsProvider,IMessageProvider messageProvider)
        {
            _apiService = apiService;
            _settingsProvider = settingsProvider;
            _messageProvider = messageProvider;
            _backgroundDownloader = new BackgroundDownloader();
        }

     

        public async Task<long> DownloadNewDatabase(CancellationToken cancellationToken, IProgressDownloadReceiver progressDownloadReceiver)
        {
            _progressDownloadReceiver = progressDownloadReceiver;
            _progressDownloadReceiver.DownloadStatusChange("Проверяем обновления...");
            var updateResult=await _apiService.CheckDatabaseUpdate(_settingsProvider.DatabaseVersion);
            if (updateResult.ErrorCode != 0)
            {
                await _messageProvider.ShowCustomOkMessageBox(updateResult.ErrorMessage, "Информация");
                return 0;
            }
            else
            {
                var newDatabase=updateResult.Result;
                var result=await _messageProvider.ShowCustomOkNoMessageBox(
                    String.Format("Размер обновления: {0}мб", (int) newDatabase.Size/1000000), "Обновление базы данных");
                if (result)
                {
                    _progressDownloadReceiver.DownloadStatusChange("Идёт загрузка..");
                    Download(new Uri(newDatabase.PathToDatabase, UriKind.RelativeOrAbsolute), newDatabase.DbVersion.ToString(), cancellationToken);
                }
                else
                {
                    _progressDownloadReceiver.DownloadStatusChange("Загрузка отменена.");
                    _progressDownloadReceiver.DownloadFinish();
                    return 0;
                }
                
                return newDatabase.DbVersion;
            }
        }

        public async void Download(Uri durl,string version, CancellationToken cancellationToken)
        {
            var storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFile file = await storageFolder.CreateFileAsync(String.Format("{0}_localdatabase.db", version), CreationCollisionOption.GenerateUniqueName);
            downloadOperation = _backgroundDownloader.CreateDownload(durl, file);
            _progressDownloadReceiver.DownloadStart();

            Progress<DownloadOperation> progress = new Progress<DownloadOperation>(ProgressChanged);
                try
                {
                    _progressDownloadReceiver.DownloadStatusChange("Идёт загрузка..");
                    await downloadOperation.StartAsync().AsTask(cancellationToken, progress);
                }
                catch (TaskCanceledException)
                {
                    downloadOperation.ResultFile.DeleteAsync();
                    downloadOperation = null;
                }
            
        }

        private void ProgressChanged(DownloadOperation downloadOperation)
        {
            int progress =
                (int)
                    (100*
                     ((double) downloadOperation.Progress.BytesReceived/
                      (double) downloadOperation.Progress.TotalBytesToReceive));
            Debug.WriteLine(downloadOperation.Progress.Status);
            switch (downloadOperation.Progress.Status)
            {
                
                case BackgroundTransferStatus.Running:
                    {
                 
                        break;
                    }
                case BackgroundTransferStatus.PausedByApplication:
                    {
                        break;
                    }
                case BackgroundTransferStatus.PausedCostedNetwork:
                    {
                        break;
                    }
                case BackgroundTransferStatus.PausedNoNetwork:
                    {
                        break;
                    }
                case BackgroundTransferStatus.Error:
                    {
                     _progressDownloadReceiver.DownloadErrorFinish();
                        break;
                    }
            }
            _progressDownloadReceiver.DownloadProgressChange(progress);
            if (progress >= 100)
            {
                downloadOperation = null;
                _progressDownloadReceiver.DownloadFinish();
                _progressDownloadReceiver = null;
            }
        }

      

       
    }
}
