using AssociationBusiness.Handlers;
using Demo_App.Models;


namespace Demo_App.Services
{
    public interface IComponentService
    {
        Task<int> CreateComponent(CreateComponentRequest Item);
        Task CreateComponents(CreateComponentsRequest Item);
        Task DeleteComponentByComponentId(int Id);
        Task UpdateComponent(UpdateComponentRequest updateComponentRequest);
    }
}
