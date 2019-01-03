using System.Collections.Generic;

namespace MTest.Infra
{
    public class PagedResult<T>
    {
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int RecordsPerPage { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
