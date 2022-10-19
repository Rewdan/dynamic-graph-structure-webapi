using System.Collections.Generic;

namespace DynamicGraphStructure.WebApi.Database.Models
{
    public sealed class Attribute
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? AttributeTypeId { get; set; }
        public int? AttributeForId { get; set; }
        public AttributeType? AttributeType { get; set; }
        /// <summary>
        /// если нулл, то будет использоваться для всех таблиц 
        /// (нужно придумать валидацию при сохранении связи, чтоб не сохранилиь свзя не к своей таблице)
        /// </summary>
        public AttributeFor? AttributeFor { get; set; }
        public ICollection<AlgorithmAttribute> AlgorithmAttributes { get; private set; }
        public ICollection<AlgorithmIOAttribute> AlgorithmIOAttributes { get; private set; }

    }
}
