using System.Globalization;
using System.Text;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace DynamicGraphStructure.WebApi.Database;

public static class EFEntityExtensions
{
    public static EntityTypeBuilder<TEntity> ToTableSnakeCase<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder, string? schema = null)
            where TEntity : class
    {
        var name = typeof(TEntity).Name.ToSnakeCase();

        if (name.LastOrDefault() != 's')
            name += 's';

        entityTypeBuilder.ToTable(name, schema);

        return entityTypeBuilder;
    }

    public static PropertyBuilder<TProperty> PropertySnakeCase<TProperty, TEntity>(this EntityTypeBuilder<TEntity> ebuilder, Expression<Func<TEntity, TProperty>> propertyExpression)
        where TEntity : class
    {
        var propertyBuilder = ebuilder.Property(propertyExpression);
        var prop = propertyBuilder.Metadata.FieldInfo?.Name.ToSnakeCase();
        return propertyBuilder.HasColumnName(prop);
    }

    public static PropertyBuilder<string> HasDefaultValueJSON(this PropertyBuilder<string> propertyBuilder)
    {
        return propertyBuilder.HasDefaultValue("{}");
    }

    private static string ToSnakeCase(this string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        var builder = new StringBuilder(text.Length + Math.Min(2, text.Length / 5));
        var previousCategory = default(UnicodeCategory?);

        for (var currentIndex = 0; currentIndex < text.Length; currentIndex++)
        {
            var currentChar = text[currentIndex];
            if (currentChar == '_')
            {
                builder.Append('_');
                previousCategory = null;
                continue;
            }

            var currentCategory = char.GetUnicodeCategory(currentChar);
            switch (currentCategory)
            {
                case UnicodeCategory.UppercaseLetter:
                case UnicodeCategory.TitlecaseLetter:
                    if (previousCategory == UnicodeCategory.SpaceSeparator ||
                        previousCategory == UnicodeCategory.LowercaseLetter ||
                        previousCategory != UnicodeCategory.DecimalDigitNumber &&
                        previousCategory != null &&
                        currentIndex > 0 &&
                        currentIndex + 1 < text.Length &&
                        char.IsLower(text[currentIndex + 1]))
                    {
                        builder.Append('_');
                    }

                    currentChar = char.ToLower(currentChar, CultureInfo.InvariantCulture);
                    break;

                case UnicodeCategory.LowercaseLetter:
                case UnicodeCategory.DecimalDigitNumber:
                    if (previousCategory == UnicodeCategory.SpaceSeparator)
                    {
                        builder.Append('_');
                    }
                    break;

                default:
                    if (previousCategory != null)
                    {
                        previousCategory = UnicodeCategory.SpaceSeparator;
                    }
                    continue;
            }

            builder.Append(currentChar);
            previousCategory = currentCategory;
        }

        return builder.ToString();
    }
}
