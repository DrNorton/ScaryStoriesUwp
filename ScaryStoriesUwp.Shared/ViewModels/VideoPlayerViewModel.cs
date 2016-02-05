using System;
using System.Threading.Tasks;
using ScaryStoriesUniversal.Api;
using ScaryStoriesUniversal.Api.Entities;
using ScaryStoriesUwp.Shared.Utilites;
using ScaryStoriesUwp.Shared.ViewModels.Base;

namespace ScaryStoriesUwp.Shared.ViewModels
{
    public class VideoPlayerViewModel:LoadingScreen
    {
        private readonly IApiService _apiService;
        private string _videoSource;

        private Video _video;

        public VideoPlayerViewModel(IApiService apiService) : base("Плеер")
        {
            _apiService = apiService;
        }

        public async void Init(string videoId)
        {
            Video = await _apiService.GetVideo(videoId);
            VideoSource = await PrepareUrl(Video.Url);
        }

        private async Task<string> PrepareUrl(string url)
        {
            var res = HttpUtilityExtensions.ParseQueryString(new Uri(url).Query);
            var key = res["?v"];
            base.Wait(true);
            YouTubeUri youtubeUrl;
            youtubeUrl = await YouTube.GetVideoUriAsync(key, YouTubeQuality.QualityHigh);
            if (youtubeUrl == null)
            {
                youtubeUrl = await YouTube.GetVideoUriAsync(key, YouTubeQuality.QualityMedium);
            }
            base.Wait(false);
            return youtubeUrl.Uri.ToString();
        }

        public Video Video
        {
            get { return _video; }
            set
            {
                _video = value;
                base.RaisePropertyChanged(()=>Video);
            }
        }

        public string VideoSource
        {
            get { return _videoSource; }
            set
            {
                _videoSource = value;
                base.RaisePropertyChanged(() => VideoSource);
            }
        }
    }
}
