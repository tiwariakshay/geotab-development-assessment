using System.Threading.Tasks;

namespace JokesAPI.Services
{
    public interface INameServices
    {
        Task<object> GetNames();
    }
}