using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Net;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Entities.Movies;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Configuration;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Logging;
using MediaBrowser.Model.Providers;

namespace Douban
{
    class DoubanImageProvider : IRemoteImageProvider, IHasOrder
    {
        private readonly IHttpClient _httpClient;
        private readonly ILogger _logger;
        private readonly IApplicationPaths _appPaths;

        public DoubanImageProvider(IHttpClient httpClient, ILogger logger, IApplicationPaths appPaths)
        {
            _httpClient = httpClient;
            _logger = logger;
            _appPaths = appPaths;
        }
        public string Name => "Douban";

        public int Order => 3;

        public Task<HttpResponseInfo> GetImageResponse(string url, CancellationToken cancellationToken)
        {
            var options = new HttpRequestOptions
            {
                Url = url,
                CancellationToken = cancellationToken
            };
            return _httpClient.GetResponse(options);
        }

        public Task<IEnumerable<RemoteImageInfo>> GetImages(BaseItem item, LibraryOptions libraryOptions, CancellationToken cancellationToken)
        {
            var list = new List<RemoteImageInfo>();
            var cachePath = Path.Combine(_appPaths.CachePath, "douban", item.GetProviderId(Name), "image.txt");
            if (File.Exists(cachePath))
            {
                var image = new RemoteImageInfo
                {
                    ProviderName = Name,
                    Url = File.ReadAllText(cachePath),
                    Type = ImageType.Primary
                };
                list.Add(image);
            }
            return Task.FromResult<IEnumerable<RemoteImageInfo>>(list);
        }

        public IEnumerable<ImageType> GetSupportedImages(BaseItem item) => new[] { ImageType.Primary };

        public bool Supports(BaseItem item) => item is Movie;
    }
}
