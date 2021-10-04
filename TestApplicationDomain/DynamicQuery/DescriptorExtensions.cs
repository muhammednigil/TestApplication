using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

namespace TestApplicationDomain.DynamicQuery
{
    public static class DescriptorExtensions
    {
        public static IQueryable<T> ApplyDescriptor<T, TProperties>(this IQueryable<T> query, DataDescriptor descriptor)
        {
            if (descriptor == null)
                return query;

            if (descriptor.Order != null && !string.IsNullOrEmpty(descriptor.Order.OrderBy))
            {
                var property = typeof(TProperties).GetProperties().FirstOrDefault(e => string.Equals(e.Name, descriptor.Order.OrderBy, StringComparison.InvariantCultureIgnoreCase));

                if (property == null)
                    throw new DescriptorException("Invalid OrderBy property name.");

                var attr = property.GetCustomAttribute<SourcePropertyAttribute>();
                var propertyName = property.Name;
                if (attr != null)
                    propertyName = attr.Name;

                descriptor.Order.OrderBy = propertyName;

                query = ApplyOrderByInner(query, descriptor.Order);
            }
            if (descriptor.Filter != null)
            {
                foreach (var filterDescription in descriptor.Filter)
                {
                    var property = typeof(TProperties).GetProperties().FirstOrDefault(e => string.Equals(e.Name, filterDescription.FilterBy, StringComparison.InvariantCultureIgnoreCase));

                    if (property == null)
                        throw new DescriptorException("Invalid FilterBy property name.");

                    var attr = property.GetCustomAttribute<SourcePropertyAttribute>();
                    var propertyName = property.Name;
                    if (attr != null)
                    {
                        propertyName = attr.Name;
                        property = typeof(T).GetProperty(propertyName);
                    }

                    filterDescription.FilterBy = propertyName;

                    query = ApplyFilterInner(query, filterDescription, property.PropertyType);
                }
            }
            return query;
        }

        public static IQueryable<T> ApplyDescriptor<T>(this IQueryable<T> query, DataDescriptor descriptor)
        {
            if (descriptor.Order != null && !string.IsNullOrEmpty(descriptor.Order.OrderBy))
            {
                var property = typeof(T).GetProperties().FirstOrDefault(e => string.Equals(e.Name, descriptor.Order.OrderBy, StringComparison.InvariantCultureIgnoreCase));

                if (property == null)
                    throw new DescriptorException("Invalid OrderBy property name.");

                descriptor.Order.OrderBy = property.Name;

                query = ApplyOrderByInner(query, descriptor.Order);
            }

            if (descriptor.Filter != null)
            {
                foreach (var filterDescription in descriptor.Filter)
                {
                    var property = typeof(T).GetProperties().FirstOrDefault(e => string.Equals(e.Name, filterDescription.FilterBy, StringComparison.InvariantCultureIgnoreCase));

                    if (property == null)
                        throw new DescriptorException("Invalid OrderBy property name.");

                    filterDescription.FilterBy = property.Name;

                    query = ApplyFilterInner(query, filterDescription, property.PropertyType);
                }
            }

            return query;
        }

        public static IPagedData<T> ApplyDescriptorWithPagination<T>(this IQueryable<T> query, DataDescriptor descriptor)
        {
            if (descriptor == null)
                descriptor = new DataDescriptor();
            if (descriptor.Pagination == null || descriptor.Pagination.PageSize == -1)
                descriptor.Pagination = PaginationDescriptor.NoPagination;


            return query.ApplyDescriptor(descriptor).ApplyPagination(descriptor);
        }

        public static IPagedData<TResult> ApplyDescriptorWithPagination<T, TResult>(this IQueryable<T> query, DataDescriptor descriptor, Func<T, TResult> transform)
        {
            if (descriptor == null)
                descriptor = new DataDescriptor();
            if (descriptor.Pagination == null || descriptor.Pagination.PageSize == -1)
                descriptor.Pagination = PaginationDescriptor.NoPagination;


            return query.ApplyDescriptor<T, TResult>(descriptor).AsEnumerable().Select(transform).AsQueryable().ApplyPagination(descriptor);
        }

