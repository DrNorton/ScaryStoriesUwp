using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using ScaryStoriesUniversal.Api;
using ScaryStoriesUniversal.Api.Entities;
using ScaryStoriesUwp.Shared.Database.DataAccess;
using ScaryStoriesUwp.Shared.Database.Repositories;
using ScaryStoriesUwp.Shared.Database.Repositories.Base;
using ScaryStoriesUwp.Shared.Services;
using ScaryStoriesUwp.Shared.ViewModels.Base;

namespace ScaryStoriesUwp.Shared.ViewModels
{
    public class MainViewModel:LoadingScreen, IIncrementalContainer<Story>
    {
        private readonly IApiService _apiService;
        private readonly StoryListIdsContainer _idsContainer;
        private readonly IStoryDatabaseLoader _storyDatabaseLoader;
        private const int PagingCount=10;
        private int _currentCount = 0;
        private Story _selectedStory;
        private List<string> _loadedStoriesIds;


        public MainViewModel(IApiService apiService, StoryListIdsContainer idsContainer,IStoryDatabaseLoader storyDatabaseLoader) 
            : base("Главная")
        {
            _apiService = apiService;
            _idsContainer = idsContainer;
           _storyDatabaseLoader = storyDatabaseLoader;
            _loadedStoriesIds=new List<string>();
            CreateTables();
            Load();
        }

        private async void CreateTables()
        {
            try
            {
                await _storyDatabaseLoader.CopyDatabase();
                await Mvx.Resolve<IDbConnection>().InitializeDatabases();
               
            }
            catch (Exception e)
            {
                
            }

        }


        public Story SelectedStory
        {
            get { return _selectedStory; }
            set
            {
                _selectedStory = value;
                if (value != null) ShowStories();
                base.RaisePropertyChanged(()=>SelectedStory);
            }
        }

        private void ShowStories()
        {
            _idsContainer.SetIds(_loadedStoriesIds);
            _idsContainer.Position = _loadedStoriesIds.IndexOf(_selectedStory.Id);
            ShowViewModel<StoryViewModel>();
        }

        private async void Load()
        {
          
        }

        public async Task<IEnumerable<Story>> LoadMoreItemsOverrideAsync(CancellationToken c, uint count)
        {
            base.Wait(true,"Загружаем истории..");
            var stories=await  _apiService.GetStories(PagingCount, _currentCount);
            base.Wait(false);
            _loadedStoriesIds.AddRange(stories.Select(x => x.Id).ToList());
            _currentCount += PagingCount;
            return stories;
        }

        public bool HasMoreItemsOverride()
        {
            return true;
        }
    }
}
