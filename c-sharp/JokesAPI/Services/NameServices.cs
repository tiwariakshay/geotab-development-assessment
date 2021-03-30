using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace JokesAPI.Services
{
    public class NameServices : INameServices
    {
        private readonly HttpClient _client;
        private readonly ILogger<NameServices> _logger;
        private JokesAPIOptions _jokesOptions;


        public NameServices(ILogger<NameServices> logger, HttpClient client, IOptions<JokesAPIOptions> jokesOptions)
        {
            _logger = logger;
            _client = client;
            _jokesOptions = jokesOptions.Value;
        }
        public async Task<object> GetNames()
        {
            var jokesUrl = _jokesOptions.NamesAPIBaseUrl;
            _client.BaseAddress = new Uri(jokesUrl);
            return await _client.GetStringAsync("");
        }
    }
}
