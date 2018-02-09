using Moq;
using NUnit.Framework;
using UnleashedApp.Contracts;
using UnleashedApp.Repositories.EmployeeRepositories;
using UnleashedApp.ViewModels;


namespace UnleashedApp.Tests.ViewModelTests.RoomTests
{
    [TestFixture()]
    public class RoomUnitTests
    {
        private Mock<INavigationService> _navigationMock;
        private Mock<IEmployeeRepository> _employeeRepoMock;
        private RoomViewModel _roomViewModel;

        [SetUp]
        public void Setup()
        {
            _navigationMock = new Mock<INavigationService>();
            _employeeRepoMock = new Mock<IEmployeeRepository>();
            _roomViewModel = new RoomViewModel(_navigationMock.Object, _employeeRepoMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _navigationMock = null;
            _employeeRepoMock = null;
            _roomViewModel = null;
        }

        [Test]
        public void ConstructorShouldInitCommand()
        {
            Assert.IsNotNull(_roomViewModel.EmployeeDetailCommand);
        }

        [Test]
        public void ConstructorShouldInitEmployeeRepo()
        {
            Assert.IsNotNull(_roomViewModel.EmployeeRepository);
            Assert.AreEqual(_employeeRepoMock.Object, _roomViewModel.EmployeeRepository);
        }
    }
}
