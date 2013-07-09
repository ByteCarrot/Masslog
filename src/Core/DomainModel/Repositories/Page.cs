using System.Collections.Generic;

namespace ByteCarrot.Masslog.Core.DomainModel.Repositories
{
    public class Page<T>
    {
        public Page(IList<T> items, long totalItems, int pageSize, int pageNumber)
        {
            Items = items;
            TotalItems = totalItems;
            PageSize = pageSize;
            PageNumber = pageNumber;
        }

        public IList<T> Items { get; private set; }

        public long TotalItems { get; private set; }

        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }
    }
}
