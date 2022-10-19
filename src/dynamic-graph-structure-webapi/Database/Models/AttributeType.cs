using System.Collections;
using System.Collections.Generic;

namespace DynamicGraphStructure.WebApi.Database.Models
{
    public sealed class AttributeType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Attribute> Attributes { get; set; }
    }
}
