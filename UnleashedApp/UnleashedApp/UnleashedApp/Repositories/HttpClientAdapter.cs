using System.Net.Http;
using System.Threading.Tasks;

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
