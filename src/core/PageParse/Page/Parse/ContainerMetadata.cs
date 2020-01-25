using CSGOStats.Infrastructure.Core.PageParse.Page.Structure.Containers;
using CSGOStats.Infrastructure.Core.Validation;

namespace CSGOStats.Infrastructure.Core.PageParse.Page.Parse
{
    public class ContainerMetadata
    {
        public string Path { get; }

        public bool IsRequired { get; }

        public ContainerMetadata(string path, bool isRequired)
        {
            Path = path.NotNull(nameof(path));
            IsRequired = isRequired;
        }

        public static ContainerMetadata CreateFrom(BasePropertyContainerAttribute attribute) => attribute == null
            ? null
            : new ContainerMetadata(attribute.Path, attribute.IsRequired);
    }
}