using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UnleashedApp.Authentication;
using UnleashedApp.Contracts;
using UnleashedApp.Models;
using UnleashedApp.Repositories.AuthenticationRepositories;

namespace UnleashedApp.Repositories
{
    public abstract class Repository
    {
        public static readonly HttpClient _client = new HttpClient();
        protected readonly Uri _baseAddress = new Uri(Constants.BASE_API_URL);
        protected IAuthenticationService authenticationService;
        protected IHttpClientAdapter httpClientAdapter;

        public Repository(IAuthenticationService authenticationService, IHttpClientAdapter httpClientAdapter)
        {
            _client.BaseAddress = _baseAddress;
            _client.Timeout = TimeSpan.FromSeconds(20);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.authenticationService = authenticationService;
            this.httpClientAdapter = httpClientAdapter;
        }

        public async Task<bool> AddAuthenticationHeaderAsync()
        {
            if (authenticationService.UserIsLoggedIn())
            {
                if (authenticationService.ShouldRefreshToken())
                {
                    CustomTokenResponse response = await RequestRefreshAccessTokenAsync(authenticationService.GetAPIRefreshToken());
                    if (response != null && response.access_token != null)
                    {
                       authenticationService.SaveCredentials(response);
                    }
                    else
                    {
                        return false;
                    }
                }
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationService.GetAPIAccessToken());
                return true;
            }
            else
            {
                _client.DefaultRequestHeaders.Authorization = null;
                return false;
            }
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

        public StringContent ConvertToJson(Object request)
        {
            string data = JsonConvert.SerializeObject(request);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            return content;
        }

        public async Task<CustomTokenResponse> ConvertToTokenObject(HttpResponseMessage response)
        {
            String tokenJson = await response.Content.ReadAsStringAsync();
            CustomTokenResponse customToken = JsonConvert.DeserializeObject<CustomTokenResponse>(tokenJson);
            return customToken;
        }
    }
}
