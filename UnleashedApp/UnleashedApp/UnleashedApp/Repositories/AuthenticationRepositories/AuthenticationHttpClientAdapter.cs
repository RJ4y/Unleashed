using System;
using System.Net.Http;
using System.Threading.Tasks;
using UnleashedApp.Contracts;
using Xamarin.Auth;

namespace UnleashedApp.Repositories.AuthenticationRepositories
{
    public class AuthenticationHttpClientAdapter : Repository, IAuthenticationHttpClientAdapter
    {
        public const string CONVERT_URL = "auth/convert-token";
        public const string REVOKE_URL = "auth/invalidate-sessions";

        public AuthenticationHttpClientAdapter(IAuthenticationService authenticationService, IHttpClientAdapter httpClientAdapter) : base(authenticationService, httpClientAdapter)
        {
        }

        public async Task<HttpResponseMessage> ExchangeTokenAsync(StringContent convertToken)
        {
            try
            {
                return await Client.PostAsync(CONVERT_URL, convertToken);
            }
            catch (TaskCanceledException ex)
            {
                return null;
            }
        }

        public async Task<HttpResponseMessage> PostRevokeTokensAsync(StringContent clientId)
        {
            await AddAuthenticationHeaderAsync();
            try
            {
                return await Client.PostAsync(REVOKE_URL, clientId);
            }
            catch (TaskCanceledException ex)
            {
                return null;
            }
        }

        public async Task<Response> GetUserInfoAsync(Account account)
        {
            try
            {
                var request = new OAuth2Request("GET", new Uri("https://www.googleapis.com/oauth2/v2/userinfo"), null, account);
                return await request.GetResponseAsync();
                
            }
            catch (TaskCanceledException ex)
            {
                return null;
            }
        }
    }
}
