using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnleashedApp.Authentication;
using Xamarin.Auth;

namespace UnleashedApp.Repositories.AuthenticationRepositories
{
    public class AuthenticationRepository : Repository, IAuthenticationRepository
    {
        public const string CONVERT_URL = "auth/convert-token";

        public async Task<CustomTokenResponse> GetCustomTokenAsync(TokenConvertRequest tokenRequest)
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

        public void SaveCredentials(Account account, string API_token)
        {
            if (account != null && !string.IsNullOrWhiteSpace(API_token))
            {
                account.Properties.Add("API_token", API_token);
                AccountStore.Create().Save(account, Configuration.APP_NAME);
            }
        }

        public string GetAPIAccessToken()
        {
            var account = AccountStore.Create().FindAccountsForService(Configuration.APP_NAME).FirstOrDefault();
            if(account != null && account.Properties.ContainsKey("API_token"))
            {
                return account.Properties["API_token"];
            }
            return null;
        }

        public void DeleteAccessToken()
        {
            var account = AccountStore.Create().FindAccountsForService(Configuration.APP_NAME).FirstOrDefault();
            account?.Properties.Remove("API_token");
        }
    }
}
