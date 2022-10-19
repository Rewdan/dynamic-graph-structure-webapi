using System;
using System.Collections.Generic;

namespace DynamicGraphStructure.WebApi.Database.Models
{
    /// <summary>
    /// информация по связам между нодами, для отрисовки графа
    /// </summary>
    public sealed class NodeIO
    {
        public int Id { get; set; }
        /// <summary>
        /// нода на вход
        /// </summary>
        public Guid? NodeInId { get; set; }
        /// <summary>
        /// надо на выход
        /// </summary>
        public Guid? NodeOutId { get; set; }
        /// <summary>
        /// принадлежность к графу
        /// </summary>
        public int GraphId { get; set; }
        public Graph Graph { get; set; }
        public Node? NodeIn { get; set; }
        public Node? NodeOut { get; set; }
    }
}
