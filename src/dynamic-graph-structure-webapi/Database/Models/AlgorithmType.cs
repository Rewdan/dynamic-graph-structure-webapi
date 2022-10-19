using System.Collections.Generic;

namespace DynamicGraphStructure.WebApi.Database.Models
{
    /// <summary>
    /// тип алгоритма, нативная функция вызов или структурный граф(пока не додумал как лучше сделать)
    /// </summary>
    public sealed class AlgorithmType
    {
        public int Id { get; set; }
        /// <summary>
        /// native, graph, structure
        /// </summary>
        public string Name { get; set; }
        public ICollection<Algorithm> Algorithms { get; private set; }

    }
}
