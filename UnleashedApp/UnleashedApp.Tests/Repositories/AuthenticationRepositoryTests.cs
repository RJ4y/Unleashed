using System;
using System.Net;
using System.Text;
using Moq;
using UnleashedApp.Authentication;
using UnleashedApp.Models;
using UnleashedApp.Repositories.AuthenticationRepositories;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace UnleashedApp.Tests.Repositories
{
    [TestFixture]
    public class AuthenticationRepositoryTests
    {
        IAuthenticationRepository repository;

        public void SetUp()
        {
        }


        [Test]
        public void TestExchangeValidGoogleToken_ShouldReturnCustomToken()
        {
            //Arrange
            TokenConvertRequest postRequest = new TokenConvertRequest("testGoogleToken");
            string data = JsonConvert.SerializeObject(postRequest);
            StringContent postContent = new StringContent(data, Encoding.UTF8, "application/json");

            CustomTokenResponse responseContent = new CustomTokenResponse()
            {
                access_token = "testAccessToken",
                expires_in = "36000",
                refresh_token = "testRefreshToken",
                scope = "",
                token_type = "bearer"
            };
            string responseDataJson = JsonConvert.SerializeObject(responseContent);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseDataJson, Encoding.UTF8, "application/json")
            };

            var mockAPI = new Mock<IAuthenticationHttpClientAdapter>();
            mockAPI.Setup(api => api.ExchangeTokenAsync(postContent)).Returns(Task.FromResult(response));

            repository = new AuthenticationRepository(mockAPI.Object);

            //Act
            var result = repository.RequestExchangeGoogleTokenAsync(postRequest).Result as CustomTokenResponse;

            //Assert
            mockAPI.Verify(api => api.ExchangeTokenAsync(postContent), Times.Once);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.access_token, Is.EqualTo("testAccessToken"));

        }
    }
}