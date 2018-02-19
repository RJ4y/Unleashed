using Moq;
using NUnit.Framework;
using UnleashedApp.Contracts;
using UnleashedApp.Repositories.AuthenticationRepositories;
using UnleashedApp.ViewModels;

namespace UnleashedApp.Tests.ViewModelTests
{
    [TestFixture]
    public class LoginViewModelTests
    {
        private LoginViewModel loginViewModel;

        [SetUp]
        public void Setup()
        {
            var authService = new Mock<IAuthenticationService>();
            var authRepository = new Mock<IAuthenticationRepository>();
            loginViewModel = new LoginViewModel(authService.Object, authRepository.Object);
        }

        [Test]
        public void TestConstructor_ShouldInitializeCommand()
        {
            Assert.IsNotNull(loginViewModel.PresentLoginScreenCommand);
        }
    }
}