        public static IPagedData<T> ApplyPagination<T>(this IQueryable<T> query, DataDescriptor descriptor)
        {
            var result = new PagedData<T>();
            var totalCount = query.Count();

            if (descriptor == null)
                descriptor = new DataDescriptor();

            if (descriptor.Pagination == null)
                descriptor.Pagination = PaginationDescriptor.NoPagination;

            var pageSize = descriptor.Pagination.PageSize;
            var pageIndex = descriptor.Pagination.PageIndex;

            result.TotalItemCount = totalCount;
            result.PageSize = descriptor.Pagination.PageSize;
            result.PageIndex = descriptor.Pagination.PageIndex;
            result.PageCount = result.TotalItemCount > 0 ? (int)Math.Ceiling(result.TotalItemCount / (double)pageSize) : 0;
            result.HasPreviousPage = (pageIndex > 1);
            result.HasNextPage = (pageIndex < (result.PageCount));
            result.IsFirstPage = (pageIndex == 1);
            result.IsLastPage = (pageIndex >= (result.PageCount));
            result.ItemStart = (pageIndex - 1) * pageSize + 1;
            result.ItemEnd = Math.Min((pageIndex - 1) * pageSize + pageSize, result.TotalItemCount);

            if (pageSize > 0 && pageIndex == 1)
            {
                query = query.Take(pageSize);
            }
            else if (pageIndex > 1)
            {
                query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }

            result.Data = query.ToList();
            return result;
        }

        private static IQueryable<T> ApplyOrderByInner<T>(IQueryable<T> query, OrderDescriptor descriptor)
        {

            var argEx = Expression.Parameter(typeof(T), "e");
            var propertyEx = Expression.Property(argEx, descriptor.OrderBy);
            var expression = Expression.Lambda<Func<T, object>>(Expression.Convert(propertyEx, typeof(object)), argEx);

            if (string.IsNullOrEmpty(descriptor.OrderType))
                descriptor.OrderType = DynamicConstants.OrderType.Ascending;

            if (descriptor.OrderType.ToLower() == DynamicConstants.OrderType.Ascending)
                query = query.OrderBy(expression);
            else if (descriptor.OrderType.ToLower() == DynamicConstants.OrderType.Descending)
                query = query.OrderByDescending(expression);
            else
                throw new DescriptorException("Invalid order type.");

            return query;
        }

        private static IQueryable<T> ApplyFilterInner<T>(IQueryable<T> query, FilterDescription descriptor, Type propertyType)
        {

            var argEx = Expression.Parameter(typeof(T), "e");
            var propertyEx = Expression.Property(argEx, descriptor.FilterBy);

            Expression body;
            if (descriptor.FilterType.ToLower() == DynamicConstants.FilterType.Equal)
            {
                body = Expression.Equal(propertyEx, Expression.Constant(descriptor.Value));
            }
            else if (descriptor.FilterType.ToLower() == DynamicConstants.FilterType.Contain)
            {
                body = Expression.Call(propertyEx, propertyType.GetMethod("Contains", new[] { typeof(string), typeof(StringComparison) }), Expression.Constant(descriptor.Value), Expression.Constant(StringComparison.OrdinalIgnoreCase));
            }
            else
                throw new DescriptorException("Invalid filter type.");

            var expression = Expression.Lambda<Func<T, bool>>(body, argEx);

            query = query.Where(expression);

            return query;
        }
    }

    [Serializable]
    public class DescriptorException : System.Exception
    {
        public DescriptorException()
        {
        }

        public DescriptorException(string message) : base(message)
        {
        }

        public DescriptorException(string message, System.Exception inner) : base(message, inner)
        {
        }

        protected DescriptorException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}


