using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TzMazeScraper.Clients.Dtos;
using TzMazeScraper.Settings;

namespace TzMazeScraper.Clients
{
    public class TzMazeClient
    {
        public const int FirstPageNumber = 0;

        private readonly HttpClient _httpClient;

        public TzMazeClient(IOptions<ScraperSettings> scraperOptions)
        {
            _httpClient = new HttpClient(new RetryHandler(new HttpClientHandler()))
            {
                BaseAddress = new Uri(scraperOptions.Value.UrlRoot)
            };
        }

        public async Task<IList<ShowDto>> ListShows(int pageNumber, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"shows?page={pageNumber}", cancellationToken);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<ShowDto>>(cancellationToken);
        }

        public async Task<IList<CastDto>> ListCast(int showId, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"shows/{showId}/cast", cancellationToken);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<CastDto>>(cancellationToken);
        }

        private class RetryHandler : DelegatingHandler
        {
            private const int MaxRetries = 3;
            private const HttpStatusCode TooManyRequestsStatusCode = (HttpStatusCode)429;
            private static readonly TimeSpan RetryDelay = TimeSpan.FromSeconds(5);

            public RetryHandler(HttpMessageHandler innerHandler)
                : base(innerHandler)
            { }

            protected override async Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request,
                CancellationToken cancellationToken)
            {
                HttpResponseMessage response = null;
                for (int i = 0; i < MaxRetries; i++)
                {
                    response = await base.SendAsync(request, cancellationToken);
                    if (response.StatusCode != TooManyRequestsStatusCode)
                    {
                        return response;
                    }
                    await Task.Delay(RetryDelay, cancellationToken);
                }

                return response;
            }
        }
    }
}
