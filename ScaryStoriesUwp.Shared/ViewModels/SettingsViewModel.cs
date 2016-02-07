using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using ScaryStoriesUwp.Shared.Services;
using ScaryStoriesUwp.Shared.Services.Models;
using ScaryStoriesUwp.Shared.ViewModels.Base;

namespace ScaryStoriesUwp.Shared.ViewModels
{
    public class SettingsViewModel:LoadingScreen, IProgressDownloadReceiver
    {
        private readonly ISettingsProvider _settingsProvider;
        private readonly IStoryDatabaseLoader _storyDatabaseLoader;
        private List<string> _fonts;
        private TextInfoSettings _textSettings;
        public ICommand DownloadLocalDatabaseCommand { get; set; }
        private CancellationTokenSource _cancellationTokenSource;

        private string _downloadStatus;
        private int _currentProgress;


        public SettingsViewModel(ISettingsProvider settingsProvider,IStoryDatabaseLoader storyDatabaseLoader) 
            : base("Настройки")
        {
            _settingsProvider = settingsProvider;
            _storyDatabaseLoader = storyDatabaseLoader;
            _textSettings = settingsProvider.TextSettings;
            CreateFontFamilyList();
            DownloadLocalDatabaseCommand=new MvxCommand(async ()=>await DownloadDatabase());
            _cancellationTokenSource=new CancellationTokenSource();
        }

        private async Task DownloadDatabase()
        {
           var version=await _storyDatabaseLoader.DownloadNewDatabase(_cancellationTokenSource.Token,this);
            _settingsProvider.DatabaseVersion = version;
            _settingsProvider.IsOffline = true;

        }

        private void CreateFontFamilyList()
        {
            _fonts = new List<string>();
            _fonts.Add("Arial");
            _fonts.Add("Calibri");
            _fonts.Add("Comic Sans MS");
            _fonts.Add("Courier New");
            _fonts.Add("Georgia");
            _fonts.Add("Lucida Sans Unicode");
            _fonts.Add("Malgun Gothic");
            _fonts.Add("Meiryo UI");
            _fonts.Add("Segoe UI");
            _fonts.Add("Tahoma");
            _fonts.Add("Times New Roman");
            _fonts.Add("Trebuchet MS");
            _fonts.Add("Verdana");
            _fonts.Add("Webdings");
        }

        public List<string> Fonts
        {
            get { return _fonts; }
            set
            {
                _fonts = value;
               
                base.RaisePropertyChanged(() => Fonts);
            }
        }

        public string CurrentFont
        {
            get { return _textSettings.Font; }
            set
            {
                _textSettings.Font = value;
                Save();
                base.RaisePropertyChanged(() => CurrentFont);
            }
        }

        public double TextSize
        {
            get { return _textSettings.Size; }
            set
            {
                _textSettings.Size = value;
                Save();
                base.RaisePropertyChanged(() => TextSize);
            }
        }

        public double LineHeight
        {
            get { return _textSettings.LineHeight; }
            set
            {

                _textSettings.LineHeight = value;
                Save();
                base.RaisePropertyChanged(() => LineHeight);
            }
        }

        public int CurrentProgress
        {
            get { return _currentProgress; }
            set
            {
                _currentProgress = value;
                base.RaisePropertyChanged(()=>CurrentProgress);
            }
        }

        private void Save()
        {
            _settingsProvider.TextSettings = _textSettings;
        }

        public void DownloadStatusChange(string status)
        {
          
        }

        public void DownloadProgressChange(int progress)
        {
            CurrentProgress = progress;
        }

        public void DownloadStart()
        {
         
        }

        public void DownloadFinish()
        {
          
        }

        public void DownloadErrorFinish()
        {
          
        }
    }
}
