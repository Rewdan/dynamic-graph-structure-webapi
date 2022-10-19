using System.Collections.Generic;

namespace DynamicGraphStructure.WebApi.Database.Models
{
    /// <summary>
    /// класс алгоритма: "MaterialStream", "Mixer",
    /// </summary>
    public sealed class ClassAlgorithm
    {
        public int Id { get; set; }
        /// <summary>
        /// "material_stream", "mixer", only lowwer case
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// материальный поток(например)
        /// по нему будет генерироваться название ноды по дефолту
        /// </summary>
        public string Description { get; set; }
        public ICollection<Algorithm> Algorithms { get; private set; }

    }
}
