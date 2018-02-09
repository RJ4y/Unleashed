using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UnleashedApp.Repositories
{
    public interface IHttpClientAdapter
    {
        Task<HttpResponseMessage> GetRefreshedAccessTokenAsync(StringContent refreshToken);
    }
}
