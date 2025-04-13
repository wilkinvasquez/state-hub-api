namespace SB.StateHub.API.DTOs.Pagination
{
    public class PaginationResponseDto<T> where T : class
    {
        public IEnumerable<T>? Items { get; set; }
        public int Total { get; set; }
    }
}