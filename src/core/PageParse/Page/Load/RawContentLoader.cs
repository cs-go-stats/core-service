using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Validation;

namespace CSGOStats.Infrastructure.Core.PageParse.Page.Load
{
    public class RawContentLoader : IContentLoader
    {
        private readonly string _content;

        public RawContentLoader(string content)
        {
            _content = content.NotNull(nameof(content));
        }

        public Task<string> LoadAsync() => Task.FromResult(_content);
    }
}