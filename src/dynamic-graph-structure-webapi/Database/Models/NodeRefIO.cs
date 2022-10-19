using System;
using System.Collections.Generic;

namespace DynamicGraphStructure.WebApi.Database.Models
{
    /// <summary>
    /// настроенные входа и выхода 
    /// параметров по каждоый ноде
    /// </summary>
    public sealed class NodeRefIO
    {
        public int Id { get; set; }
        public int Index { get; set; }
        /// <summary>
        /// через NodeId смотреть algrithm
        /// </summary>
        public Guid NodeId { get; set; }
        /// <summary>
        /// 0 - читать значение по тегу?
        /// 1 - уже забито значение?
        /// </summary>
        public int TypeRef { get; set; }
        /// <summary>
        /// json
        /// { "tag":"tagName", "value": 3.2}
        /// если typeRef = 0 значение будет браться по тегу (поле tag)
        /// если typeRef = 1 значение будет браться из поля value
        /// </summary>
        public string DataRef { get; set; } = null!;
        public Node Node { get; set; }
    }
}
