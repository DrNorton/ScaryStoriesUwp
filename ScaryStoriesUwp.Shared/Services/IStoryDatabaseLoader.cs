using System.Threading.Tasks;

namespace ScaryStoriesUwp.Shared.Services
{
    public interface IStoryDatabaseLoader
    {
        Task<bool> IsDatabaseDownloaded();
        Task<long> DownloadNewDatabase();
    }
}