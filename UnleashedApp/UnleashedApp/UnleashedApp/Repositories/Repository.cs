using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using UnleashedApp.Authentication;
using UnleashedApp.Models;
using UnleashedApp.Repositories.AuthenticationRepositories;

namespace UnleashedApp.Repositories
{
    public abstract class Repository
    {
        protected static readonly HttpClient _client = new HttpClient();
        //NOTE: phones will turn to their own (device's) localhost. so set the ip to the ip of the device/server running the python backend!
        //protected readonly Uri _baseAddress = new Uri("http://localhost:8000/");
        protected readonly Uri _baseAddress = new Uri(Constants.BASE_API_URL);
        public const string REFRESH_URL = "auth/token";

        public Repository()
        {
            _client.BaseAddress = _baseAddress;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async void AddAuthenticationHeaderAsync()
        {
            AuthenticationService authService = AuthenticationService.Instance;
            if (authService.UserIsLoggedIn())
            {
                if (authService.ShouldRefreshToken())
                {
                    IAuthenticationRepository authRepo = new AuthenticationRepository(new AuthenticationHttpClientAdapter());
                    CustomTokenResponse response = await authRepo.RequestRefreshAccessTokenAsync(authService.GetAPIRefreshToken());
                    if (response.access_token != null)
                    {
                        authService.SaveCredentials(response);
                    }
                }
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthenticationService.Instance.GetAPIAccessToken());
            }
        }
    }
}
