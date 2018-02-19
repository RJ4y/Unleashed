using Moq;
using NUnit.Framework;
using UnleashedApp.Contracts;
using UnleashedApp.Repositories.AuthenticationRepositories;
using UnleashedApp.ViewModels;

namespace UnleashedApp.Tests.ViewModelTests.SplitViewTests
{
    [TestFixture]
    public class SplitViewModelTests
    {
        private Mock<IAuthenticationService> _authenticationServiceMock;
        private Mock<IAuthenticationRepository> _authenticationRepoMock;
        private Mock<INavigationService> _navigationMock;
        private SplitViewViewModel _splitViewViewModel;

        [SetUp]
        public void Setup()
        {
            _authenticationServiceMock = new Mock<IAuthenticationService>();
            _authenticationRepoMock = new Mock<IAuthenticationRepository>();
            _navigationMock = new Mock<INavigationService>();

            _splitViewViewModel = new SplitViewViewModel(_navigationMock.Object, _authenticationServiceMock.Object, _authenticationRepoMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _authenticationServiceMock = null;
            _authenticationRepoMock = null;
            _navigationMock = null;

            _splitViewViewModel = null;
        }

        [Test]
        public void ConstructorShouldInitCommands()
        {
            Assert.IsNotNull(_splitViewViewModel.GoFloorplanCommand);
            Assert.IsNotNull(_splitViewViewModel.GoHomeCommand);
            Assert.IsNotNull(_splitViewViewModel.GoLogoutCommand);
            Assert.IsNotNull(_splitViewViewModel.GoNameGameCommand);
            Assert.IsNotNull(_splitViewViewModel.GoWhoIsWhoCommand);
        }
    }
}
