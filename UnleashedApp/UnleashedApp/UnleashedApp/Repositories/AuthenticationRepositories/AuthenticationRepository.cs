using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using UnleashedApp.Authentication;
using UnleashedApp.Contracts;
using UnleashedApp.Models;
using Xamarin.Auth;

namespace UnleashedApp.Repositories.AuthenticationRepositories
{
    public class AuthenticationRepository : Repository, IAuthenticationRepository
    {
        private readonly IAuthenticationHttpClientAdapter httpAuthClientAdapter;

        public AuthenticationRepository(IAuthenticationService authenticationService, IHttpClientAdapter httpClientAdapter, IAuthenticationHttpClientAdapter httpAuthClientAdapter): base(authenticationService, httpClientAdapter)
        {
            this.httpAuthClientAdapter = httpAuthClientAdapter;
        }

        public async Task<CustomTokenResponse> RequestExchangeGoogleTokenAsync(TokenConvertRequest tokenConvertRequest)
        {
            StringContent content = ConvertToJson(tokenConvertRequest);
            HttpResponseMessage response = await httpAuthClientAdapter.ExchangeTokenAsync(content);
            if (response != null && response.IsSuccessStatusCode)
            {
                return await ConvertToTokenObject(response);
            }
            return null;
        }

        public async Task<bool> RequestRevokeTokens()
        {
            TokenRevokeRequest revokeRequest = new TokenRevokeRequest();
            StringContent content = ConvertToJson(revokeRequest);
            HttpResponseMessage response = await httpAuthClientAdapter.PostRevokeTokensAsync(content);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<User> GetUserInfoAsync(Account account)
        {
            Response response = await httpAuthClientAdapter.GetUserInfoAsync(account);
            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                string userJson = response.GetResponseText();
                var user = JsonConvert.DeserializeObject<User>(userJson);
                return user;
            }
            return null;
        }
    }
}
