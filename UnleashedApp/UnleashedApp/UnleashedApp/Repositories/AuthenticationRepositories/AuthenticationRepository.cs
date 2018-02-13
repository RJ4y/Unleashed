﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UnleashedApp.Authentication;
using UnleashedApp.Contracts;
using UnleashedApp.Models;

namespace UnleashedApp.Repositories.AuthenticationRepositories
{
    public class AuthenticationRepository : Repository, IAuthenticationRepository
    {
        private readonly IAuthenticationHttpClientAdapter httpAuthClientAdapter;

        public AuthenticationRepository(IAuthenticationService authenticationService, IHttpClientAdapter httpClientAdapter, IAuthenticationHttpClientAdapter httpAuthClientAdapter): base(authenticationService, httpClientAdapter)
        {
            this.httpAuthClientAdapter = httpAuthClientAdapter;
        }

        public async Task<CustomTokenResponse> RequestExchangeGoogleTokenAsync(TokenConvertRequest tokenConvertRequest)
        {
            StringContent content = ConvertToJson(tokenConvertRequest);
            HttpResponseMessage response = await httpAuthClientAdapter.ExchangeTokenAsync(content);
            if (response != null && response.IsSuccessStatusCode)
            {
                return await ConvertToTokenObject(response);
            }
            return null;
        }

        public async Task<bool> RequestRevokeTokens()
        {
            TokenRevokeRequest revokeRequest = new TokenRevokeRequest();
            StringContent content = ConvertToJson(revokeRequest);
            HttpResponseMessage response = await httpAuthClientAdapter.PostRevokeTokensAsync(content);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<Dictionary<string, string>> GetUserName()
        {
            HttpResponseMessage response = await httpAuthClientAdapter.GetUserNameAsync();
            if (response != null && response.IsSuccessStatusCode)
            {
                string nameJson = await response.Content.ReadAsStringAsync();
                var nameObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(nameJson);
                return nameObject;
            }
            return null;
        }
    }
}
