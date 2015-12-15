using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ScaryStoriesUniversal.Api;
using ScaryStoriesUniversal.Api.Entities;
using ScaryStoriesUwp.Shared.ViewModels.Base;

namespace ScaryStoriesUwp.Shared.ViewModels
{
    public class VideosViewModel:LoadingScreen, IIncrementalContainer<Video>
    {
        private readonly IApiService _apiService;
        private const int PagingCount = 10;
        private int _currentCount = 0;
        private Video _selectedVideo;

        public VideosViewModel(IApiService apiService)
            :base("Видео")
        {
            _apiService = apiService;
        }

        public Video SelectedVideo
        {
            get { return _selectedVideo; }
            set
            {
                _selectedVideo = value;
                if (value != null) PlayVideo(value);
                base.RaisePropertyChanged(()=>SelectedVideo);
            }
        }

        private void PlayVideo(Video selectedVideo)
        {
            ShowViewModel<VideoPlayerViewModel>(new {videoId = selectedVideo.Id});
        }

        public async Task<IEnumerable<Video>> LoadMoreItemsOverrideAsync(CancellationToken c, uint count)
        {
            base.Wait(true,"Загружаем видео..");
            var videos = await _apiService.GetVideos(PagingCount, _currentCount);
            _currentCount += PagingCount;
            base.Wait(false);
            return videos;
        }

        public bool HasMoreItemsOverride()
        {
            return true;
        }
    }
}
