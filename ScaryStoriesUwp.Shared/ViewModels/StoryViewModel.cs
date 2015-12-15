using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Cirrious.CrossCore.UI;
using Cirrious.MvvmCross.ViewModels;
using ScaryStoriesUniversal.Api;
using ScaryStoriesUniversal.Api.Entities;
using ScaryStoriesUwp.Shared.Database.Entities;
using ScaryStoriesUwp.Shared.Database.Repositories.Base;
using ScaryStoriesUwp.Shared.Services;
using ScaryStoriesUwp.Shared.Services.Models;
using ScaryStoriesUwp.Shared.ViewModels.Base;

namespace ScaryStoriesUwp.Shared.ViewModels
{
    public class StoryViewModel : LoadingScreen
    {
        private readonly IApiService _apiService;
        private readonly StoryListIdsContainer _idsContainer;
        private readonly IMessageProvider _messageProvider;
        private readonly IFavoriteStoriesRepository _favoriteStoriesRepository;
        private readonly ISpeechSyntizerService _speechSyntizerService;
        private Story _story;
        private Photo _photo;
        private bool _isPlaying;
        private MvxVisibility _toFavoriteButtonVisible = MvxVisibility.Collapsed;
        private MvxVisibility _deleteFromFavoriteButtonVisible = MvxVisibility.Collapsed;
        private TextInfoSettings _textSettings;
       
       

        #region Commands
        public ICommand NavigateToWebUrlCommand { get; set; }
        public ICommand BackStoryCommand { get; set; }
        public ICommand NextStoryCommand { get; set; }
        public ICommand DeleteFromFavoriteCommand { get; set; }

        public ICommand SyntheseCommand { get; set; }

        public ICommand AddToFavoriteCommand { get; set; }

        public ICommand PauseCommand { get; set; }
        #endregion

        public Story Story
        {
            get { return _story; }
            set
            {
                _story = value;
                base.RaisePropertyChanged(() => Story);
            }
        }

        public Photo Photo
        {
            get { return _photo; }
            set
            {
                _photo = value;
                base.RaisePropertyChanged(() => Photo);
            }
        }

        

        public StoryViewModel(IApiService apiService, StoryListIdsContainer idsContainer, IMessageProvider messageProvider,
            IFavoriteStoriesRepository favoriteStoriesRepository, ISettingsProvider settingsProvider,ISpeechSyntizerService speechSyntizerService)
            :base("История")
        {
            _apiService = apiService;
            _idsContainer = idsContainer;
            _messageProvider = messageProvider;
            _favoriteStoriesRepository = favoriteStoriesRepository;
            _speechSyntizerService = speechSyntizerService;
            NavigateToWebUrlCommand=new MvxCommand(WebNavigateToUrl);
            BackStoryCommand=new MvxCommand(Back);
            NextStoryCommand = new MvxCommand(Next);
            DeleteFromFavoriteCommand=new MvxCommand(DeleteFromFavorite);
            AddToFavoriteCommand=new MvxCommand(ToFavorite);
            SyntheseCommand=new MvxCommand(Synthese);
            PauseCommand=new MvxCommand(Pause);
            TextSettings = settingsProvider.TextSettings;
         }

        

        public async void WebNavigateToUrl()
        {
            if (!String.IsNullOrEmpty(Story.Url))
            {
                //var success = await Windows.System.Launcher.LaunchUriAsync(new Uri(Story.Url));
            }
        }

        public void NavigateToSettings()
        {
            //_navigationService.UriFor<SettingsViewModel>().Navigate();
        }

        public async void Back()
        {
            Pause();
            await GetStoryById(_idsContainer.Back());
        }

        public bool CanBack
        {
            get { return _idsContainer.CanBack(); }
        }

        public bool CanNext
        {
            get
            {
                return _idsContainer.CanNext();
            }
        }

        public MvxVisibility ToFavoriteButtonVisible
        {
            get { return _toFavoriteButtonVisible; }
            set
            {
                _toFavoriteButtonVisible = value;
                base.RaisePropertyChanged(() => ToFavoriteButtonVisible);
            }
        }

        public MvxVisibility DeleteFromFavoriteButtonVisible
        {
            get { return _deleteFromFavoriteButtonVisible; }
            set
            {
                _deleteFromFavoriteButtonVisible = value;
                base.RaisePropertyChanged(() => DeleteFromFavoriteButtonVisible);
            }
        }

        public TextInfoSettings TextSettings
        {
            get { return _textSettings; }
            set
            {
                _textSettings = value;
                base.RaisePropertyChanged(() => TextSettings);
            }
        }

        public bool IsPlaying
        {
            get { return _isPlaying; }
            set
            {
                _isPlaying = value;
                base.RaisePropertyChanged(()=>IsPlaying);
            }
        }


        public async void Next()
        {
            Pause();
            await GetStoryById(_idsContainer.Next());
        }

        private void Pause()
        {
            _speechSyntizerService.Pause();
            IsPlaying = false;
        }

        public async void ToFavorite()
        {
            await _favoriteStoriesRepository.InsertAsync(new FavoriteStory() { CreatedAt = Story.CreatedAt, Id = Story.Id, Name = Story.Name, Thumb = Photo.Thumb });
            IsFavoriteVisible();
        }

        public async void DeleteFromFavorite()
        {
            await _favoriteStoriesRepository.DeleteAsync(Story.Id.ToString());
            IsUnfavoriteVisible();
        }

        public async void Synthese()
        {
            if (_speechSyntizerService.State == SynzSpeechStates.Pause)
            {
                _speechSyntizerService.Play();
                IsPlaying = true;
            }
            else
            {
                base.Wait(true, "Синтезируем речь..");
                await _speechSyntizerService.Synt(Story.Text);
                IsPlaying = true;
                base.Wait(false);
            }
           
        }

        public async void StopSynthese()
        {
            _speechSyntizerService.Pause();
          
        }

        public async override void Start()
        {
            var currentId = _idsContainer.GetCurrentStoryId();
            await GetStoryById(currentId);
            base.Start();
        }

        

        private async Task GetStoryById(string currentId)
        {
            _speechSyntizerService.PositionNull();
            base.Wait(true);
            Story = await _apiService.GetStory(currentId);
            base.Wait(false);
            Photo = await _apiService.GetPhoto(Story.PhotoId);
            Story.Thumb = Photo.Image;
          //  await _tileService.AddTilesToQueue(Story);

            await RefreshAppBarButtons();
        }

        public void ImageTap()
        {
            //_navigationService.UriFor<PhotoViewModel>().WithParam(x => x.PhotoId, _photo.Id).Navigate();
        }

        private async Task RefreshAppBarButtons()
        {
            base.RaisePropertyChanged(() => CanBack);
            base.RaisePropertyChanged(() => CanNext);
            var isFavorite = await _favoriteStoriesRepository.CheckIsFavorite(Story.Id.ToString());
            if (isFavorite)
            {
                IsFavoriteVisible();
            }
            else
            {
                IsUnfavoriteVisible();
            }
            
        }

        private void IsUnfavoriteVisible()
        {
            DeleteFromFavoriteButtonVisible = MvxVisibility.Collapsed;
            ToFavoriteButtonVisible = MvxVisibility.Visible;
        }

        private void IsFavoriteVisible()
        {
            DeleteFromFavoriteButtonVisible = MvxVisibility.Visible;
            ToFavoriteButtonVisible = MvxVisibility.Collapsed;
        }

        public void SetMediaElement(object mediaElement)
        {
            _speechSyntizerService.SetMediaElement(mediaElement);
        }
    }
}
