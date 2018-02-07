using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnleashedApp.Authentication;
using Xamarin.Auth;

namespace UnleashedApp.Contracts
{
    public interface IAuthenticationService
    {
        void SaveCredentials(CustomTokenResponse tokenResponse);
        void SaveCredentials(Account account, CustomTokenResponse tokenResponse);
        string GetAPIAccessToken();
        string GetAPIRefreshToken();
        bool ShouldRefreshToken();
        void DeleteAccessTokens();
        bool UserIsLoggedIn();
        Account GetUser();
    }
}
