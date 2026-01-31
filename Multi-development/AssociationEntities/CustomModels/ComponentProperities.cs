using AssociationEntities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationEntities
{
    public class ComponentProperitiesRequest

    {
        public List<ComponentProperty> ComponentProperties { get; set; } = new List<ComponentProperty>();
    }
}
