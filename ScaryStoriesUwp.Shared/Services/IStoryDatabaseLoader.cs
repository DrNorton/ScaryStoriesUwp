using System.Threading;
using System.Threading.Tasks;

namespace ScaryStoriesUwp.Shared.Services
{
    public interface IStoryDatabaseLoader
    {
        Task<long> DownloadNewDatabase(CancellationToken cancellationToken,IProgressDownloadReceiver receiver);
    }
}