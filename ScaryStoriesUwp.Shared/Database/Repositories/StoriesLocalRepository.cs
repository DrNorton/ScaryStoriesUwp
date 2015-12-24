using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using ScaryStoriesUniversal.Api.Entities;
using ScaryStoriesUwp.Shared.Database.DataAccess;
using ScaryStoriesUwp.Shared.Database.Entities;
using ScaryStoriesUwp.Shared.Database.Repositories.Base;
using SQLite;

namespace ScaryStoriesUwp.Shared.Database.Repositories
{
    public class StoriesLocalRepository : IStoriesLocalRepository
    {
        private SQLiteAsyncConnection _connection;

        public StoriesLocalRepository(IDbConnection connection)
        {
            _connection = connection.GetAsyncConnection(DatabaseType.Local);
        }
        public async Task<IEnumerable<Story>> GetStories(int limit, int offset)
        {
            try
            {
                var query = String.Format("Select Stories.*,Photos.Thumb from Stories INNER JOIN Photos On stories.PhotoId=Photos.Id LIMIT {0} OFFSET {1}", limit, offset);
                var stories = await _connection.QueryAsync<StoryBackupEntity>(query);
                return stories.Select(
                    x =>
                        new Story()
                        {
                            CategoryId = x.CategoryId,
                            Name = x.Name,
                            PhotoId = x.PhotoId,
                            Text = x.Text,
                            Thumb = x.Thumb,
                            Id = x.Id
                        }).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
