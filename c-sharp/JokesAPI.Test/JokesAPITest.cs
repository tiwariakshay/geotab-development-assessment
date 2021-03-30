using Xunit;
using AutoFixture;
using System.Net.Http;
using System;
using Newtonsoft.Json;

namespace JokesAPI.Test
{
    public class JokesAPITest
    {
        [Fact]
        public void GetJokes()
        {
            //arrange
            var fixture = new Fixture();
            var count = fixture.CreateInt(1, 9);

            //act
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44392");
            string url = "jokes";
            BuildURL(ref url, count, "number");
            var result = client.GetStringAsync(url).Result;

            //asset
            Assert.Equal(count, JsonConvert.DeserializeObject<string[]>(result).Length);
        }

        private string BuildURL(ref string url, object value, string parameter)
        {
            if (url.Contains('?'))
                url += "&";
            else url += "?";
            url += $"{parameter}=";
            url += value;
            return url;
        }

    }
}
