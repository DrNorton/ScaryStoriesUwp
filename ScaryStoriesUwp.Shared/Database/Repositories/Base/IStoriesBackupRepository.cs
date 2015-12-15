using System.Collections.Generic;
using System.Threading.Tasks;
using ScaryStoriesUniversal.Api;
using ScaryStoriesUniversal.Api.Entities;

namespace ScaryStoriesUwp.Shared.Database.Repositories.Base
{
    public interface IStoriesBackupRepository
    {
        Task<IEnumerable<Story>> GetStories(int limit, int offset);
    }
}