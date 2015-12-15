using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScaryStoriesUniversal.Api;
using ScaryStoriesUniversal.Api.Entities;
using ScaryStoriesUwp.Shared.Database.Repositories.Base;

namespace ScaryStoriesUwp.Shared.Services.OnlineOfflineStoryService
{
    public class StoriesStorage:IApiService
    {
        private readonly IApiService _apiService;
        private readonly IStoriesBackupRepository _storiesBackupRepository;
        private readonly ISettingsProvider _settingsProvider;

        public StoriesStorage(IApiService apiService,IStoriesBackupRepository storiesBackupRepository,ISettingsProvider settingsProvider)
        {
            _apiService = apiService;
            _storiesBackupRepository = storiesBackupRepository;
            _settingsProvider = settingsProvider;
        }

        public Task<IEnumerable<Story>> GetStories(int limit, int offset)
        {
            if (_settingsProvider.IsOffline)
            {
                return _storiesBackupRepository.GetStories(limit, offset); 
            }
            else
            {
                return _apiService.GetStories(limit, offset);
            }
        }

        public Task<IEnumerable<Story>> GetByCategory(string categoryId, int limit, int offset)
        {
            return _apiService.GetByCategory(categoryId, limit, offset);
        }

        public Task<IEnumerable<Story>> GetBySourceId(string sourceId, int limit, int offset)
        {
            return _apiService.GetBySourceId(sourceId, limit, offset);
        }

        public Task<Story> GetStory(string storyId)
        {
            return _apiService.GetStory(storyId);
        }

        public Task<IEnumerable<Category>> GetCategories()
        {
            return _apiService.GetCategories();
        }

        public Task<IEnumerable<Source>> GetSources(int limit, int offset)
        {
            return _apiService.GetSources(limit, offset);
        }

        public Task<IEnumerable<Photo>> GetPhotos(int limit, int offset)
        {
            return _apiService.GetPhotos(limit, offset);
        }

        public Task<Photo> GetPhoto(string storyId)
        {
            return _apiService.GetPhoto(storyId);
        }

       
        public Task<IEnumerable<Story>> FindStories(string search, int limit, int offset)
        {
            return _apiService.FindStories(search, limit, offset);
        }

        public Task<IEnumerable<Video>> GetVideos(int limit, int offset)
        {
            return _apiService.GetVideos(limit, offset);
        }

        public Task<Video> GetVideo(string videoId)
        {
            return _apiService.GetVideo(videoId);
        }

        public Task<Source> GetSource(string sourceId)
        {
            return _apiService.GetSource(sourceId);
        }

        public Task<IEnumerable<Video>> GetVideosBySourceId(string sourceId, int limit, int offset)
        {
            return _apiService.GetVideosBySourceId(sourceId, limit, offset);
        }
    }
}
