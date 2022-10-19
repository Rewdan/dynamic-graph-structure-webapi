using System.Collections.Generic;

namespace DynamicGraphStructure.WebApi.Database.Models
{
    /// <summary>
    /// определить тип исполнения функции 
    /// ( calc - простой расчет)
    /// ( train - обучить)
    /// ( anything - что-то еще)
    /// </summary>
    public sealed class Function
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<AlgorithmIO> AlgorithmIOs { get; private set; }

    }
}
