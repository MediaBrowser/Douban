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
using MediaBrowser.Model.IO;

namespace Douban
{
    class DoubanImageProvider : IRemoteImageProvider, IHasOrder
    {
        private readonly IHttpClient _httpClient;
        private readonly ILogger _logger;
        private readonly IApplicationPaths _appPaths;
        private readonly IFileSystem _fileSystem;

        public DoubanImageProvider(IHttpClient httpClient, ILogger logger, IApplicationPaths appPaths, IFileSystem fileSystem)
        {
            _httpClient = httpClient;
            _logger = logger;
            _appPaths = appPaths;
            _fileSystem = fileSystem;
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

        public async Task<IEnumerable<RemoteImageInfo>> GetImages(BaseItem item, LibraryOptions libraryOptions, CancellationToken cancellationToken)
        {
            var list = new List<RemoteImageInfo>();
            if (!string.IsNullOrEmpty(item.GetProviderId(Name)))
            {
                var cachePath = Path.Combine(_appPaths.CachePath, "douban", item.GetProviderId(Name), "image.txt");

                try
                {
                    var image = new RemoteImageInfo
                    {
                        ProviderName = Name,
                        Url = await _fileSystem.ReadAllTextAsync(cachePath, cancellationToken).ConfigureAwait(false),
                        Type = ImageType.Primary
                    };
                    list.Add(image);
                }
                catch (FileNotFoundException)
                {

                }
            }

            return list;
        }

        public IEnumerable<ImageType> GetSupportedImages(BaseItem item) => new[] { ImageType.Primary };

        public bool Supports(BaseItem item) => item is Movie;
    }
}
