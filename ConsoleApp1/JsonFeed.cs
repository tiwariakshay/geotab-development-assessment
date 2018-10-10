using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class JsonFeed
    {
        static string _url = "";
        static int _results = 42;

        public JsonFeed() { }
        public JsonFeed(string endpoint, int results)
        {
            _url = endpoint;
        }
        
		public static string[] GetRandomJokes(string firstname, string lastname, string category)
		{
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(_url);
			string url = "jokes/";
			url += _results.ToString();
			if (firstname != null)
			{
				if (url.Contains('?'))
					url += "&";
				else url += "?";
				url += "firstName=";
				url += firstname.ToString();
			}
			if (lastname != null)
			{
				if (url.Contains('?'))
					url += "&";
				else url += "?";
				url += "lastName=";
				url += lastname.ToString();
			}
			if (category != null)
			{
				if (url.Contains('?'))
					url += "&";
				else url += "?";
				url += "limitTo=[";
				url += category;
				url += "]";
			}

			return new string[] { Task.FromResult(client.GetStringAsync(url).Result).Result };
		}

        /// <summary>
        /// returns an object that contains name and surname
        /// </summary>
        /// <param name="client2"></param>
        /// <returns></returns>
		public static dynamic Getnames()
		{
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(_url);
			var result = client.GetStringAsync("").Result;
			return JsonConvert.DeserializeObject<dynamic>(result);
		}

		public static string[] GetCategories()
		{
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(_url);

			return new string[] { Task.FromResult(client.GetStringAsync("categories").Result).Result };
		}
    }
}
