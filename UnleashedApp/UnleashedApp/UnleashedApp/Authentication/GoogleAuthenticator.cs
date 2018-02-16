﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;

namespace UnleashedApp.Authentication
{
    public static class GoogleAuthenticator
    {

        private const string CLIENT_ID = Constants.CLIENT_ID;

        public const string SCOPE = "email profile";
        private const string AUTHORIZE_URI = "https://accounts.google.com/o/oauth2/v2/auth";
        private const string ACCESSTOKEN_URI = "https://www.googleapis.com/oauth2/v4/token";

        public const string REDIRECT_SCHEME = Constants.REDIRECT_SCHEME;
        public const string REDIRECT_PATH = "oauth2redirect";                

        public static readonly OAuth2Authenticator Authenticator;

        static GoogleAuthenticator()
        {
            Authenticator = new OAuth2Authenticator(
                CLIENT_ID,
                null,
                SCOPE,
                new Uri(AUTHORIZE_URI),
                new Uri(REDIRECT_SCHEME + ":/" + REDIRECT_PATH),
                new Uri(ACCESSTOKEN_URI),
                null,
                true);
        }
    }
}
