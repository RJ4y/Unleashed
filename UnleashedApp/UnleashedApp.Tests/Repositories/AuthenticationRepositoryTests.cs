using System;
using System.Net;
using System.Text;
using Moq;
using UnleashedApp.Authentication;
using UnleashedApp.Repositories.AuthenticationRepositories;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using UnleashedApp.Contracts;
using UnleashedApp.Repositories;

namespace UnleashedApp.Tests.Repositories
{
    [TestFixture]
    public class AuthenticationRepositoryTests
    {
        private IAuthenticationRepository repository;
        private IAuthenticationService authService;

        [SetUp]
        public void Setup()
        {
            authService = new Mock<IAuthenticationService>().Object;
        }

        [Test]
        public void TestExchangeValidGoogleToken_ShouldReturnCustomToken()
        {
            //Arrange
            TokenConvertRequest postRequest = new TokenConvertRequest("testGoogleToken");
            CustomTokenResponse responseContent = new CustomTokenResponse()
            {
                access_token = "testAccessToken",
                expires_in = "36000",
                refresh_token = "testRefreshToken",
                scope = "",
                token_type = "bearer"
            };
            string responseDataJson = JsonConvert.SerializeObject(responseContent);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(responseDataJson, Encoding.UTF8, "application/json");

            var mockAPI = new Mock<IAuthenticationHttpClientAdapter>();
            mockAPI.Setup(api => api.ExchangeTokenAsync(It.IsAny<StringContent>())).Returns(Task.FromResult(response));

            repository = new AuthenticationRepository(authService, new HttpClientAdapter(), mockAPI.Object);

            //Act
            var result = repository.RequestExchangeGoogleTokenAsync(postRequest).Result;

            //Assert
            mockAPI.Verify(api => api.ExchangeTokenAsync(It.IsAny<StringContent>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.That(result.access_token, Is.EqualTo("testAccessToken"));
        }

        [Test]
        public void TestExchangeInvalidGoogleToken_ShouldReturnNull()
        {
            //Arrange
            TokenConvertRequest postRequest = new TokenConvertRequest("testGoogleToken");
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest);

            var mockAPI = new Mock<IAuthenticationHttpClientAdapter>();
            mockAPI.Setup(api => api.ExchangeTokenAsync(It.IsAny<StringContent>())).Returns(Task.FromResult(response));

            repository = new AuthenticationRepository(authService, new HttpClientAdapter(), mockAPI.Object);

            //Act
            var result = repository.RequestExchangeGoogleTokenAsync(postRequest).Result;

            //Assert
            mockAPI.Verify(api => api.ExchangeTokenAsync(It.IsAny<StringContent>()), Times.Once);
            Assert.IsNull(result);
        }
        
        [Test]
        public void TestRevokeTokens_ShouldReturnTrue()
        {
            //Arrange
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
           
            var mockApi = new Mock<IAuthenticationHttpClientAdapter>();
            mockApi.Setup(api => api.PostRevokeTokensAsync(It.IsAny<StringContent>())).Returns(Task.FromResult(response));

            repository = new AuthenticationRepository(authService, new HttpClientAdapter(), mockApi.Object);

            //Act
            var result = repository.RequestRevokeTokens().Result;

            //Assert
            mockApi.Verify(api => api.PostRevokeTokensAsync(It.IsAny<StringContent>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.True(result);
        }

        [Test]
        public void TestRevokeTokens_BadRequest_ShouldReturnFalse()
        {
            //Arrange
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest);

            var mockApi = new Mock<IAuthenticationHttpClientAdapter>();
            mockApi.Setup(api => api.PostRevokeTokensAsync(It.IsAny<StringContent>())).Returns(Task.FromResult(response));

            repository = new AuthenticationRepository(authService, new HttpClientAdapter(), mockApi.Object);

            //Act
            var result = repository.RequestRevokeTokens().Result;

            //Assert
            mockApi.Verify(api => api.PostRevokeTokensAsync(It.IsAny<StringContent>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.False(result);
        }
    }
}