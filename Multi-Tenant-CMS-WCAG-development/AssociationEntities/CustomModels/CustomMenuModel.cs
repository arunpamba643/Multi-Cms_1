namespace AssociationEntities.CustomModels.Models
{
    public class CustomMenuModel
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public int ParentMenuId { get; set; }
        public string PageUrl { get; set; }
        public int Position { get; set; }
    }
}
