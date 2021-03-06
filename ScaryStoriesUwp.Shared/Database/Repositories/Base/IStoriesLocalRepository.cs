using System.Collections.Generic;
using System.Threading.Tasks;
using ScaryStoriesUniversal.Api.Entities;

namespace ScaryStoriesUwp.Shared.Database.Repositories.Base
{
    public interface IStoriesLocalRepository
    {
        Task<IEnumerable<Story>> GetStories(int limit, int offset);
    }
}