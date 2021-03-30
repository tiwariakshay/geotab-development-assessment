using System;
using System.Net.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    public class JsonFeed : IJsonFeed
    {
        private JokesOptions _jokesOptions;

        public JsonFeed(IOptions<JokesOptions> jokesOptions)
        {
            _jokesOptions = jokesOptions.Value;
        }

        /// <summary>
        /// Get random names
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public string[] GetRandomJokes(string firstname, string lastname, string category, int number)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_jokesOptions.APIBaseUrl);
                string url = "jokes";

                if (firstname != null)
                {
                    BuildURL(ref url, firstname, "firstname");
                }
                if (lastname != null)
                {
                    BuildURL(ref url, lastname, "lastname");
                }
                if (category != null)
                {
                    BuildURL(ref url, category, "category");
                }
                if (number > 0)
                {
                    BuildURL(ref url, number, "number");
                }

                var result = client.GetStringAsync(url).Result;
                if (result.Equals("[]"))
                {
                    result = AppConstants.ExceptionNoJokeFound;
                }
                return JsonConvert.DeserializeObject<string[]>(result);
            }
            catch (Exception ex)
            {
                return JsonConvert.DeserializeObject<string[]>(AppConstants.ExceptionGeneral);
            }
                                              
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public dynamic GetNames()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_jokesOptions.APIBaseUrl);
                var result = client.GetStringAsync("names").Result;
                return JsonConvert.DeserializeObject<dynamic>(result);
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// Get Categories
        /// </summary>
        /// <returns></returns>
        public string[] GetCategories()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_jokesOptions.APIBaseUrl);
                var result = client.GetStringAsync("jokes/category").Result;
                return JsonConvert.DeserializeObject<string[]>(result);
            }
            catch(Exception ex) 
            {
                return JsonConvert.DeserializeObject<string[]>(AppConstants.ExceptionGeneral);
            }
           
        }

        /// <summary>
        /// BuildURL
        /// </summary>
        /// <param name="url"></param>
        /// <param name="value"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
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
