using System.Collections.Generic;

namespace DynamicGraphStructure.WebApi.Database.Models
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Graph
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Node> Nodes { get; private set; }
        public ICollection<NodeIO> NodeIOs { get; private set; }

    }
}