namespace TestApplicationDomain.DynamicQuery
{
    public class PaginationDescriptor
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }

        public static PaginationDescriptor NoPagination = new PaginationDescriptor
        {
            PageSize = int.MaxValue,
            PageIndex = 1
        };
    }
}

