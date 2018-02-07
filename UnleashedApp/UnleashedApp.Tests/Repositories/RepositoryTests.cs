using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnleashedApp.Authentication;
using UnleashedApp.Contracts;
using UnleashedApp.Repositories;
using UnleashedApp.Repositories.AuthenticationRepositories;
using UnleashedApp.Repositories.EmployeeRepositories;
using Validation;
using Xamarin.Auth;

namespace UnleashedApp.Tests.Repositories
{
    [TestFixture]
    public class RepositoryTests
    {
        private Mock<IAuthenticationService> authServiceMock;
        private Mock<IHttpClientAdapter> httpClientMock;

        [SetUp]
        public void Setup()
        {
            authServiceMock = new Mock<IAuthenticationService>();
            httpClientMock = new Mock<IHttpClientAdapter>();
        }

        [TearDown]
        public void Teardown()
        {
            authServiceMock = null;
            httpClientMock = null;
        }

        [Test]
        public void TestConstructor_ShouldAddJsonHeader()
        {
            //Act
            new EmployeeRepository(authServiceMock.Object, httpClientMock.Object);

            //Assert
            Assert.IsNotNull(Repository._client.DefaultRequestHeaders.Accept);
            Assert.AreEqual(Repository._client.DefaultRequestHeaders.Accept.Count, 1);
            Assert.IsTrue(Repository._client.DefaultRequestHeaders.Accept.Contains(new MediaTypeWithQualityHeaderValue("application/json")));
        }

        [Test]
        public void TestAddAddAuthenticationHeader_UserIsloggedIn_ShouldAddTokenToRequest()
        {
            //Arrange
            authServiceMock.Setup(s => s.GetUser()).Returns(new Account());
            authServiceMock.Setup(s => s.UserIsLoggedIn()).Returns(true);
            authServiceMock.Setup(s => s.ShouldRefreshToken()).Returns(false);
            authServiceMock.Setup(s => s.GetAPIAccessToken()).Returns("access_token");
            Repository repository = new EmployeeRepository(authServiceMock.Object, httpClientMock.Object);
            var authHeader = new AuthenticationHeaderValue("bearer", "access_token");

            //Act
            bool result = repository.AddAuthenticationHeaderAsync().Result;

            //Assert
            Assert.IsTrue(result);
            Assert.IsNotNull(Repository._client.DefaultRequestHeaders.Authorization);
            Assert.AreEqual(Repository._client.DefaultRequestHeaders.Authorization, authHeader);
        }

        [Test]
        public void TestAddAddAuthenticationHeader_UserIsNotloggedIn_ShouldNotAddTokenToRequest()
        {
            //Arrange
            authServiceMock.Setup(s => s.UserIsLoggedIn()).Returns(false);
            authServiceMock.Setup(s => s.ShouldRefreshToken()).Returns(false);
            Repository repository = new EmployeeRepository(authServiceMock.Object, httpClientMock.Object);

            //Act
            bool result = repository.AddAuthenticationHeaderAsync().Result;

            //Assert
            Assert.IsNull(Repository._client.DefaultRequestHeaders.Authorization);
            Assert.IsFalse(result);
        }


        [Test]
        public void TestAddAddAuthenticationHeader_UserIsloggedIn_RefreshToken_ShouldAddTokenToRequest()
        {
            //Arrange
            CustomTokenResponse tokenResponse = new CustomTokenResponse
            {
                access_token = "new_access",
                expires_in = "36000",
                refresh_token = "refresh_token",
                scope = "",
                token_type = "bearer"
            };
            string responseJson = JsonConvert.SerializeObject(tokenResponse);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(responseJson, Encoding.UTF8, "application/json");

            string refreshToken = "test refresh_token";

            authServiceMock.Setup(s => s.GetAPIAccessToken()).Returns(tokenResponse.access_token);
            authServiceMock.Setup(s => s.UserIsLoggedIn()).Returns(true);
            authServiceMock.Setup(s => s.ShouldRefreshToken()).Returns(true);
            authServiceMock.Setup(s => s.GetAPIRefreshToken()).Returns(refreshToken);
            authServiceMock.Setup(s => s.SaveCredentials(tokenResponse));

            httpClientMock.Setup(client => client.GetRefreshedAccessTokenAsync(It.IsAny<StringContent>()))
                .Returns(Task.FromResult(response));

            var authRepoMock = new Mock<IAuthenticationRepository>();
            authRepoMock.Setup(repo => repo.RequestRefreshAccessTokenAsync(refreshToken))
                .Returns(Task.FromResult(tokenResponse));
            Repository repository = new EmployeeRepository(authServiceMock.Object, httpClientMock.Object);
            var authHeader = new AuthenticationHeaderValue("bearer", tokenResponse.access_token);

            //Act
            bool result = repository.AddAuthenticationHeaderAsync().Result;

            //Assert
            httpClientMock.Verify(client => client.GetRefreshedAccessTokenAsync(It.IsAny<StringContent>()), Times.Once);
            Assert.IsTrue(result);
            Assert.IsNotNull(Repository._client.DefaultRequestHeaders.Authorization);
            Assert.AreEqual(Repository._client.DefaultRequestHeaders.Authorization, authHeader);
        }

