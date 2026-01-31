using AssociationEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationEntities.CustomModels
{
    public class CreateComponents
    {
        public List<Component> Components = new List<Component>();
    }

    public class UpdateComponent
    {
        public int ComponentId { get; set; }
        public string? ComponentType { get; set; }
        public DateTime UpdateOn { get; set; }
    }
}
