using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnleashedApp.Contracts;

namespace UnleashedApp.Repositories
{
    public class HttpClientAdapter: IHttpClientAdapter
    {
        public Task<HttpResponseMessage> GetRefreshedAccessTokenAsync(StringContent refreshToken)
        {
            return Repository.Client.PostAsync(Constants.REFRESH_URL, refreshToken);
        }

    }
}
