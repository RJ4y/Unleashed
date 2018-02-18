using System;
using System.Linq;
using UnleashedApp.Contracts;
using UnleashedApp.Models;
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
            if(user.Properties.ContainsKey(Constants.AccountPropertyAccessToken))
                return user.Properties[Constants.AccountPropertyAccessToken];
            return null;
        }

        public string GetAPIRefreshToken()
        {
            if (user.Properties.ContainsKey(Constants.AccountPropertyRefreshToken))
                return user.Properties[Constants.AccountPropertyRefreshToken];
            return null;
        }

        public string GetGoogleAccessToken()
        {
            if (user.Properties.ContainsKey("access_token"))
                return user.Properties["access_token"];
            return null;
        }

        public string GetUserFirstName()
        {
            if (user.Properties.ContainsKey(Constants.AccountPropertyFirstName))
                return user.Properties[Constants.AccountPropertyFirstName];
            return null;
        }

        public string GetUserLastName()
        {
            if (user.Properties.ContainsKey(Constants.AccountPropertyLastName))
                return user.Properties[Constants.AccountPropertyLastName];
            return null;
        }

        public bool ShouldRefreshToken()
        {
            DateTime expiration = DateTime.Parse(user.Properties[Constants.AccountPropertyExpiration]);
            return expiration < DateTime.Now;
        }

        private void SaveTokens(Account account, CustomTokenResponse tokenResponse)
        {
            if (account != null && !string.IsNullOrWhiteSpace(tokenResponse.access_token) && !string.IsNullOrWhiteSpace(tokenResponse.refresh_token))
            {
                if (account.Properties.ContainsKey(Constants.AccountPropertyAccessToken)) 
                    account.Properties[Constants.AccountPropertyAccessToken] = tokenResponse.access_token;
                account.Properties.Add(Constants.AccountPropertyAccessToken, tokenResponse.access_token);

                if (account.Properties.ContainsKey(Constants.AccountPropertyRefreshToken))
                    account.Properties[Constants.AccountPropertyRefreshToken] = tokenResponse.refresh_token;
                account.Properties.Add(Constants.AccountPropertyRefreshToken, tokenResponse.refresh_token);

                DateTime expiration = CalculateTokenExpiration(tokenResponse);

                if (account.Properties.ContainsKey(Constants.AccountPropertyExpiration))
                    account.Properties[Constants.AccountPropertyExpiration] = expiration.ToString();
                account.Properties.Add(Constants.AccountPropertyExpiration, expiration.ToString());

                AccountStore.Create().Save(account, Constants.AppName);
                user = account;
            }
        }

        public void DeleteAccessTokens()
        {
            user.Properties.Remove(Constants.AccountPropertyAccessToken);
            user.Properties.Remove(Constants.AccountPropertyRefreshToken);
            user.Properties.Remove(Constants.AccountPropertyExpiration);
            AccountStore.Create().Save(user, Constants.AppName);
        }

        public bool UserIsLoggedIn()
        {
            return user != null && user.Properties.ContainsKey(Constants.AccountPropertyRefreshToken);
        }

        public Account GetUser()
        {
            return AccountStore.Create().FindAccountsForService(Constants.AppName).FirstOrDefault();
        }

        public void SaveUserName(User loggedInUser)
        {
            if (user.Properties.ContainsKey(Constants.AccountPropertyFirstName) && user.Properties.ContainsKey(Constants.AccountPropertyLastName))
            {
                user.Properties[Constants.AccountPropertyFirstName] = loggedInUser.FirstName;
                user.Properties[Constants.AccountPropertyLastName] = loggedInUser.LastName;
            }
            else
            {
                user.Properties.Add(Constants.AccountPropertyFirstName, loggedInUser.FirstName);
                user.Properties.Add(Constants.AccountPropertyLastName, loggedInUser.LastName);
            }
        }

        private DateTime CalculateTokenExpiration(CustomTokenResponse tokenResponse)
        {
            int expiresIn = int.Parse(tokenResponse.expires_in);
            return DateTime.Now.AddSeconds(expiresIn);
        }
    }
}
