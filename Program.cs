using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace ElectricCarSearcher
{
    public class Program
    {
        public static string key = "YOUR EDMUNDS KEY";

        public static void Main(string[] args)
        {
            // Get all vehicles for 2017 
            var client = new HttpClient();
            client.BaseAddress = new Uri(string.Format("https://api.edmunds.com/api/vehicle/v2/makes?state=new&year=2017&view=basic&fmt=json&api_key={0}", key));
            var response = client.GetAsync("").Result;

            var responseAsString = response.Content.ReadAsStringAsync().Result;
        
            // get all nice make names - note that the nice make name is always before this string: ","models" 
            var identifier = "\",\"models\"";
            List<string> makes = new List<string>();
            for (int index = 0;; index += identifier.Length) {
                index = responseAsString.IndexOf(identifier, index);
                if(index == -1)
                    break;
              
                var searchString = responseAsString.Substring(0,index);
    
                var make = searchString.Substring(searchString.LastIndexOf("\"")+1);
                makes.Add(make);
            }

            
            // get all styles for all makes
            var client2 = new HttpClient();
            client2.BaseAddress = new Uri(string.Format("https://api.edmunds.com/api/vehicle/v2/chevrolet/models?state=new&year=2017&view=basic&fmt=json&api_key={0}", key));
            var response2 = client2.GetAsync("").Result;

            var response2AsString  = response2.Content.ReadAsStringAsync().Result;

            var indexOfId = response2AsString.IndexOf("id\":");
            while(indexOfId > 0){
                var styleID = response2AsString.Substring(indexOfId+4,response2AsString.IndexOf(',', indexOfId) - indexOfId-4);

                try
                {
                    InspectEngine(styleID);
                    SaveVehicle(styleID);
                }
                catch{}
                indexOfId = response2AsString.IndexOf("id\":",indexOfId+4);
            }


        }

        private static void InspectEngine(object styleID)
        {
           
            var client = new HttpClient();
            client.BaseAddress = new Uri(string.Format("https://api.edmunds.com/api/vehicle/v2/styles/{1}/engines?availability=standard&fmt=json&api_key={0}", key, styleID));
            var response = client.GetAsync("").Result;

            var responseAsString = response.Content.ReadAsStringAsync().Result;

            if(responseAsString.Contains("\"fuelType\":\"electric\"") == false)
                throw new ArgumentException();
        }

        private static void SaveVehicle(object styleID){
            var client = new HttpClient();
            client.BaseAddress = new Uri(string.Format("https://api.edmunds.com/api/vehicle/v2/styles/{1}?view=full&fmt=json&api_key={0}", key, styleID));
            var response = client.GetAsync("").Result;

            Console.WriteLine(response.Content.ReadAsStringAsync().Result.Substring(0,200));            
        }
    }
}
