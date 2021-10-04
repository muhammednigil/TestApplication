using System;
using System.Collections.Generic;
using System.Text;

namespace TestApplicationDomain
{
    public class PagedData<T> : IPagedData<T>
    {
        public IList<T> Data { get; set; }

        public int PageCount { get; set; }
        public int TotalItemCount { get; set; }
        public int PageIndex { get; set; }
        public int PageNumber { get { return PageIndex; } }

        public int PageSize { get; set; }

        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public bool IsFirstPage { get; set; }
        public bool IsLastPage { get; set; }
        public int ItemStart { get; set; }
        public int ItemEnd { get; set; }

        public void SetAll(IList<T> data, int _PageCount, int _TotalItemCount, int _PageIndex, bool _HasPreviousPage, bool _HasNextPage, bool _IsFirstPage, bool _IsLastPage, int _ItemStart, int _ItemEnd)
        {
            Data = data;
            PageCount = _PageCount;
            TotalItemCount = _TotalItemCount;
            PageIndex = _PageIndex;

            HasPreviousPage = _HasPreviousPage;
            HasNextPage = _HasNextPage;
            IsFirstPage = _IsFirstPage;
            IsLastPage = _IsLastPage;
            ItemStart = _ItemStart;
            ItemEnd = _ItemEnd;
        }

        public void SetData(IList<T> data)
        {
            this.Data = data;
        }
    }
}
