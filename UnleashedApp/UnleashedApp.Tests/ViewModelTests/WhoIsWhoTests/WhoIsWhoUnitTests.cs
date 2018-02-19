using System.Collections.Generic;
using System.Collections.ObjectModel;
using Moq;
using NUnit.Framework;
using UnleashedApp.Contracts;
using UnleashedApp.Models;
using UnleashedApp.Repositories.HabitatRepositories;
using UnleashedApp.Repositories.SquadRepositories;
using UnleashedApp.ViewModels;

namespace UnleashedApp.Tests.ViewModelTests.WhoIsWhoTests
{
    [TestFixture]
    public class WhoIsWhoUnitTests
    {
        private Mock<INavigationService> _navigationServiceMock;
        private Mock<IHabitatRepository> _habitatRepositoryMock;
        private Mock<ISquadRepository> _squadRepositoryMock;
        private WhoIsWhoViewModel _whoIsWhoViewModel;
        private HabitatBuilder _habitatBuilder;
        private SquadBuilder _squadBuilder;

        [SetUp]
        public void Setup()
        {
            _navigationServiceMock = new Mock<INavigationService>();
            _habitatRepositoryMock = new Mock<IHabitatRepository>();
            _squadRepositoryMock = new Mock<ISquadRepository>();
            _habitatBuilder = new HabitatBuilder();
            _squadBuilder = new SquadBuilder();

            _habitatRepositoryMock.Setup(x => x.GetAllHabitats()).Returns(_habitatBuilder.InitHabitats);
            _habitatRepositoryMock.Setup(y => y.GetEmployees(1)).Returns(_habitatBuilder.InitEmployees);
            _squadRepositoryMock.Setup(z => z.GetAllSquads()).Returns(_squadBuilder.InitSquads);
            _squadRepositoryMock.Setup(a => a.GetEmployees(1)).Returns(_habitatBuilder.InitEmployees);

            _whoIsWhoViewModel = new WhoIsWhoViewModel(_habitatRepositoryMock.Object, _squadRepositoryMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _navigationServiceMock = null;
            _habitatRepositoryMock = null;
            _squadRepositoryMock = null;
            _habitatBuilder = null;
            _squadBuilder = null;
            _whoIsWhoViewModel = null;
        }

        [Test]
        public void ConstructorShouldInitialiseCommands()
        {
            Assert.IsNotNull(_whoIsWhoViewModel.HabitatEmployeeDetailCommand);
            Assert.IsNotNull(_whoIsWhoViewModel.SquadEmployeeDetailCommand);
        }

        [Test]
        public void LoadEmployeesPerHabitatShouldInitCollection()
        {
            _whoIsWhoViewModel.LoadEmployeesPerHabitat();

            var result = _whoIsWhoViewModel.GetEmployeesPerHabitat();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(ObservableCollection<Group>), result);
        }

        [Test]
        public void LoadEmployeesPerSquadShouldInitCollection()
        {
            _whoIsWhoViewModel.LoadEmployeesPerSquad();

            var result = _whoIsWhoViewModel.GetEmployeesPerSquad();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(ObservableCollection<Group>), result);
        }

        [Test]
        public void AddingFilterShouldInitFilteredSquadList()
        {
            _whoIsWhoViewModel.LoadEmployeesPerSquad();

            _whoIsWhoViewModel.Filter = "";

            Assert.IsNotNull(_whoIsWhoViewModel.FilteredSquadList);
        }

        [Test]
        public void AddingFilterShouldInitFilteredHabitatList()
        {
            _whoIsWhoViewModel.LoadEmployeesPerHabitat();

            _whoIsWhoViewModel.Filter = "";

            Assert.IsNotNull(_whoIsWhoViewModel.FilteredHabitatList);
        }

        [Test]
        public void CallingFilterShouldFilterList()
        {
            _whoIsWhoViewModel.LoadEmployeesPerSquad();
            _whoIsWhoViewModel.LoadEmployeesPerHabitat();

            _whoIsWhoViewModel.Filter = "Jan";

            //var group = new Group()
            //{
            //    Id = 1,
            //    Name = "Care"
            //};

            //var employee = new Employee
            //{
            //    Id = 3,
            //    FirstName = "Jan",
            //    LastName = "Janssen",
            //    HabitatId = 2
            //};

            //group.Add(employee);

            //var expected = new ObservableCollection<Group>
            //{
            //    group
            //};

            Assert.AreEqual(1, _whoIsWhoViewModel.FilteredHabitatList.Count);
            Assert.AreEqual(1, _whoIsWhoViewModel.FilteredSquadList.Count);
        }

        public class HabitatBuilder
        {
            public List<Habitat> InitHabitats()
            {
                var list = new List<Habitat>
                {
                    new Habitat
                    {
                        Id = 1,
                        Name = "Care"
                    },
                    new Habitat
                    {
                        Id = 2,
                        Name = "Customer"
                    },
                    new Habitat
                    {
                        Id = 3,
                        Name = "Dev"
                    },
                    new Habitat
                    {
                        Id = 4,
                        Name = "Admin"
                    },
                };

                return list;
            }

            public List<Employee> InitEmployees()
            {
                var list = new List<Employee>
                {
                    new Employee
                    {
                        Id = 1,
                        FirstName = "Fons",
                        LastName = "Lambrechts",
                        HabitatId = 1
                    },
                    new Employee
                    {
                        Id = 2,
                        FirstName = "Fons",
                        LastName = "Lambrechts",
                        HabitatId = 1
                    },
                    new Employee
                    {
                        Id = 3,
                        FirstName = "Jan",
                        LastName = "Janssen",
                        HabitatId = 2
                    },
                    new Employee
                    {
                        Id = 4,
                        FirstName = "Fons",
                        LastName = "Lambrechts",
                        HabitatId = 2
                    },
                    new Employee
                    {
                        Id = 5,
                        FirstName = "Fons",
                        LastName = "Lambrechts",
                        HabitatId = 3
                    }
                };

                return list;
            }
        }

        public class SquadBuilder
        {
            public List<Squad> InitSquads()
            {
                var list = new List<Squad>
                {
                    new Squad()
                    {
                        Id = 1,
                        Name = "Best Team"
                    },
                    new Squad()
                    {
                        Id = 2,
                        Name = "Doolittle"
                    },
                    new Squad()
                    {
                        Id = 3,
                        Name = "SurferRosa"
                    },
                    new Squad()
                    {
                        Id = 4,
                        Name = "Unleashed"
                    },
                };

                return list;
            }
        }
    }
}
