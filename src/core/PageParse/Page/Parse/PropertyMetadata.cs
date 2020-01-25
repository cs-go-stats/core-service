using System.Reflection;
using CSGOStats.Infrastructure.Core.Validation;

namespace CSGOStats.Infrastructure.Core.PageParse.Page.Parse
{
    public class PropertyMetadata
    {
        public object Subtree { get; }

        public PropertyInfo Property { get; }

        public ContainerMetadata Container { get; }

        public bool IsCollection { get; }

        public string MappingCode { get; }

        public PropertyMetadata(object subtree, PropertyInfo property, ContainerMetadata container, bool isCollection, string mappingCode)
        {
            Subtree = subtree.NotNull(nameof(subtree));
            Property = property.NotNull(nameof(property));
            Container = container;
            IsCollection = isCollection;
            MappingCode = mappingCode;
        }
    }
}