using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using UnleashedApp.Models;
using UnleashedApp.Repositories.EmployeeRepositories;
using UnleashedApp.ViewModels;

namespace UnleashedApp.Tests.ViewModelTests.NameGameTests
{
    [TestFixture]
    public class NameGameUnitTests
    {
        private NameGameViewModel _nameGameViewModel;
        private Mock<IEmployeeRepository> _employeeRepoMock;

        [Test]
        public void ConstructorShouldInitListOfEmployees()
        {
            var list = new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    FirstName = "Fons"
                },
                new Employee
                {
                    Id = 2,
                    FirstName = "Jan"
                },
                new Employee
                {
                    Id = 3,
                    FirstName = "Karel"
                }
            };

            _employeeRepoMock = new Mock<IEmployeeRepository>();
            _employeeRepoMock.Setup(x => x.GetAllEmployees()).Returns(list);

            _nameGameViewModel = new NameGameViewModel(_employeeRepoMock.Object);

            Assert.IsNotNull(_nameGameViewModel.Employees);
            Assert.AreEqual(list, _nameGameViewModel.Employees);
        }
    }
}
