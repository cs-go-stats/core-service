using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.PageParse.Page.Load;

namespace CSGOStats.Infrastructure.Core.PageParse.Page.Parse
{
    public interface IPageParser<T>
        where T : class
    {
        Task<T> ParseAsync(IContentLoader contentLoader);
    }
}