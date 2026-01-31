namespace Demo_App.Models
{
    public class ComponentPropertyModel
    {

        public int ComponentId { get; set; }
        public string? Key { get; set; }  
        public string? Value { get; set; }

        public ComponentPropertyModel GetProperty()
        {
            return new ComponentPropertyModel
            {
                ComponentId = this.ComponentId,
                Key = this.Key,
                Value = this.Value
            };
        }
    }
}
