using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Demo_App.Models
{
    public class Component
    {
        public Component(int OrderNumber)
        {
            this.OrderNumber = OrderNumber;
        }
        public Component()
        {

        }

        public Component(Component source)
        {
            ComponentId = source.ComponentId;
            ContainerId = source.ContainerId;
            ComponentName = source.ComponentName;
            ComponentType = source.ComponentType;
            Width = source.Width;
            OrderNumber = source.OrderNumber;
            RefValue = source.RefValue;
            Description = source.Description;
            ShowColumnModal = source.ShowColumnModal;
            ShowPopup = source.ShowPopup;

            if (source.componentProperties != null)
            {
                componentProperties = source.componentProperties.Select(prop => prop.GetProperty()).ToList();
            }
            else
            {
                componentProperties = new List<ComponentPropertyModel>();
            }
        }


        public int ComponentId { get; set; }
        public int? ContainerId { get; set; }
        public string? ComponentName { get; set; }
        public string? ComponentType { get; set; }
        public string? Width { get; set; }
        public int OrderNumber { get; set; }
        public int RefValue { get; set; }
        public string? Description { get; set; } = " ";
        public bool ShowColumnModal { get; set; } = false;
        public bool ShowPopup { get; set; } = false;
        public bool IsUpdating { get; set; } = false;
        public ICollection<ComponentPropertyModel> componentProperties { get; set; }
    }
}
