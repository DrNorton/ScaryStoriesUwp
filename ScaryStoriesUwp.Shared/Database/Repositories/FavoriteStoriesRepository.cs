using System.Collections.Generic;
using System.Threading.Tasks;
using ScaryStoriesUwp.Shared.Database.DataAccess;
using ScaryStoriesUwp.Shared.Database.Entities;
using ScaryStoriesUwp.Shared.Database.Repositories.Base;
using SQLite;

namespace ScaryStoriesUwp.Shared.Database.Repositories
{
    public class FavoriteStoriesRepository : IFavoriteStoriesRepository
    {
        private readonly SQLiteAsyncConnection _connection;

        public FavoriteStoriesRepository(IDbConnection connection)
        {
            _connection = connection.GetAsyncConnection(DatabaseType.Favorites);
        }

        public async Task InsertAsync(FavoriteStory story)
        {
            await _connection.InsertAsync(story);
        }

        public async Task<IEnumerable<FavoriteStory>> GetAll()
        {
            return await _connection.Table<FavoriteStory>().OrderBy(x=>x.CreatedAt).ToListAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var item = await _connection.FindAsync<FavoriteStory>(x => x.Id == id);
            if (item != null)
            {
                await _connection.DeleteAsync(item);
            }
           
        }

        public async Task<bool> CheckIsFavorite(string id)
        {
            var item = await _connection.FindAsync<FavoriteStory>(x => x.Id == id);
            return (item != null);
        }


        public async Task<IEnumerable<FavoriteStory>> GetFetch(int limit, int offset)
        {
           var stories=await _connection.Table<FavoriteStory>().OrderBy(x => x.CreatedAt).Skip(offset).Take(limit).ToListAsync();
            return stories;
        }


    }
}
