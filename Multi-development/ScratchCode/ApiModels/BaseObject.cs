namespace ScratchCode.ApiModels
{
    public abstract class BaseObject
    {
        public string Name { get; set; }
        public Guid   Id   { get; set; }
    }
}