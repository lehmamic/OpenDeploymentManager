using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenDeploymentManager.Server.Contracts
{
    public class PagingResult<T> // : IEnumerable<T>
    {
        public PagingResult()
        {
            this.Items = new T[0];
        }

        public PagingResult(IEnumerable<T> items, long? totalCount)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            this.Items = items.ToArray();
            this.TotalCount = totalCount;
        }

        public T[] Items { get; set; }

        public long? TotalCount { get; set; }

        ////#region Implementation of IEnumerable
        ////public IEnumerator<T> GetEnumerator()
        ////{
        ////    IEnumerable<T> items = this.Items ?? new T[0];
        ////    return items.GetEnumerator();
        ////}

        ////IEnumerator IEnumerable.GetEnumerator()
        ////{
        ////    return this.GetEnumerator();
        ////}
        ////#endregion
    }
}