namespace AssociationEntities.Models.Api
{
    public class CustomMenuModel
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public int ParentMenuId { get; set; }
        public string PageUrl { get; set; }
        public int Position { get; set; }

        public string ParentMenu { get; set; }
        public string Menu { get; set; }
    }
}
