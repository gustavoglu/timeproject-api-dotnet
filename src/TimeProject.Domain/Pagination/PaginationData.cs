using System.Collections.Generic;

namespace TimeProject.Domain.Pagination
{
    public class PaginationData<T>
    {
        public PaginationData(IEnumerable<T> data, int? limit = 30, int? page = 1, long? total = 0)
        {
            Data = data;
            Limit = limit ?? 330;
            Page = page ?? 1;
            Total = total ?? 0;
        }

        public IEnumerable<T> Data { get; set; }
        public int Limit { get; set; }
        public int Page { get; set; }
        public long Total { get; set; }
    }
}
