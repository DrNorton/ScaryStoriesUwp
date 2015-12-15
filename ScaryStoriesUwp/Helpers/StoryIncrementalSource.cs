using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ScaryStoriesUniversal.Api.Entities;
using ScaryStoriesUwp.Shared.ViewModels.Base;

namespace ScaryStoriesUwp.Helpers
{
    public class StoryIncrementalSource:IncrementalSource<Story>
    {
        private readonly IIncrementalContainer<Story> _incrementalContainer;

        public StoryIncrementalSource(IIncrementalContainer<Story> incrementalContainer)
        {
            _incrementalContainer = incrementalContainer;
        }

        protected override Task<IEnumerable<Story>> LoadMoreItemsOverrideAsync(CancellationToken c, uint count)
        {
            return _incrementalContainer.LoadMoreItemsOverrideAsync(c, count);
        }

        protected override bool HasMoreItemsOverride()
        {
            return _incrementalContainer.HasMoreItemsOverride();
        }
    }
}
