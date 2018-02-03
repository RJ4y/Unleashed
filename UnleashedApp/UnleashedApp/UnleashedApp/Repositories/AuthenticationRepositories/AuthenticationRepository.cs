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
        public const string CONVERT_URL = "auth/convert-token";
        public const string REVOKE_URL = "auth/invalidate-sessions";

        public async Task<CustomTokenResponse> RequestExchangeGoogleTokenAsync(TokenConvertRequest tokenConvertRequest)
        {
            StringContent content = ConvertToJson(tokenConvertRequest);
            HttpResponseMessage response = await _client.PostAsync(CONVERT_URL, content);
            if (response.IsSuccessStatusCode)
            {
                return await ConvertToTokenObject(response);
            }
            return null;
        }

        public async Task<CustomTokenResponse> RequestRefreshAccessTokenAsync(string refreshToken)
        {
            TokenRefreshRequest refreshRequest = new TokenRefreshRequest(refreshToken);
            StringContent content = ConvertToJson(refreshRequest);
            HttpResponseMessage response = await _client.PostAsync(REFRESH_URL, content);
            if (response.IsSuccessStatusCode)
            {
                return await ConvertToTokenObject(response);
            }
            return null;
        }

        public async Task<bool> RequestRevokeTokens()
        {
            TokenRevokeRequest revokeRequest = new TokenRevokeRequest();
            StringContent content = ConvertToJson(revokeRequest);
            Debug.WriteLine("*****************************************************************" + content.ReadAsStringAsync().Result);
            AddAuthenticationHeaderAsync();
            Debug.WriteLine("*****************************************************************" + _client.DefaultRequestHeaders);
            HttpResponseMessage response = await _client.PostAsync(REVOKE_URL, content);
            if (response.IsSuccessStatusCode)
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
