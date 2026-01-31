using Demo_App.Data;

namespace Demo_App.Models
{
    public class RowModel
    {
        public int RowId { get; set; }
        public int? PageId { get; set; }
        public int? OrderNumber { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool ShowRowModal { get; set; } = false;
        public virtual ICollection<Container>? Containers { get; set; } = new List<Container>();
    }
}
