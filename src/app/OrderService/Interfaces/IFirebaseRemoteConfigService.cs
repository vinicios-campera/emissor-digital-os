using System.Threading.Tasks;

namespace OrderService.Interfaces
{
    public interface IFirebaseRemoteConfigService
    {
        Task Init();

        Task<TInput> GetAsync<TInput>(string key);
    }
}