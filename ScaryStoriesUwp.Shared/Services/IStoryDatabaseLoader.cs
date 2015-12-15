using System.Threading.Tasks;

namespace ScaryStoriesUwp.Shared.Services
{
    public interface IStoryDatabaseLoader
    {
        Task CopyDatabase();
        Task<bool> IsDatabaseDownloaded(string fileName);
    }
}