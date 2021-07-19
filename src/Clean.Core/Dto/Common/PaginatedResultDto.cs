using System.Collections.Generic;

namespace Clean.Core.Dto.Common
{
    public class PaginatedResultDto<T>
    {
        public ICollection<T> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
