using System.Collections.Generic;
using System.Threading.Tasks;
using ScaryStoriesUwp.Shared.Database.Entities;

namespace ScaryStoriesUwp.Shared.Database.Repositories.Base
{
    public interface IFavoriteStoriesRepository
    {
        Task InsertAsync(FavoriteStory category);
        Task<IEnumerable<FavoriteStory>> GetAll();
        Task DeleteAsync(string id);
        Task<bool> CheckIsFavorite(string id);
        Task<IEnumerable<FavoriteStory>> GetFetch(int fetchCount, int count);
    }
}