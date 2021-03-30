using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace JokesAPI.Services
{
    public class JokesServices : IJokesServices
    {
        private readonly HttpClient _client;
        private readonly ILogger<JokesServices> _logger;
        private JokesAPIOptions _jokesOptions;

        public JokesServices(ILogger<JokesServices> logger, HttpClient client, IOptions<JokesAPIOptions> jokesOptions)
        {
            _logger = logger;
            _client = client;
            _jokesOptions = jokesOptions.Value;
        }
        public List<string> GetJokes(string firstname, string lastname, string category, int number)
        {
            List<string> jokes = new List<string>();
            var jokesUrl = _jokesOptions.ChuckAPIBaseUrl;
            _client.BaseAddress = new Uri(jokesUrl);
            string url = "jokes/random";
            if (category != null)
            {
                if (url.Contains('?'))
                    url += "&";
                else url += "?";
                url += "category=";
                url += category;
            }
            Parallel.For(0, number, i => CallJokesApi(firstname, lastname, jokes, url));

            return jokes;
        }

        public async Task<string> GetCategories()
        {
            var jokesUrl = _jokesOptions.ChuckAPIBaseUrl;
            _client.BaseAddress = new Uri(jokesUrl);
            return await _client.GetStringAsync("jokes/categories");
        }

        /// <summary>
        /// Get Jokes
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="jokes"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        private List<string> CallJokesApi(string firstname, string lastname, List<string> jokes, string url)
        {
            string joke = _client.GetStringAsync(url).Result;

            if (firstname != null && lastname != null)
            {
                int index = joke.IndexOf("Chuck Norris");
                string firstPart = joke.Substring(0, index);
                string secondPart = joke.Substring(0 + index + "Chuck Norris".Length, joke.Length - (index + "Chuck Norris".Length));
                joke = firstPart + " " + firstname + " " + lastname + secondPart;
            }

            jokes.Add(JsonConvert.DeserializeObject<dynamic>(joke).value.Value);

            return jokes;
        }
    }
}
