using System.Threading.Tasks;

namespace CSGOStats.Infrastructure.Core.PageParse.Page.Load
{
    public interface IContentLoader
    {
        Task<string> LoadAsync();
    }
}