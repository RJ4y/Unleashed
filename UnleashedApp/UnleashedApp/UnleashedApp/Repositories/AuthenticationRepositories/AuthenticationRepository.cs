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

        public CustomTokenResponse GetCustomToken(TokenConvertRequest tokenRequest)
        {
            string data = JsonConvert.SerializeObject(tokenRequest);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            System.Diagnostics.Debug.WriteLine("CONTENT******************************************" + content.ReadAsStringAsync().Result);
            HttpResponseMessage response = _client.PostAsync(CONVERT_URL, content).Result;
            if (response.IsSuccessStatusCode)
            {
                String tokenJson = response.Content.ReadAsStringAsync().Result;
                CustomTokenResponse customToken = JsonConvert.DeserializeObject<CustomTokenResponse>(tokenJson);
                return customToken;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("**********************************************************************************************");
            }
            return null;
        }

        public void SaveCredentials(Account account, string API_token, string Google_token)
        {
            if (account != null && !string.IsNullOrWhiteSpace(API_token) && !string.IsNullOrWhiteSpace(Google_token))
            {
                account.Properties.Add("API_token", API_token);
                account.Properties.Add("Google_token", Google_token);
                AccountStore.Create().Save(account, Configuration.APP_NAME);
            }
        }

        public string GetAPIAccessToken()
        {
            var account = AccountStore.Create().FindAccountsForService(Configuration.APP_NAME).FirstOrDefault();
            return account?.Properties["API_token"];
        }
    }
}
