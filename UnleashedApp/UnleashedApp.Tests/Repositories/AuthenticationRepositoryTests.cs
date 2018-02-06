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
        private IAuthenticationRepository repository;

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

            repository = new AuthenticationRepository(mockAPI.Object);

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

            repository = new AuthenticationRepository(mockAPI.Object);

            //Act
            var result = repository.RequestExchangeGoogleTokenAsync(postRequest).Result;

            //Assert
            mockAPI.Verify(api => api.ExchangeTokenAsync(It.IsAny<StringContent>()), Times.Once);
            Assert.IsNull(result);
        }

        [Test]
        public void TestRefreshToken_ShouldReturnNewCustomToken()
        {
            //Arrange
            string refreshToken = "testRefreshToken";

            CustomTokenResponse responseContent = new CustomTokenResponse()
            {
                access_token = "newAccessToken",
                expires_in = "36000",
                refresh_token = refreshToken,
                scope = "",
                token_type = "bearer"
            };
            string responseDataJson = JsonConvert.SerializeObject(responseContent);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(responseDataJson, Encoding.UTF8, "application/json");

            var mockApi = new Mock<IAuthenticationHttpClientAdapter>();
            mockApi.Setup(api => api.GetRefreshedAccessTokenAsync(It.IsAny<StringContent>())).Returns(Task.FromResult(response));

            repository = new AuthenticationRepository(mockApi.Object);

            //Act
            var result = repository.RequestRefreshAccessTokenAsync(refreshToken).Result as CustomTokenResponse;

            //Assert
            mockApi.Verify(api => api.GetRefreshedAccessTokenAsync(It.IsAny<StringContent>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.That(result.access_token, Is.EqualTo("newAccessToken"));
        }

        [Test]
        public void TestRefreshInvalidGoogleToken_ShouldReturnNull()
        {
            //Arrange
            string refreshToken = "testRefreshToken";
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest);

            var mockAPI = new Mock<IAuthenticationHttpClientAdapter>();
            mockAPI.Setup(api => api.GetRefreshedAccessTokenAsync(It.IsAny<StringContent>())).Returns(Task.FromResult(response));

            repository = new AuthenticationRepository(mockAPI.Object);

            //Act
            var result = repository.RequestRefreshAccessTokenAsync(refreshToken).Result;

            //Assert
            mockAPI.Verify(api => api.GetRefreshedAccessTokenAsync(It.IsAny<StringContent>()), Times.Once);
            Assert.IsNull(result);
        }

        [Test]
        public void TestRevokeTokens_ShouldReturnTrue()
        {
            //Arrange
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
           
            var mockApi = new Mock<IAuthenticationHttpClientAdapter>();
            mockApi.Setup(api => api.PostRevokeTokensAsync(It.IsAny<StringContent>())).Returns(Task.FromResult(response));

            repository = new AuthenticationRepository(mockApi.Object);

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

            repository = new AuthenticationRepository(mockApi.Object);

            //Act
            var result = repository.RequestRevokeTokens().Result;

            //Assert
            mockApi.Verify(api => api.PostRevokeTokensAsync(It.IsAny<StringContent>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.False(result);
        }
    }
}