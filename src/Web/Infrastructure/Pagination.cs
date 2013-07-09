using MvcContrib.Pagination;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ByteCarrot.Masslog.Web.Infrastructure
{
    public class Pagination<T> : IPagination<T>
    {
        private readonly IList<T> _list;

        public Pagination(IList<T> list, int totalItems, int pageSize, int pageNumber)
        {
            _list = list;
            TotalItems = totalItems;
            PageSize = pageSize;
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(TotalItems / (double)PageSize);
            FirstItem = (PageNumber - 1) * PageSize + 1;
            LastItem = FirstItem + _list.Count - 1;
            HasPreviousPage = PageNumber > 1;
            HasNextPage = PageNumber < TotalPages;
        }

        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public int TotalItems { get; private set; }

        public int TotalPages { get; private set; }

        public int FirstItem { get; private set; }

        public int LastItem { get; private set; }

        public bool HasPreviousPage { get; private set; }

        public bool HasNextPage { get; private set; }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}