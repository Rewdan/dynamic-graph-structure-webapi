using System;
using System.Collections.Generic;

namespace DynamicGraphStructure.WebApi.Database.Models
{
    /// <summary>
    /// элементарная нода, без связей
    /// </summary>
    public sealed class AlgorithmIOAttribute
    {
        public int Id { get; set; }
        public int AlgorithmIOId { get; set; }
        public int AttributeId { get; set; }
        public Attribute Attribute { get; set; }
        public AlgorithmIO AlgorithmIO { get; set; }

    }
}
