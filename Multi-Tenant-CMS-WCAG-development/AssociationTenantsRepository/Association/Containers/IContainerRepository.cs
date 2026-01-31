using AssociationEntities.Models;

namespace AssociationRepository.Association
{
    public interface IContainerRepository
    {
        List<Container> GetAllContainersByID(int RowId);
        void DeleteContainerByRowId(int RowId);
        Task<int> CreateContainer(Container container);
    }
}
