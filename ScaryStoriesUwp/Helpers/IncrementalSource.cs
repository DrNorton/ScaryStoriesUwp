using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace ScaryStoriesUwp.Helpers
{
    public abstract class IncrementalSource<T> : ObservableCollection<T>, ISupportIncrementalLoading
    {
        #region State
        
        bool _busy = false;

        #endregion

        #region ISupportIncrementalLoading

        public bool HasMoreItems
        {
            get { return HasMoreItemsOverride(); }
        }

        public Windows.Foundation.IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            if (_busy)
            {
                throw new InvalidOperationException("Only one operation in flight at a time");
            }

            _busy = true;

            return AsyncInfo.Run((c) => LoadMoreItemsAsync(c, count));
        }

        #endregion

        #region INotifyCollectionChanged

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        #endregion

        #region Private methods

        async Task<LoadMoreItemsResult> LoadMoreItemsAsync(CancellationToken c, uint count)
        {
            try
            {
                var items = await LoadMoreItemsOverrideAsync(c, count);
                var baseIndex = this.Count;
                foreach (var item in items)
                {
                    this.Add(item);
                }
                

                // Now notify of the new items
                NotifyOfInsertedItems(baseIndex, items.Count());

                return new LoadMoreItemsResult { Count = (uint)(items.Count())} ;
            }
            finally
            {
                _busy = false;
            }
        }

        void NotifyOfInsertedItems(int baseIndex, int count)
        {
            if (CollectionChanged == null)
            {
                return;
            }

            for (int i = 0; i < count; i++)
            {
                var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, this[i + baseIndex], i + baseIndex);
                CollectionChanged(this, args);
            }
        }

        #endregion

        #region Overridable methods

        protected abstract Task<IEnumerable<T>> LoadMoreItemsOverrideAsync(CancellationToken c, uint count);
        protected abstract bool HasMoreItemsOverride();

        #endregion
       


    }
}
