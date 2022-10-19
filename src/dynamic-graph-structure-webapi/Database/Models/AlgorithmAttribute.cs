using System.Collections.Generic;
using DynamicGraphStructure.WebApi.Database;

namespace DynamicGraphStructure.WebApi.Database.Models;

public sealed class AlgorithmAttribute
{
    public int Id { get; set; }
    public int AttributeId { get; set; }
    public int AlgorithmId { get; set; }
    public Attribute Attribute { get; set; }
    public Algorithm Algorithm { get; set; }

}

