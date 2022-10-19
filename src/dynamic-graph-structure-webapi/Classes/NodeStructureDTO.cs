using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DynamicGraphStructure.WebApi
{
    public sealed class NodeStructureDTO
    {
        public Guid NodeId { get; set; } = Guid.Empty;
        public string NodeName { get; set; } = string.Empty;
        public string NodeType { get; set; } = string.Empty;
        public string ClassId { get; set; } = string.Empty;
        public string[] Attributes { get; set; } = { };
        public int State { get; set; }
        public LinkNode Link { get; set; } = new LinkNode();
        public ICollection<DataAlgorithm> Inputs { get; set; } = new List<DataAlgorithm>();
        public ICollection<DataAlgorithm> Outputs { get; set; } = new List<DataAlgorithm>();

    }
    /// <summary>
    /// связи между нодами
    /// </summary>
    public class LinkNode
    {
        public ICollection<string> Inputs { get; set; } = new List<string>();
        public ICollection<string> Outputs { get; set; } = new List<string>();
    }
    /// <summary>
    /// заменить на одноименный класс из common
    /// </summary>
    public class DataAlgorithm
    {
        public string Name { get; set; } = string.Empty;
        public bool IsNessery { get; set; }
        public string[] Attributes { get; set; } = { };
        public string Tag { get; set; } = string.Empty;

        public IDictionary<string, object> Props = new Dictionary<string, object>() { { "type", "Cur" } };
        public int? I { get; set; }
        public ICollection<string> Outputs { get; set; } = new List<string>();
    }
}
