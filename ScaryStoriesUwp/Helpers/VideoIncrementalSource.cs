using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ScaryStoriesUniversal.Api.Entities;
using ScaryStoriesUwp.Shared.ViewModels.Base;

namespace ScaryStoriesUwp.Helpers
{
    public class VideoIncrementalSource:IncrementalSource<Video>
    {
        private readonly IIncrementalContainer<Video> _videocontainer;

        public VideoIncrementalSource(IIncrementalContainer<Video> videocontainer)
        {
            _videocontainer = videocontainer;
        }

        protected override Task<IEnumerable<Video>> LoadMoreItemsOverrideAsync(CancellationToken c, uint count)
        {
            return _videocontainer.LoadMoreItemsOverrideAsync(c, count);
        }

        protected override bool HasMoreItemsOverride()
        {
            return _videocontainer.HasMoreItemsOverride();
        }
    }
}
