using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScaryStoriesUwp.Shared.ViewModels.Base
{
    public interface IIncrementalContainer<T>
    {
        Task<IEnumerable<T>> LoadMoreItemsOverrideAsync(CancellationToken c, uint count);
        bool HasMoreItemsOverride();
    }
}
