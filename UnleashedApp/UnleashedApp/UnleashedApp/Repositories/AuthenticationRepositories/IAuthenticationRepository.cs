using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnleashedApp.Authentication;

namespace UnleashedApp.Repositories.AuthenticationRepositories
{
    public interface IAuthenticationRepository
    {
        Task<CustomTokenResponse> ExchangeGoogleTokenAsync(TokenConvertRequest tokenRequest);
    }
}
