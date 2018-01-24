using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnleashedApp.Authentication;

namespace UnleashedApp.Repositories.AuthenticationRepositories
{
    public class AuthenticationRepository : Repository, IAuthenticationRepository
    {
        public const string CONVERT_URL = "auth/convert-token";
        public const string REFRESH_URL = "auth/token";

        public async Task<CustomTokenResponse> ExchangeGoogleTokenAsync(TokenConvertRequest tokenRequest)
        {
            string data = JsonConvert.SerializeObject(tokenRequest);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(CONVERT_URL, content);
            if (response.IsSuccessStatusCode)
            {
                String tokenJson = await response.Content.ReadAsStringAsync();
                CustomTokenResponse customToken = JsonConvert.DeserializeObject<CustomTokenResponse>(tokenJson);
                return customToken;
            }
            return null;
        }

    
    }
}
