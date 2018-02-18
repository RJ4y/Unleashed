using System.Net.Http;
using System.Threading.Tasks;

namespace UnleashedApp.Repositories
{
    public interface IHttpClientAdapter
    {
        Task<HttpResponseMessage> GetRefreshedAccessTokenAsync(StringContent refreshToken);
    }
}
