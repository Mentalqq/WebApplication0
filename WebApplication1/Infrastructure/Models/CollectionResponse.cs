using System.Collections.Generic;

namespace WebApplication1.Infrastructure.Models
{
    public class CollectionResponse<T>
    {
        public int TotalCount { get; set; }

        public IEnumerable<T> Data { get; set; }
    }
}
