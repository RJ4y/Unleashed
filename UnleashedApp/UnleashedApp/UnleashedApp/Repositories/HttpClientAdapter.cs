using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnleashedApp.Contracts;
using Xamarin.Forms;

namespace UnleashedApp.Repositories
{
    public class HttpClientAdapter : IHttpClientAdapter
    {
        public Task<HttpResponseMessage> GetRefreshedAccessTokenAsync(StringContent refreshToken)
        {
            try
            {
                return Repository.Client.PostAsync(Constants.RefreshUrl, refreshToken);
            }
            catch (TaskCanceledException ex)
            {
                return null;
            }
        }
    }
}
