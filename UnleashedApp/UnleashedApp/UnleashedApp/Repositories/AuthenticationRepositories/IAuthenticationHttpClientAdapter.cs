using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UnleashedApp.Repositories.AuthenticationRepositories
{
    public interface IAuthenticationHttpClientAdapter
    {
        Task<HttpResponseMessage> ExchangeTokenAsync(StringContent convertToken);
        Task<HttpResponseMessage> PostRevokeTokensAsync(StringContent clientId);
    }
}
