using System;
using System.Linq;
using NUnit.Framework;
using UnleashedApp.Authentication;
using Xamarin.Auth;

namespace UnleashedApp.Tests.Services
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        private AuthenticationService service;
        private CustomTokenResponse apiResponse;

        [SetUp]
        public void Setup()
        {
            service = new AuthenticationService();
            apiResponse = new CustomTokenResponse()
            {
                access_token = "testAccessToken",
                expires_in = "36000",
                refresh_token = "testRefreshToken",
                scope = "",
                token_type = "bearer"
            };
        }

        //[Test]
        //public void TestSaveNewCredentials_SavesTokensForUser()
        //{
        //    //Arrange

        //    //Act
        //    service.SaveCredentials(apiResponse);

        //    //Assert

        //}
    }
}
