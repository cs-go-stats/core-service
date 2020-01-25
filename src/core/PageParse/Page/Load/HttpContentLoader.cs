using System.Net.Http;
using System.Threading.Tasks;
using CSGOStats.Infrastructure.Core.Validation;

namespace CSGOStats.Infrastructure.Core.PageParse.Page.Load
{
    public class HttpContentLoader : IContentLoader
    {
        private readonly string _url;

        public HttpContentLoader(string url)
        {
            _url = url.NotNull(nameof(url));
        }

        public async Task<string> LoadAsync()
        {
            var response = await new HttpClient().GetAsync(_url).ContinueWith(x =>
                    x.Result.EnsureSuccessStatusCode(), TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
            return await response.Content.ReadAsStringAsync();
        }
    }
}