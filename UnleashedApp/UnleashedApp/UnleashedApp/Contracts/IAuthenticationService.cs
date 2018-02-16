using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnleashedApp.Authentication;
using UnleashedApp.Models;
using Xamarin.Auth;

namespace UnleashedApp.Contracts
{
    public interface IAuthenticationService
    {
        void SaveCredentials(CustomTokenResponse tokenResponse);
        void SaveCredentials(Account account, CustomTokenResponse tokenResponse);
        string GetAPIAccessToken();
        string GetAPIRefreshToken();
        string GetGoogleAccessToken();
        string GetUserFirstName();
        string GetUserLastName();
        bool ShouldRefreshToken();
        void DeleteAccessTokens();
        bool UserIsLoggedIn();
        Account GetUser();
        void SaveUserName(User user);
    }
}
