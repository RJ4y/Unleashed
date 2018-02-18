using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnleashedApp.Authentication;
using UnleashedApp.Models;
using Xamarin.Auth;

namespace UnleashedApp.Repositories.AuthenticationRepositories
{
    public interface IAuthenticationRepository
    {
        Task<CustomTokenResponse> RequestExchangeGoogleTokenAsync(TokenConvertRequest tokenConvertRequest);
        Task<CustomTokenResponse> RequestRefreshAccessTokenAsync(string refreshToken);
        Task<bool> RequestRevokeTokens();
        Task<User> GetUserInfoAsync(Account account);
    }
}
