using System.Collections.Generic;

namespace DynamicGraphStructure.WebApi.Database.Models
{
    /// <summary>
    /// элементарный алгоритм, нельзя декомпозировать, без связей
    /// </summary>
    public sealed class Algorithm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClassId { get; set; }
        /// <summary>
        /// native, graph
        /// </summary>
        public int AlgorithmTypeId { get; set; }
        public ICollection<AlgorithmAttribute> AlgorithmAttributes { get; private set; }
        public ICollection<AlgorithmIO> AlgorithmIOs { get; private set; }
        public ICollection<Node> Nodes { get; private set; }
        public ClassAlgorithm Class { get; set; }
        public AlgorithmType AlgorithmType { get; set; }

    }
}
