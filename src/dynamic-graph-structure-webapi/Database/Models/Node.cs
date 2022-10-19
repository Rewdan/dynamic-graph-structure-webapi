using System;
using System.Collections.Generic;

namespace DynamicGraphStructure.WebApi.Database.Models
{
    public sealed class Node
    {
        public Guid Id { get; set; }
        /// <summary>
        /// название ноды: "Материальный\nпоток_0", "Материальный миксер",
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// принадлежность ноды к алгоритму
        /// </summary>
        public int AlgorithmId { get; set; }
        public int GraphId { get; set; }
        /// <summary>
        /// уровень ноды в графе
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// принадлежность к графу
        /// </summary>
        public Graph Graph { get; set; }
        public Algorithm Algorithm { get; set; }
        public ICollection<NodeIO>? NodeIns { get; private set; }
        public ICollection<NodeIO>? NodeOuts { get; private set; }
        public ICollection<NodeRefIO>? NodeRefIOs { get; private set; }

    }
}
