using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
            return await Client.PostAsync(CONVERT_URL, convertToken);
        }

        public async Task<HttpResponseMessage> PostRevokeTokensAsync(StringContent clientId)
        {
            await AddAuthenticationHeaderAsync();
            Debug.WriteLine("in client adapter stringcontent " + clientId.ReadAsStringAsync().Result);
            return await Client.PostAsync(REVOKE_URL, clientId);
        }
    }
}
