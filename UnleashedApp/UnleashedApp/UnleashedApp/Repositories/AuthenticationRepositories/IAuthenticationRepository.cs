using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnleashedApp.Authentication;
using Xamarin.Auth;

namespace UnleashedApp.Repositories.AuthenticationRepositories
{
    public interface IAuthenticationRepository
    {
        CustomTokenResponse GetCustomToken(TokenConvertRequest tokenRequest);
        void SaveCredentials(Account account, string API_Token, string Google_token);
        string GetAPIAccessToken();
    }
}
