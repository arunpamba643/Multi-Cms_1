using AssociationBusiness.Handlers;

namespace Demo_App.Services
{
    public interface IComponentPropertyService
    {
        Task<int> CreateComponentProperties(CreateComponentPropertiesRequest createComponentPropertiesRequest);
        Task<int> DeleteComponentPropertiesByComponentId(int ComponentId);

    }
}
