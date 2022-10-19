using System.Collections.Generic;

namespace DynamicGraphStructure.WebApi.Database.Models
{
    /// <summary>
    /// для какой таблицы атрибут (algorithm or algorithIO)
    /// скорее для валидации, чтобы атрибуты соответстволи своим смежным таблицам
    /// </summary>
    public sealed class AttributeFor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Attribute> Attributes { get; private set; }
    }
}