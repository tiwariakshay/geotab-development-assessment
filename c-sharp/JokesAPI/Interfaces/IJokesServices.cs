using System.Collections.Generic;
using System.Threading.Tasks;

namespace JokesAPI.Services
{
    public interface IJokesServices
    {
        Task<string> GetCategories();
        List<string> GetJokes(string firstname, string lastname, string category, int number);
    }
}