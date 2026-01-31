using AssociationEntities;
using AssociationEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationRepository.Association
{
    public class ComponentPropertiesRespository : IComponentPropertiesRespository
    {
        private readonly AssociationContext _associationContext;
        public ComponentPropertiesRespository(AssociationContext associationContext)
        {
            _associationContext = associationContext;
        }
        ~ComponentPropertiesRespository()
        {
            _associationContext.Dispose();
        }

        public async Task<int> CreateComponentProperties(ComponentProperitiesRequest componentProperitiesRequest)
        {

            try
            {
                _associationContext.ComponentProperties.AddRange(componentProperitiesRequest.ComponentProperties);
                var data = await _associationContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
                return 0;
            }

            return 1;
        }

        public async Task<int> CreateComponentProperty(ComponentProperty componentProperty)
        {
            try
            {
                _associationContext.ComponentProperties.Add(componentProperty);
                var data = await _associationContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
                return 0;
            }

            return 1;
        }

        public async Task<int> DeleteComponentProperitesByComponentId(int ComponentId)
        {
            var data = _associationContext.ComponentProperties.Where(x => x.ComponentId == ComponentId).ToList();
            var component = _associationContext.Components.Where(x => x.ComponentId == ComponentId).ToList();

            if (component != null)
            {
                component.ForEach(a =>
                {
                    a.ComponentType = null;
                    a.UpdatedOn = DateTime.Now;
                });
                await _associationContext.SaveChangesAsync();
            }

            if (data != null)
            {
                _associationContext.RemoveRange(data);
                await _associationContext.SaveChangesAsync();

            }

            return 0;
        }
    }
}
