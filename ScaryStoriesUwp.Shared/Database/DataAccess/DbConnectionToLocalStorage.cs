using System.Threading.Tasks;
using ScaryStoriesUwp.Shared.Database.Entities;

using SQLite;

namespace ScaryStoriesUwp.Shared.Database.DataAccess
{
    public class DbConnectionToLocalStorage : IDbConnection
    {
        private readonly string _pathtoFavoritesStorage;
        private readonly string _pathToLocalStoriesBackup;

        public DbConnectionToLocalStorage(string pathtoFavoritesStorage,string pathToLocalStoriesBackup)
        {
            _pathtoFavoritesStorage = pathtoFavoritesStorage;
            _pathToLocalStoriesBackup = pathToLocalStoriesBackup;
        }

        public async Task InitializeLocalDatabase()
        {
           
            var conn2 = GetAsyncConnection(DatabaseType.Local);
            await conn2.CreateTableAsync<StoryBackupEntity>();
            await conn2.CreateTableAsync<PhotoBackupEntity>();
        }

        public async Task InitFavoritesTables()
        {
            var conn = GetAsyncConnection(DatabaseType.Favorites);
            await conn.CreateTableAsync<FavoriteStory>();
        }



        public SQLiteAsyncConnection GetAsyncConnection(DatabaseType type)
        {
            if (type == DatabaseType.Local)
            {
                return new SQLiteAsyncConnection(_pathToLocalStoriesBackup,SQLiteOpenFlags.ReadWrite);
            }

            if (type == DatabaseType.Favorites)
            {
                return new SQLiteAsyncConnection(_pathtoFavoritesStorage);
            }
            return null;
        }
    }

    public enum DatabaseType
    {
        Local,
        Favorites
    }
}
