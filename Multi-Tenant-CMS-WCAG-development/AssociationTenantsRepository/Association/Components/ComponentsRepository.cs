using AssociationEntities.CustomModels;
using AssociationEntities.Models;
using Microsoft.EntityFrameworkCore;


namespace AssociationRepository.Association
{
    public class ComponentsRepository : IComponentsRepository
    {
        private readonly AssociationContext _associationContext;
        public ComponentsRepository(AssociationContext associationContext)
        {
            _associationContext = associationContext;
        }
        ~ComponentsRepository()
        {
            _associationContext.Dispose();
        }
        public async Task<int> CreateComponent(Component Item)
        {
            _associationContext.Components.Add(Item);
            var data = await _associationContext.SaveChangesAsync();
            int componentId = _associationContext.Components.Where(x => x.ContainerId == Item.ContainerId).OrderByDescending(x => x.ComponentId).FirstOrDefault().ComponentId;
            return componentId;
        }

        public async Task<int> CreateComponents(CreateComponents createComponentsRequest)
        {
            _associationContext.Components.AddRange(createComponentsRequest.Components);
            await _associationContext.SaveChangesAsync();
            return 1;
        }

        public async Task DeleteComponentByComponentId(int Id)
        {
            var data = _associationContext.Components.Where(x => x.ComponentId == Id);
            if (data != null)
            {
                _associationContext.Components.RemoveRange(data);
                await _associationContext.SaveChangesAsync();
            }
        }

        public async Task DeleteComponentByContainerId(int Id)
        {
            var data = _associationContext.Components.Where(x => x.ContainerId == Id).ToList();
            if (data != null)
            {
                _associationContext.Components.RemoveRange(data);
                await _associationContext.SaveChangesAsync();
            }
        }

        public List<Component> GetComponentByContainerId(int Id)
        {
            var data = _associationContext.Components.Where(x => x.ContainerId == Id).ToList();
            return data;
        }

        public async Task UpdateComponent(UpdateComponent updateComponent)
        {
            var data = _associationContext.Components
                .Where(x => x.ComponentId == updateComponent.ComponentId).ToList();
            data.ForEach(a =>
            {
                a.ComponentType = updateComponent.ComponentType;
                a.UpdatedOn = updateComponent.UpdateOn;
            });

            await _associationContext.SaveChangesAsync();
        }
    }
}
