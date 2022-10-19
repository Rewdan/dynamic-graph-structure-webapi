using Microsoft.EntityFrameworkCore;
using DynamicGraphStructure.WebApi.Database.Models;
using System;

namespace DynamicGraphStructure.WebApi.Database;

public sealed class DGSContextDb : DbContext
{
    public const string SCHEMA = "dgs";
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
    public DGSContextDb(DbContextOptions dbContextOptions) : base(dbContextOptions)
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
    {
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    public DbSet<Algorithm> Algorithms { get; private set; }
    public DbSet<AlgorithmAttribute> AlgorithmAttributes { get; private set; }
    public DbSet<AlgorithmIO> AlgorithmIOs { get; private set; }
    public DbSet<AlgorithmIOAttribute> AlgorithmIOAttributes { get; private set; }
    public DbSet<Models.Attribute> Attributes { get; private set; }
    public DbSet<AttributeType> AttributeTypes { get; private set; }
    public DbSet<Function> Functions { get; private set; }
    public DbSet<Graph> Graphs { get; private set; }
    public DbSet<Node> Nodes { get; private set; }
    public DbSet<NodeIO> NodeIOs { get; private set; }
    public DbSet<NodeRefIO> NodeRefIOs { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Algorithm>().ToTableSnakeCase(SCHEMA);
        modelBuilder.Entity<Algorithm>().Property(x => x.Name).HasMaxLength(128);
        modelBuilder.Entity<Algorithm>().HasIndex(x => x.Name).IsUnique();
        modelBuilder.Entity<Algorithm>().HasKey(x => x.Id);
        modelBuilder.Entity<Algorithm>()
            .HasOne(x => x.Class)
            .WithMany(x => x.Algorithms)
            .HasForeignKey(x => x.ClassId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Algorithm>()
            .HasOne(x => x.AlgorithmType)
            .WithMany(x => x.Algorithms)
            .HasForeignKey(x => x.AlgorithmTypeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AttributeFor>().ToTableSnakeCase(SCHEMA);
        modelBuilder.Entity<AttributeFor>().Property(x => x.Name).HasMaxLength(128);
        modelBuilder.Entity<AttributeFor>().HasIndex(x => x.Name).IsUnique();
        modelBuilder.Entity<AttributeFor>().HasKey(x => x.Id);


        modelBuilder.Entity<AlgorithmType>().ToTableSnakeCase(SCHEMA);
        modelBuilder.Entity<AlgorithmType>().HasKey(x => x.Id);

        modelBuilder.Entity<ClassAlgorithm>().ToTableSnakeCase(SCHEMA);
        modelBuilder.Entity<ClassAlgorithm>().Property(x => x.Id);
        modelBuilder.Entity<ClassAlgorithm>().Property(x => x.Description).HasMaxLength(128);
        modelBuilder.Entity<ClassAlgorithm>().Property(x => x.Name).HasMaxLength(128);
        modelBuilder.Entity<ClassAlgorithm>().HasIndex(x => x.Name).IsUnique();
        modelBuilder.Entity<ClassAlgorithm>().HasKey(x => x.Id);


        modelBuilder.Entity<AlgorithmAttribute>().ToTableSnakeCase(SCHEMA);
        modelBuilder.Entity<AlgorithmAttribute>().HasIndex(x => new { x.AlgorithmId, x.AttributeId }).IsUnique();
        modelBuilder.Entity<AlgorithmAttribute>().HasKey(x => x.Id);
        modelBuilder.Entity<AlgorithmAttribute>()
            .HasOne(x => x.Attribute)
            .WithMany(x => x.AlgorithmAttributes)
            .HasForeignKey(x => x.AttributeId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<AlgorithmAttribute>()
            .HasOne(x => x.Algorithm)
            .WithMany(x => x.AlgorithmAttributes)
            .HasForeignKey(x => x.AlgorithmId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AlgorithmIO>().ToTableSnakeCase(SCHEMA);
        modelBuilder.Entity<AlgorithmIO>().Property(x => x.Props).HasDefaultValueJSON();
        modelBuilder.Entity<AlgorithmIO>().Property(x => x.IsNecesse).HasDefaultValue(true);
        modelBuilder.Entity<AlgorithmIO>().HasKey(x => x.Id);
        modelBuilder.Entity<AlgorithmIO>()
            .HasOne(x => x.Function)
            .WithMany(x => x.AlgorithmIOs)
            .HasForeignKey(x => x.FunctionId);

        modelBuilder.Entity<AlgorithmIOAttribute>().ToTableSnakeCase(SCHEMA);
        modelBuilder.Entity<AlgorithmIOAttribute>().HasKey(x => x.Id);
        modelBuilder.Entity<AlgorithmIOAttribute>()
            .HasOne(x => x.Attribute)
            .WithMany(x => x.AlgorithmIOAttributes)
            .HasForeignKey(x => x.AttributeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AlgorithmIOAttribute>()
            .HasOne(x => x.AlgorithmIO)
            .WithMany(x => x.AlgorithmIOAttributes)
            .HasForeignKey(x => x.AlgorithmIOId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Models.Attribute>().ToTableSnakeCase(SCHEMA);
        modelBuilder.Entity<Models.Attribute>().Property(x => x.Name).HasMaxLength(128);
        modelBuilder.Entity<Models.Attribute>().Property(x => x.Description).HasMaxLength(256);
        modelBuilder.Entity<Models.Attribute>().HasKey(x => x.Id);
        modelBuilder.Entity<Models.Attribute>()
            .HasOne(x => x.AttributeType)
            .WithMany(x => x.Attributes)
            .HasForeignKey(x => x.AttributeTypeId);
        modelBuilder.Entity<Models.Attribute>()
            .HasOne(x => x.AttributeFor)
            .WithMany(x => x.Attributes)
            .HasForeignKey(x => x.AttributeForId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AttributeType>().ToTableSnakeCase(SCHEMA);
        modelBuilder.Entity<AttributeType>().Property(x => x.Name).HasMaxLength(128);
        modelBuilder.Entity<AttributeType>().Property(x => x.Description).HasMaxLength(256);
        modelBuilder.Entity<AttributeType>().HasKey(x => x.Id);

        modelBuilder.Entity<Function>().ToTableSnakeCase(SCHEMA);
        modelBuilder.Entity<Function>().Property(x => x.Name).HasMaxLength(128);
        modelBuilder.Entity<Function>().HasKey(x => x.Id);

        modelBuilder.Entity<Graph>().ToTableSnakeCase(SCHEMA);
        modelBuilder.Entity<Graph>().Property(x => x.Name).HasMaxLength(128);
        modelBuilder.Entity<Graph>().HasKey(x => x.Id);

        modelBuilder.Entity<Node>().ToTableSnakeCase(SCHEMA);
        modelBuilder.Entity<Node>().Property(x => x.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Node>().Property(x => x.Name).HasMaxLength(128);
        modelBuilder.Entity<Node>().HasKey(x => x.Id);
        modelBuilder.Entity<Node>()
            .HasOne(x => x.Graph)
            .WithMany(x => x.Nodes)
            .HasForeignKey(x => x.GraphId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Node>()
            .HasOne(x => x.Algorithm)
            .WithMany(x => x.Nodes)
            .HasForeignKey(x => x.AlgorithmId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<NodeIO>().ToTableSnakeCase(SCHEMA);
        modelBuilder.Entity<NodeIO>().HasKey(x => x.Id);
        modelBuilder.Entity<NodeIO>()
            .HasOne(x => x.Graph)
            .WithMany(x => x.NodeIOs)
            .HasForeignKey(x => x.GraphId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<NodeIO>()
            .HasOne(x => x.NodeIn)
            .WithMany(x => x.NodeIns)
            .HasForeignKey(x => x.NodeInId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<NodeIO>()
            .HasOne(x => x.NodeOut)
            .WithMany(x => x.NodeOuts)
            .HasForeignKey(x => x.NodeOutId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<NodeRefIO>().ToTableSnakeCase(SCHEMA);
        modelBuilder.Entity<NodeRefIO>().Property(x => x.DataRef).HasDefaultValueJSON().HasMaxLength(1028);
        modelBuilder.Entity<NodeRefIO>().HasKey(x => x.Id);
        modelBuilder.Entity<NodeRefIO>()
            .HasOne(x => x.Node)
            .WithMany(x => x.NodeRefIOs)
            .HasForeignKey(x => x.NodeId)
            .OnDelete(DeleteBehavior.Cascade);

        ///////////////////////////////////// сэмплеры данных

        var attributeFor1 = new AttributeFor { Name = "AlgorithmIO" };

        var attributeFor2 = new AttributeFor { Name = "Algorithm" };

        modelBuilder.Entity<AttributeFor>().HasData(attributeFor1, attributeFor2);

        var funcCalc = new Function { Name = "calc" };

        modelBuilder.Entity<Function>().HasData(funcCalc);

        var attributeType = new AttributeType { Name = "ModeType", Description = "Режим работы" };

        var attributeType2 = new AttributeType { Name = "ParameterType", Description = "Тип параметра" };

        var attributeType3 = new AttributeType { Name = "CharacterType", Description = "Типовая характеристика" };

        modelBuilder.Entity<AttributeType>().HasData(attributeType, attributeType2);

        var attribute1 = new Models.Attribute
        {
            Name = "ДТЛ",
            Description = "Дизелька летняя",
            AttributeType = attributeType
        };

        var attribute2 = new Models.Attribute
        {
            Name = "ДТЗ",
            Description = "Дизелька зимняя",
            AttributeType = attributeType
        };

        var attribute3 = new Models.Attribute
        {
            Name = "ТП",
            Description = "Технологический параметр",
            AttributeType = attributeType2,
            AttributeFor = attributeFor1
        };

        var attribute4 = new Models.Attribute
        {
            Name = "СММСГ",
            Description = "Средняя молекулярная масса смеси газов",
            AttributeType = attributeType3,
            AttributeFor = attributeFor1
        };

        var attribute5 = new Models.Attribute
        {
            Name = "ВМ",
            Description = "Выход по модели",
            AttributeType = attributeType2,
            AttributeFor = attributeFor1
        };

        modelBuilder.Entity<Models.Attribute>().HasData(attribute1, attribute2, attribute3, attribute4);

        var column = new AlgorithmType { Name = "Column" };

        modelBuilder.Entity<AlgorithmType>().HasData(
            new AlgorithmType { Name = "Function" },
            column,
            new AlgorithmType { Name = "Plant" },
            new AlgorithmType { Name = "Manufacture" });

        var classId = new ClassAlgorithm { Name = "MaterialStream", Description = "Материальный поток" };

        var classId2 = new ClassAlgorithm { Name = "Mixer", Description = "Смеситель" };

        modelBuilder.Entity<ClassAlgorithm>().HasData(classId);

        var algo = new Algorithm { Name = "Колонна", AlgorithmType = column, Class = classId };

        modelBuilder.Entity<Algorithm>().HasData(algo);

        var algoAttr = new AlgorithmAttribute { Attribute = attribute1, Algorithm = algo };

        modelBuilder.Entity<AlgorithmAttribute>().HasData(algoAttr, algoAttr);

        var algorithmIO = new AlgorithmIO { Function = funcCalc, Algorithm = algo, Index = 0, IsNecesse = true, Props = "{}", TypeIO = 1 };

        var algorithmIO2 = new AlgorithmIO { Function = funcCalc, Algorithm = algo, Index = 1, IsNecesse = true, Props = "{}", TypeIO = 1 };

        var algorithmIO3 = new AlgorithmIO { Function = funcCalc, Algorithm = algo, Index = 2, IsNecesse = false, Props = "{}", TypeIO = 0 };

        modelBuilder.Entity<AlgorithmIO>().HasData(algorithmIO, algorithmIO2, algorithmIO3);

        var algorithmIOAttr1 = new AlgorithmIOAttribute { Attribute = attribute3, AlgorithmIO = algorithmIO };

        var algorithmIOAttr2 = new AlgorithmIOAttribute { Attribute = attribute4, AlgorithmIO = algorithmIO2 };

        var algorithmIOAttr3 = new AlgorithmIOAttribute { Attribute = attribute5, AlgorithmIO = algorithmIO3 };

        modelBuilder.Entity<AlgorithmIOAttribute>().HasData(algorithmIOAttr1, algorithmIOAttr2, algorithmIOAttr3);

        //// для workflow (графы и тд)

        //var graph = new Graph { Name = "Цепочка АВТ-6" };

        //modelBuilder.Entity<Graph>().HasData(graph);

        //var node = new Node { Graph = graph, Name = "Материальный поток #1", State = 0, Algorithm = algo };

        //modelBuilder.Entity<Node>().HasData(node);

        //var nodeIn = new NodeIO { Graph = graph},
            

        base.OnModelCreating(modelBuilder);

    }


    public DbSet<DynamicGraphStructure.WebApi.Database.Models.ClassAlgorithm> ClassAlgorithm { get; set; }


    public DbSet<DynamicGraphStructure.WebApi.Database.Models.AlgorithmType> AlgorithmType { get; set; }


}
