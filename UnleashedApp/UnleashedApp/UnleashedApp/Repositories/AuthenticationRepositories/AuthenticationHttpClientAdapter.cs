using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UnleashedApp.Repositories.AuthenticationRepositories
{
    public class AuthenticationHttpClientAdapter : Repository,  IAuthenticationHttpClientAdapter
    {
        public const string CONVERT_URL = "auth/convert-token";
        public const string REVOKE_URL = "auth/invalidate-sessions";

        public async Task<HttpResponseMessage> ExchangeTokenAsync(StringContent convertToken)
        {
            return await _client.PostAsync(CONVERT_URL, convertToken);
        }

        public Task<HttpResponseMessage> GetRefreshedAccessTokenAsync(StringContent refreshToken)
        {
            return _client.PostAsync(REFRESH_URL, refreshToken);
        }

        public async Task<HttpResponseMessage> PostRevokeTokensAsync(StringContent revokeToken)
        {
            AddAuthenticationHeaderAsync();
            return await _client.PostAsync(REVOKE_URL, revokeToken);
        }
    }
}
