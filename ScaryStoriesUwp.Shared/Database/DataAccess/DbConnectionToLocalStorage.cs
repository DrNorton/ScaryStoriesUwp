using System.Threading.Tasks;
using ScaryStoriesUwp.Shared.Database.Entities;

using SQLite;

namespace ScaryStoriesUwp.Shared.Database.DataAccess
{
    public class DbConnectionToLocalStorage : IDbConnection
    {
        private readonly string _pathtoLocalStorage;
        private readonly string _pathToLocalStoriesBackup;

        public DbConnectionToLocalStorage(string pathtoLocalStorage,string pathToLocalStoriesBackup)
        {
            _pathtoLocalStorage = pathtoLocalStorage;
            _pathToLocalStoriesBackup = pathToLocalStoriesBackup;
        }

        public async Task InitializeDatabases()
        {
           var conn = GetAsyncConnection(DatabaseType.Favorites);
           await  conn.CreateTableAsync<FavoriteStory>();
            var conn2 = GetAsyncConnection(DatabaseType.Backup);
            await conn2.CreateTableAsync<StoryBackupEntity>();
            await conn2.CreateTableAsync<PhotoBackupEntity>();
        }



        public SQLiteAsyncConnection GetAsyncConnection(DatabaseType type)
        {
            if (type == DatabaseType.Backup)
            {
                return new SQLiteAsyncConnection(_pathToLocalStoriesBackup,SQLiteOpenFlags.ReadWrite);
            }

            if (type == DatabaseType.Favorites)
            {
                return new SQLiteAsyncConnection(_pathtoLocalStorage);
            }
            return null;
        }
    }

    public enum DatabaseType
    {
        Backup,
        Favorites
    }
}
