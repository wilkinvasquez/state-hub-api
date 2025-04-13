namespace SB.StateHub.API.DTOs.Pagination
{
    public class PaginationDto
    {
        private string _filter = "";

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string Filter
        {
            set { _filter = value == null ? "" : value; }
            get { return _filter; }
        }
    }
}