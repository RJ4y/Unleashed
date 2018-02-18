using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Auth;

namespace UnleashedApp.Repositories.AuthenticationRepositories
{
    public interface IAuthenticationHttpClientAdapter
    {
        Task<HttpResponseMessage> ExchangeTokenAsync(StringContent convertToken);
        Task<HttpResponseMessage> PostRevokeTokensAsync(StringContent clientId);
        Task<Response> GetUserInfoAsync(Account account);
    }
}
