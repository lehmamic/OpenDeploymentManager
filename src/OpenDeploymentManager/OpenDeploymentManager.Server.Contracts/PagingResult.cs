using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OpenDeploymentManager.Server.Contracts
{
    public class PagingResult<T> : IEnumerable<T>
    {
        public PagingResult(IEnumerable<T> items, Uri nextLinkCount, long? count)
        {
            this.Items = items.ToArray();
            this.NextLinkCount = nextLinkCount;
            this.Count = count;
        }

        public T[] Items { get; set; }

        public Uri NextLinkCount { get; set; }

        public long? Count { get; set; }

        public Uri NextPageLink { get; set; }

        #region Implementation of IEnumerable
        public IEnumerator<T> GetEnumerator()
        {
            IEnumerable<T> items = this.Items ?? new T[0];
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion
    }
}