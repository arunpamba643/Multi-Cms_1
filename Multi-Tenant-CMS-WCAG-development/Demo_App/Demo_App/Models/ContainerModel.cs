namespace Demo_App.Models
{
    public class Container
    {
        public int ContainerId { get; set; }
        public int? RowId { get; set; }
        public string? ContainerName { get; set; }
        public int ContainerNo { get; set; }
        public int NoofContainers { get; set; }
        public DateTime? CreatedOn { get; set; }
        public virtual ICollection<Component>? Components { get; set; } = new List<Component>();
    }
}
