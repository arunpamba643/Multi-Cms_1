using AssociationEntities.CustomModels;
using AssociationEntities.Models;

namespace AssociationRepository.Association
{
    public class ContainerRepository : IContainerRepository
    {
        private readonly AssociationContext _associationContext;
        public ContainerRepository(AssociationContext associationContext)
        {
            _associationContext = associationContext;
        }
        ~ContainerRepository()
        {
            _associationContext.Dispose();
        }
        public List<Container> GetAllContainersByID(int RowId)
        {
            var data = _associationContext.Containers.Where(x => x.RowId == RowId).OrderBy(x => x.RowId).ToList();
            return data;
        }

        public void DeleteContainerByRowId(int RowId)
        {
            var containers = _associationContext.Containers.Where(x => x.RowId == RowId).ToList();
            if (containers != null)
            {
                _associationContext.Containers.RemoveRange(containers);
                _associationContext.SaveChanges();
            }
        }

        public async Task<int> CreateContainer(Container container)
        {
            _associationContext.Containers.Add(container);
            var data = await _associationContext.SaveChangesAsync();
            int containerId = _associationContext.Containers.Where(x => x.RowId == container.RowId).OrderByDescending(x => x.ContainerId).FirstOrDefault().ContainerId;
            return containerId;
        }
    }
}
