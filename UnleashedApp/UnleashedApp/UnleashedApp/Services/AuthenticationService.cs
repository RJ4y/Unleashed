using System.Linq;
using Xamarin.Auth;

namespace UnleashedApp.Authentication
{
    public class AuthenticationService
    {
        public void SaveCredentials(Account account, string API_token)
        {
            if (account != null && !string.IsNullOrWhiteSpace(API_token))
            {
                account.Properties.Add("API_token", API_token);
                AccountStore.Create().Save(account, Constants.APP_NAME);
            }
        }

        public string GetAPIAccessToken()
        {
            if (UserIsLoggedIn())
            {
                Account account = GetUser();
                return account.Properties["API_token"];
            }
            return null;
        }

        public void DeleteAccessToken()
        {
            var account = GetUser();
            account.Properties.Remove("API_token");
            AccountStore.Create().Save(account, Constants.APP_NAME);
        }

        public bool UserIsLoggedIn()
        {
            Account account = GetUser();
            if (account != null && account.Properties.ContainsKey("API_token"))
            {
                return true;
            }
            return false;
        }

        private Account GetUser()
        {
            return AccountStore.Create().FindAccountsForService(Constants.APP_NAME).FirstOrDefault();
        }
    }
}
