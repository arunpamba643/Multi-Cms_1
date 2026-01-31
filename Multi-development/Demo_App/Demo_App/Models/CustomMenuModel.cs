using System.ComponentModel.DataAnnotations;

namespace Demo_App.Models
{
    public class CustomMenuModel
    {
        public int MenuId { get; set; }
        [Required(ErrorMessage = "Menu Name is Mandatory")]
        public string MenuName { get; set; }
        public int ParentMenuId { get; set; }
        public string PageUrl { get; set; }
        public int Position { get; set; }
        public int PageId { get; set; }
        // Add this property to your model or create a separate property


    }
}
