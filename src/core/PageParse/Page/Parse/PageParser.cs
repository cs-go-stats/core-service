using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Extensions;
using CSGOStats.Infrastructure.Core.PageParse.Mapping;
using CSGOStats.Infrastructure.Core.PageParse.Page.Load;
using CSGOStats.Infrastructure.Core.PageParse.Page.Structure.Containers;
using CSGOStats.Infrastructure.Core.PageParse.Page.Structure.Markers;
using CSGOStats.Infrastructure.Core.Validation;
using HtmlAgilityPack;

namespace CSGOStats.Infrastructure.Core.PageParse.Page.Parse
{
    public class PageParser<TModel> : IPageParser<TModel>
        where TModel : class
    {
        private readonly IValueMapperFactory _valueMapperFactory;

        public PageParser(IValueMapperFactory valueMapperFactory)
        {
            _valueMapperFactory = valueMapperFactory.NotNull(nameof(valueMapperFactory));
        }

        public async Task<TModel> ParseAsync(IContentLoader contentLoader)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(await contentLoader.LoadAsync());

            return ParseInternal(htmlDocument);
        }

        private TModel ParseInternal(HtmlDocument document)
        {
            var resultInstance = Activator.CreateInstance<TModel>();

            var rootContainer = typeof(TModel).GetCustomAttribute<RootContainerAttribute>(false);
            if (rootContainer == null)
            {
                throw new ModelDoesNotContainRootContainerException(typeof(TModel));
            }

            var rootElement = document.DocumentNode;
            MapSubtree(resultInstance, rootElement.SelectSingleNode(rootContainer.Path));

            return resultInstance;
        }

        private void MapSubtree(object subtree, HtmlNode htmlRoot)
        {
            foreach (var property in subtree.GetProperties())
            {
                ProcessProperty(property, htmlRoot);
            }
        }

        private void MapSubtreeCollection(WrappedCollection collection, HtmlNode htmlRoot)
        {
            var (subtree, properties) = collection.GetProperties();
            collection.Add(subtree);

            foreach (var property in properties)
            {
                ProcessProperty(property, htmlRoot);
            }
        }

        private void ProcessProperty(PropertyMetadata property, HtmlNode htmlRoot)
        {
            var actionType = property.GetActionType();
            ((int) actionType).AnythingBut((int) ActionType.Unknown, nameof(ActionType));
            switch (actionType)
            {
                case ActionType.BindMarkup:
                    EnsureSubtreeCreated(property);
                    BindMarkup(property, htmlRoot);
                    break;
                case ActionType.ExtractValue:
                    AssignValue(property, htmlRoot);
                    break;
                case ActionType.BindMarkupAndExtractValue:
                    EnsureSubtreeCreated(property);
                    foreach (var subtreeRoot in SelectNodes(property, htmlRoot))
                    {
                        AssignValue(property, subtreeRoot);
                    }

                    break;
            }
        }

        private static void EnsureSubtreeCreated(PropertyMetadata property)
        {
            if (property.IsCollection)
            {
                GetSubtree(property).NotNull($"{property.Property.DeclaringType.Name}.{property.Property.Name}");
                return;
            }

            if (property.Property.PropertyType.IsLeaf())
            {
                property.Property.SetValue(property.Subtree, CreateInstance(property.Property, property.Subtree));
            }
        }

        private static object GetSubtree(PropertyMetadata property)
        {
            if (property.Property.IsCollection())
            {
                return property.Property.GetValue(property.Subtree);
            }

            if (property.Property.PropertyType.IsLeaf())
            {
                return property.Property.GetValue(property.Subtree);
            }

            if (property.Subtree.GetType().IsLeaf() || property.Subtree.GetType().IsRoot())
            {
                return property.Subtree;
            }

            throw new InvalidOperationException("Subtree cannot be determined in current context.");
        }

        private void BindMarkup(PropertyMetadata property, HtmlNode htmlRoot)
        {
            foreach (var subtreeRoot in SelectNodes(property, htmlRoot))
            {
                DoIfWrappedCollection(
                    GetSubtree(property),
                    collection => MapSubtreeCollection(collection, subtreeRoot),
                    @object => MapSubtree(@object, subtreeRoot));
            }
        }

        private void AssignValue(PropertyMetadata property, HtmlNode htmlRoot)
        {
            var value = ExtractValueFromMarkup(property, htmlRoot);
            var subtree = GetSubtree(property);
            DoIfWrappedCollection(
                subtree, 
                collection => collection.Add(value), 
                @object => property.Property.SetValue(@object, value));
        }

        private object ExtractValueFromMarkup(PropertyMetadata property, HtmlNode htmlRoot) =>
            htmlRoot == null 
                ? null 
                : _valueMapperFactory.Create(property.MappingCode).Map(htmlRoot);

        private static IEnumerable<HtmlNode> SelectNodes(PropertyMetadata property, HtmlNode htmlRoot) => property.IsCollection
            ? SelectNodeCollection(property, htmlRoot)
            : SelectSingleNode(property, htmlRoot);

        private static IEnumerable<HtmlNode> SelectNodeCollection(PropertyMetadata property, HtmlNode htmlRoot) =>
            GetNodesByPath(htmlRoot, property.Container.Path)
                .ToArrayFast()
                .CheckRequirement(property, htmlRoot);

        private static IEnumerable<HtmlNode> SelectSingleNode(PropertyMetadata property, HtmlNode htmlRoot) =>
            GetNodesByPath(htmlRoot, property.Container.Path)
                .SingleOrThrow(property, htmlRoot)
                .CheckRequirement(property, htmlRoot);

        private static IEnumerable<HtmlNode> GetNodesByPath(HtmlNode htmlRoot, string path) =>
            htmlRoot.SelectNodes(path);

        private static object CreateInstance(PropertyInfo property, object parent)
        {
#if DEBUG
            try
            {
#endif
                return property.PropertyType.CreateInstance();
#if DEBUG
            }
            catch (MissingMethodException)
            {
                System.Diagnostics.Debug.WriteLine($"'{property.Name}' cannot be created for object of type '{parent.GetType().Name}'.");
                throw;
            }
#endif
        }

        private static void DoIfWrappedCollection(object instance, Action<WrappedCollection> isCollectionAction, Action<object> isObjectAction)
        {
            if (instance is WrappedCollection collection)
            {
                isCollectionAction(collection);
            }
            else
            {
                isObjectAction(instance);
            }
        }
    }
}