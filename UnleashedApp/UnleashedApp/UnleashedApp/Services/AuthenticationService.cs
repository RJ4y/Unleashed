using System;
using System.Linq;
using UnleashedApp.Contracts;
using Xamarin.Auth;

namespace UnleashedApp.Authentication
{
    public class AuthenticationService: IAuthenticationService
    {
        private static AuthenticationService instance;
        private Account user;

        private AuthenticationService()
        {
            user = GetUser();
        }

        public static AuthenticationService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AuthenticationService();
                }
                return instance; }
        }

        public void SaveCredentials(CustomTokenResponse tokenResponse)
        {
            Account account = GetUser();
            SaveTokens(account, tokenResponse);
        }

        public void SaveCredentials(Account account, CustomTokenResponse tokenResponse)
        {
            SaveTokens(account, tokenResponse);

        }

        public string GetAPIAccessToken()
        {
            if(user.Properties.ContainsKey(Constants.ACCOUNT_PROPERTY_ACCESS_TOKEN))
                return user.Properties[Constants.ACCOUNT_PROPERTY_ACCESS_TOKEN];
            return null;
        }

        public string GetAPIRefreshToken()
        {
            if (user.Properties.ContainsKey(Constants.ACCOUNT_PROPERTY_REFRESH_TOKEN))
                return user.Properties[Constants.ACCOUNT_PROPERTY_REFRESH_TOKEN];
            return null;
        }

        public bool ShouldRefreshToken()
        {
            DateTime expiration = DateTime.Parse(user.Properties[Constants.ACCOUNT_PROPERTY_EXPIRATION]);
            if (expiration < DateTime.Now)
            {
                return true;
            }
            return false;
        }

        private void SaveTokens(Account account, CustomTokenResponse tokenResponse)
        {
            if (account != null && !string.IsNullOrWhiteSpace(tokenResponse.access_token) && !string.IsNullOrWhiteSpace(tokenResponse.refresh_token))
            {
                if (account.Properties.ContainsKey(Constants.ACCOUNT_PROPERTY_ACCESS_TOKEN)) 
                    account.Properties[Constants.ACCOUNT_PROPERTY_ACCESS_TOKEN] = tokenResponse.access_token;
                account.Properties.Add(Constants.ACCOUNT_PROPERTY_ACCESS_TOKEN, tokenResponse.access_token);

                if (account.Properties.ContainsKey(Constants.ACCOUNT_PROPERTY_REFRESH_TOKEN))
                    account.Properties[Constants.ACCOUNT_PROPERTY_REFRESH_TOKEN] = tokenResponse.refresh_token;
                account.Properties.Add(Constants.ACCOUNT_PROPERTY_REFRESH_TOKEN, tokenResponse.refresh_token);

                DateTime expiration = CalculateTokenExpiration(tokenResponse);

                if (account.Properties.ContainsKey(Constants.ACCOUNT_PROPERTY_EXPIRATION))
                    account.Properties[Constants.ACCOUNT_PROPERTY_EXPIRATION] = expiration.ToString();
                account.Properties.Add(Constants.ACCOUNT_PROPERTY_EXPIRATION, expiration.ToString());

                AccountStore.Create().Save(account, Constants.APP_NAME);
                user = account;
            }
        }

        public void DeleteAccessTokens()
        {
            user.Properties.Remove(Constants.ACCOUNT_PROPERTY_ACCESS_TOKEN);
            user.Properties.Remove(Constants.ACCOUNT_PROPERTY_REFRESH_TOKEN);
            user.Properties.Remove(Constants.ACCOUNT_PROPERTY_EXPIRATION);
            AccountStore.Create().Save(user, Constants.APP_NAME);
        }

        public bool UserIsLoggedIn()
        {
            if (user != null && user.Properties.ContainsKey(Constants.ACCOUNT_PROPERTY_REFRESH_TOKEN))
                return true;
            return false;
        }

        public Account GetUser()
        {
            return AccountStore.Create().FindAccountsForService(Constants.APP_NAME).FirstOrDefault();
        }

        private DateTime CalculateTokenExpiration(CustomTokenResponse tokenResponse)
        {
            int expiresIn = int.Parse(tokenResponse.expires_in);
            return DateTime.Now.AddSeconds(expiresIn);
        }
    }
}
