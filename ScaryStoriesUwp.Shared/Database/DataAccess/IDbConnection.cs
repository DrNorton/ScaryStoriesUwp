using System.Threading.Tasks;
using SQLite;

namespace ScaryStoriesUwp.Shared.Database.DataAccess
{
    public interface IDbConnection
    {
        Task InitializeLocalDatabase();
        SQLiteAsyncConnection GetAsyncConnection(DatabaseType type);
        Task InitFavoritesTables();
    }
}
