using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokesAPI
{
    public class JokesAPIOptions
    {
        public const string ApiUrl = "APIBaseUrl";
        public string ChuckAPIBaseUrl { get; set; }
        public string NamesAPIBaseUrl { get; set; }
    }
}
