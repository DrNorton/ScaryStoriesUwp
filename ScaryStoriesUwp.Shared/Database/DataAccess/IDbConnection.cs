using System.Threading.Tasks;
using SQLite;

namespace ScaryStoriesUwp.Shared.Database.DataAccess
{
    public interface IDbConnection
    {
        Task InitializeDatabases();
        SQLiteAsyncConnection GetAsyncConnection(DatabaseType type);
    }
}
