namespace ScratchCode.ApiModels
{
    public class BaseListWithQueryParameters<T> : BaseList<T>
    {
        public object QueryParameters { get; set; }
    }
}