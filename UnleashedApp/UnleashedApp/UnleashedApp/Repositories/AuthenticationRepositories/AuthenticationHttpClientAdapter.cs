using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UnleashedApp.Authentication;
using UnleashedApp.Contracts;

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

        public async Task<HttpResponseMessage> GetUserNameAsync()
        {
            try
            {
                string googleAccesToken = authenticationService.GetGoogleAccessToken();
                UriBuilder builder = new UriBuilder("https://www.googleapis.com/oauth2/v2/userinfo");
                builder.Query = "fields=family_name%2Cgiven_name&key=" + googleAccesToken;
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", googleAccesToken);
                return await Client.GetAsync(builder.Uri);
            }
            catch (TaskCanceledException ex)
            {
                return null;
            }
        }
    }
}
