using System.Collections.Generic;

namespace DynamicGraphStructure.WebApi.Database.Models
{
    /// <summary>
    /// элементарная нода, без связей
    /// </summary>
    public sealed class AlgorithmIO
    {
        public int Id { get; set; }
        /// <summary>
        /// 1 вход / 0 выход
        /// </summary>
        public int TypeIO { get; set; }
        public int FunctionId { get; set; }
        /// <summary>
        /// пропсы предобработки параметра
        /// </summary>
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public string Props { get; set; }
        /// <summary>
        /// индекс входа
        /// </summary>
        public int? Index { get; set; }
        /// <summary>
        /// обязательный параметр?
        /// </summary>
        public bool IsNecesse { get; set; }
        public int AlgorithmId { get; set; }
        public Algorithm Algorithm { get; set; }
        public Function Function { get; set; }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public ICollection<AlgorithmIOAttribute> AlgorithmIOAttributes { get; private set; }

    }
}
