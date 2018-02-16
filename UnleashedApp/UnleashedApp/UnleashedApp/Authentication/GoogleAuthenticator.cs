using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;

namespace UnleashedApp.Authentication
{
    public static class GoogleAuthenticator
    {

        private static readonly string ClientId = Constants.ClientId;
        public const string Scope = "email profile";
        private const string AuthorizeUri = "https://accounts.google.com/o/oauth2/v2/auth";
        private const string AccesstokenUri = "https://www.googleapis.com/oauth2/v4/token";

        public static string RedirectScheme = Constants.RedirectScheme;
        public const string RedirectPath = "oauth2redirect";                

        public static readonly OAuth2Authenticator Authenticator;

        static GoogleAuthenticator()
        {
            Authenticator = new OAuth2Authenticator(
                ClientId,
                null,
                Scope,
                new Uri(AuthorizeUri),
                new Uri(RedirectScheme + ":/" + RedirectPath),
                new Uri(AccesstokenUri),
                null,
                true);
        }
    }
}
