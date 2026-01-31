using AssociationEntities.CustomModels;
using AssociationEntities.Models;

namespace AssociationRepository.Association
{
    public interface IComponentsRepository
    {
        Task<int> CreateComponent(Component Item);
        Task<int> CreateComponents(CreateComponents createComponentsRequest);
        Task UpdateComponent(UpdateComponent updateComponent);
        List<Component> GetComponentByContainerId(int Id);
        Task DeleteComponentByComponentId(int Id);
        Task DeleteComponentByContainerId(int Id);
    }
}
