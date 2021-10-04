using System;
using System.Collections.Generic;
using System.Text;

namespace TestApplicationDomain.DynamicQuery
{
    public class DynamicConstants
    {
        public class FilterType
        {
            public const string Equal = "equal";
            public const string Contain = "contain";
        }
        public class OrderType
        {
            public const string Ascending = "asc";
            public const string Descending = "desc";
        }
    }
}
