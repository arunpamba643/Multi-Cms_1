namespace WebsiteDemo.Models
{
    public class PaginationModel
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 24;
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(TotalItems, PageSize));
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        //public int ItemsFrom => CurrentPage * PageSize - (PageSize - 1);
        //public int ItemTo => ItemsFrom + (PageSize - 1);
    }
}
