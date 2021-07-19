namespace Clean.Core.Dto.Common
{
    public class PaginationInputDto
    {
        public PaginationInputDto()
        {
            PageNumber = 0;
            PageSize = int.MaxValue;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public int StartFrom {
            get { return PageNumber * PageSize; }
        }
    }
}
