using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScaryStoriesUwp.Shared.Database.Entities;
using ScaryStoriesUwp.Shared.Database.Repositories.Base;
using ScaryStoriesUwp.Shared.Services;
using ScaryStoriesUwp.Shared.ViewModels.Base;

namespace ScaryStoriesUwp.Shared.ViewModels
{
    public class FavoritesViewModel:LoadingScreen
    {
        private readonly IFavoriteStoriesRepository _favoriteStoriesRepository;
        private readonly StoryListIdsContainer _idsContainer;
        private List<FavoriteStory> _favorites;
        private FavoriteStory _selectedStory;
       

        public FavoritesViewModel(IFavoriteStoriesRepository favoriteStoriesRepository, StoryListIdsContainer idsContainer)
            :base("Избранные истории")
        {
            _favoriteStoriesRepository = favoriteStoriesRepository;
            _idsContainer = idsContainer;
        }

        public List<FavoriteStory> Favorites
        {
            get { return _favorites; }
            set
            {
                _favorites = value;
                base.RaisePropertyChanged(()=>Favorites);
            }
        }

        public FavoriteStory SelectedStory
        {
            get { return _selectedStory; }
            set
            {
                _selectedStory = value;
                if (value != null) ShowStories();
                base.RaisePropertyChanged(()=>SelectedStory);
            }
        }

        public override async void Start()
        {
            base.Start();
            LoadFavorites();
        }

        private void ShowStories()
        {
            _idsContainer.SetIds(_favorites.Select(x=>x.Id));
            _idsContainer.Position = _favorites.IndexOf(_selectedStory);
            ShowViewModel<StoryViewModel>();
        }

        private async Task LoadFavorites()
        {
            Favorites = (await _favoriteStoriesRepository.GetAll()).ToList();
        }
    }
}
