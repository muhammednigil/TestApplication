using System;
using System.Collections.Generic;
using System.Text;

namespace TestApplicationDomain
{
    public interface IPagedData<T>
    {
        int PageCount { get; }
        int TotalItemCount { get; }
        int PageIndex { get; }
        int PageNumber { get; }
        int PageSize { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
        bool IsFirstPage { get; }
        bool IsLastPage { get; }
        int ItemStart { get; }
        int ItemEnd { get; }
        IList<T> Data { get; }

        void SetData(IList<T> data);

        void SetAll(IList<T> data, int _PageCount, int _TotalItemCount, int _PageIndex, bool _HasPreviousPage, bool _HasNextPage, bool _IsFirstPage, bool _IsLastPage, int _ItemStart, int _ItemEnd);
    }
}