        [Test]
        public void TestAddAddAuthenticationHeader_UserNotloggedIn_RefreshToken_ShouldNotAddHeader()
        {
            //Arrange
            string refreshToken = "testRefreshToken";

            authServiceMock.Setup(s => s.UserIsLoggedIn()).Returns(false);
            Repository repository = new EmployeeRepository(authServiceMock.Object, httpClientMock.Object);

            //Act
            bool result = repository.AddAuthenticationHeaderAsync().Result;

            //Assert
            httpClientMock.Verify(api => api.GetRefreshedAccessTokenAsync(It.IsAny<StringContent>()), Times.Never);
            Assert.IsNull(Repository._client.DefaultRequestHeaders.Authorization);
            Assert.IsFalse(result);
        }

        [Test]
        public void TestAddAddAuthenticationHeader_UserIsloggedIn_InvalidRefreshToken_ShouldReturnFalse()
        {
            //Arrange
            string refreshToken = "testRefreshToken";

            authServiceMock.Setup(s => s.UserIsLoggedIn()).Returns(true);
            authServiceMock.Setup(s => s.ShouldRefreshToken()).Returns(true);
            authServiceMock.Setup(s => s.GetAPIRefreshToken()).Returns(refreshToken);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            httpClientMock.Setup(client => client.GetRefreshedAccessTokenAsync(It.IsAny<StringContent>()))
                .Returns(Task.FromResult(response));
            Repository repository = new EmployeeRepository(authServiceMock.Object, httpClientMock.Object);

            //Act
            bool result = repository.AddAuthenticationHeaderAsync().Result;

            //Assert
            httpClientMock.Verify(api => api.GetRefreshedAccessTokenAsync(It.IsAny<StringContent>()), Times.Once);
            Assert.IsNull(Repository._client.DefaultRequestHeaders.Authorization);
            Assert.IsFalse(result);
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

            httpClientMock.Setup(client => client.GetRefreshedAccessTokenAsync(It.IsAny<StringContent>())).Returns(Task.FromResult(response));

            Repository repository = new EmployeeRepository(authServiceMock.Object, httpClientMock.Object);

            //Act
            var result = repository.RequestRefreshAccessTokenAsync(refreshToken).Result as CustomTokenResponse;

            //Assert
            httpClientMock.Verify(api => api.GetRefreshedAccessTokenAsync(It.IsAny<StringContent>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.That(result.access_token, Is.EqualTo("newAccessToken"));
        }

        [Test]
        public void TestRefreshInvalidGoogleToken_ShouldReturn()
        {
            //Arrange
            string refreshToken = "testRefreshToken";
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest);

            httpClientMock.Setup(api => api.GetRefreshedAccessTokenAsync(It.IsAny<StringContent>())).Returns(Task.FromResult(response));
            Repository repository = new EmployeeRepository(authServiceMock.Object, httpClientMock.Object);

            //Act
            var result = repository.RequestRefreshAccessTokenAsync(refreshToken).Result;

            //Assert
            httpClientMock.Verify(api => api.GetRefreshedAccessTokenAsync(It.IsAny<StringContent>()), Times.Once);
            Assert.IsNull(result);
        }

      
    }
}
