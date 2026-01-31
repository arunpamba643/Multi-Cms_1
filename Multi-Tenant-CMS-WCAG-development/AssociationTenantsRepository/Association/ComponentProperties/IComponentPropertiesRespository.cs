using AssociationEntities;
using AssociationEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationRepository.Association
{
    public interface IComponentPropertiesRespository
    {
        public Task<int> CreateComponentProperty(ComponentProperty componentProperties);
        public Task<int> CreateComponentProperties(ComponentProperitiesRequest componentProperitiesRequest);
        public Task<int> DeleteComponentProperitesByComponentId(int ComponentId);
    }
}
