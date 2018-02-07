using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using UnleashedApp.Authentication;
using UnleashedApp.Contracts;
using UnleashedApp.Contracts.ViewModels;
using UnleashedApp.Repositories;
using UnleashedApp.Repositories.AuthenticationRepositories;
using UnleashedApp.ViewModels;

namespace UnleashedApp.Tests.Views
{
    [TestFixture]
    public class LogOutTests
    {
        private Mock<IAuthenticationRepository> authenticationRepository;
        private Mock<IAuthenticationService> authenticationService;

        [SetUp]
        public void Setup()
        {
            authenticationRepository = new Mock<IAuthenticationRepository>();
            authenticationService = new Mock<IAuthenticationService>();
        }

        //[Test]
        //public void TestLogout_ShouldDeleteTokens()
        //{
        //    //Arrange
        //    authenticationRepository.Setup(repo => repo.RequestRevokeTokens()).Returns(Task.FromResult(true));
        //    authenticationService.Setup(s => s.DeleteAccessTokens());

        //    MenuViewModel menuViewModel= new MenuViewModel(new NavigationService(), authenticationService.Object, authenticationRepository.Object);
           
        //    //Act
        //    menuViewModel.LogOutCommand.Execute(null);

        //    //Assert

        //}
    }
}
