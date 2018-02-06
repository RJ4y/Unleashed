using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UnleashedApp.Authentication;
using UnleashedApp.Models;

namespace UnleashedApp.Repositories.AuthenticationRepositories
{
    public class AuthenticationRepository : Repository, IAuthenticationRepository
    {
        private readonly IAuthenticationHttpClientAdapter httpClientAdapter;

        public AuthenticationRepository(IAuthenticationHttpClientAdapter httpClientAdapter)
        {
            this.httpClientAdapter = httpClientAdapter;
        }

        public async Task<CustomTokenResponse> RequestExchangeGoogleTokenAsync(TokenConvertRequest tokenConvertRequest)
        {
            StringContent content = ConvertToJson(tokenConvertRequest);
            HttpResponseMessage response = await httpClientAdapter.ExchangeTokenAsync(content);
            if (response != null && response.IsSuccessStatusCode)
            {
                return await ConvertToTokenObject(response);
            }
            return null;
        }

        public async Task<CustomTokenResponse> RequestRefreshAccessTokenAsync(string refreshToken)
        {
            TokenRefreshRequest refreshRequest = new TokenRefreshRequest(refreshToken);
            StringContent content = ConvertToJson(refreshRequest);
            HttpResponseMessage response = await httpClientAdapter.GetRefreshedAccessTokenAsync(content);
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
            HttpResponseMessage response = await httpClientAdapter.PostRevokeTokensAsync(content);
            if (response != null && response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        private StringContent ConvertToJson(TokenRequest request)
        {
            string data = JsonConvert.SerializeObject(request);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            return content;
        }

        private async Task<CustomTokenResponse> ConvertToTokenObject(HttpResponseMessage response)
        {
            String tokenJson = await response.Content.ReadAsStringAsync();
            CustomTokenResponse customToken = JsonConvert.DeserializeObject<CustomTokenResponse>(tokenJson);
            return customToken;
        }
    }
}
