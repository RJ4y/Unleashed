using System;
using NUnit.Framework;
using UnleashedApp.Authentication;

namespace UnleashedApp.Tests.Authentication
{
    [TestFixture]
    public class GoogleAuthenticatorTests
    {
        [Test]
        public void TestGoogleAuthenticator_InitializesAuthenticatorWithCorrectClientId()
        {
            Assert.IsNotNull(GoogleAuthenticator.Authenticator.ClientId);
        }

        [Test]
        public void TestGoogleAuthenticator_InitializesAuthenticatorWithCorrectScope()
        {
            Assert.AreEqual(GoogleAuthenticator.Authenticator.Scope, GoogleAuthenticator.Scope);
        }

        [Test]
        public void TestGoogleAuthenticator_InitializesAuthenticatorWithCorrectAuthorizeUri()
        {
            Assert.AreEqual(GoogleAuthenticator.Authenticator.AuthorizeUrl, "https://accounts.google.com/o/oauth2/v2/auth");
        }

        [Test]
        public void TestGoogleAuthenticator_InitializesAuthenticatorWithCorrectAccessTokenuri()
        {
            Assert.AreEqual(GoogleAuthenticator.Authenticator.AccessTokenUrl, "https://www.googleapis.com/oauth2/v4/token");
        }

        [Test]
        public void TestGoogleAuthenticator_ShouldUseNativeUI()
        {
            Assert.IsTrue(GoogleAuthenticator.Authenticator.IsUsingNativeUI);
        }
    }
}
