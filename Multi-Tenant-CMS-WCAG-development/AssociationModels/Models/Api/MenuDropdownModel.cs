namespace AssociationEntities.Models.Api
{
    public class MenuDropdownModel
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public int ParentMenuId { get; set; }
        public string PageUrl { get; set; }
        public int Position { get; set; }

        public string ParentMenu { get; set; }
        public string Menu { get; set; }
    }

    public class MenuDragDropModel
    {
        public string Id { get; set;}
        public string Name { get; set; }
        public string ParentId { get; set; }
        public string Link { get; set; }
        public int Position { get; set; }
        public List<MenuDragDropModel> Children { get; set; } = new List<MenuDragDropModel>();
    }



}
