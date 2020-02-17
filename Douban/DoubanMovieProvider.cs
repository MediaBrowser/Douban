using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Douban.Model;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Net;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Entities.Movies;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Logging;
using MediaBrowser.Model.Providers;
using MediaBrowser.Model.Serialization;
using MediaBrowser.Model.IO;

namespace Douban
{
    class DoubanMovieProvider : IRemoteMetadataProvider<Movie, MovieInfo>, IHasOrder
    {
        private readonly IHttpClient _httpClient;
        private readonly ILogger _logger;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IApplicationPaths _appPaths;
        private readonly string baseUrl = "https://api.douban.com/v2";
        private readonly string apiKey = "0dad551ec0f84ed02907ff5c42e8ec70";
        private readonly IFileSystem _fileSystem;

        public DoubanMovieProvider(ILogger logger, IHttpClient httpClient, IJsonSerializer jsonSerializer, IApplicationPaths appPaths, IFileSystem fileSystem)
        {
            _logger = logger;
            _httpClient = httpClient;
            _jsonSerializer = jsonSerializer;
            _appPaths = appPaths;
            _fileSystem = fileSystem;
        }
        public int Order => 3;

        public string Name => "Douban";

        public Task<HttpResponseInfo> GetImageResponse(string url, CancellationToken cancellationToken)
        {
            var options = new HttpRequestOptions
            {
                Url = url,
                CancellationToken = cancellationToken
            };
            return _httpClient.GetResponse(options);
        }

        public async Task<MetadataResult<Movie>> GetMetadata(MovieInfo info, CancellationToken cancellationToken)
        {
            var metadataResult = new MetadataResult<Movie>();
            if (!info.ProviderIds.TryGetValue(Name, out string id))
            {
                var res = await GetSearchResults(info, cancellationToken).ConfigureAwait(false);
                if (res.Count() == 0 || !res.FirstOrDefault().ProviderIds.TryGetValue(Name, out id))
                {
                    return metadataResult;
                }
            }

            var options = new HttpRequestOptions()
            {
                Url = $"{baseUrl}/movie/subject/{id}?apikey={apiKey}",
                CancellationToken = cancellationToken
            };
            using (var res = await _httpClient.GetResponse(options).ConfigureAwait(false))
            {
                var result = await _jsonSerializer.DeserializeFromStreamAsync<MovieMetadataResult>(res.Content).ConfigureAwait(false);

                metadataResult.HasMetadata = true;
                metadataResult.QueriedById = true;

                metadataResult.Item = new Movie
                {
                    Name = result.title,
                    Overview = result.summary,
                    ProductionYear = int.Parse(result.year),
                    OriginalTitle = result.original_title,
                    Genres = result.genres
                };
                if (result.rating != null)
                {
                    metadataResult.Item.CommunityRating = result.rating.average;
                }
                metadataResult.Item.ProviderIds.Add(Name, result.id);

                foreach (var cast in result.casts)
                {
                    var pi = new PersonInfo
                    {
                        Name = cast.name
                    };
                    if (cast.avatars != null)
                    {
                        pi.ImageUrl = cast.avatars.large;
                    }
                    if (!string.IsNullOrEmpty(cast.id))
                    {
                        pi.ProviderIds.Add(Name, cast.id);
                    }
                    metadataResult.AddPerson(pi);
                }

                try
                {
                    var cachePath = Path.Combine(_appPaths.CachePath, "douban", result.id, "image.txt");
                    _fileSystem.CreateDirectory(_fileSystem.GetDirectoryName(cachePath));
                    _fileSystem.WriteAllText(cachePath, result.images.large);
                }
                catch
                {
                };
            }
            return metadataResult;
        }

        public async Task<IEnumerable<RemoteSearchResult>> GetSearchResults(MovieInfo searchInfo, CancellationToken cancellationToken)
        {
            var list = new List<RemoteSearchResult>();
            var options = new HttpRequestOptions()
            {
                Url = $"{baseUrl}/movie/search?q={searchInfo.Name}&apikey={apiKey}",
                CancellationToken = cancellationToken
            };
            using (var res = await _httpClient.GetResponse(options).ConfigureAwait(false))
            {
                var searchResult = await _jsonSerializer.DeserializeFromStreamAsync<MovieSearchResult>(res.Content).ConfigureAwait(false);
                foreach (var subject in searchResult.subjects)
                {
                    var result = new RemoteSearchResult
                    {
                        Name = subject.title,
                        ProductionYear = int.Parse(subject.year),
                        ImageUrl = subject.images.large,
                        SearchProviderName = Name
                    };
                    result.ProviderIds.Add(Name, subject.id);
                    list.Add(result);
                }
                return list;
            }
        }
    }
}
