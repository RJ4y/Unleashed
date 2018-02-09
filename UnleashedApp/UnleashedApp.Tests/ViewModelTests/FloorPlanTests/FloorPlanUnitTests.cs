using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using UnleashedApp.Contracts;
using UnleashedApp.Models;
using UnleashedApp.Repositories.RoomRepositories;
using UnleashedApp.Repositories.SpaceRepositories;
using UnleashedApp.Tests.ServiceTests;
using UnleashedApp.ViewModels;
using Xamarin.Forms;

namespace UnleashedApp.Tests.ViewModelTests.FloorPlanTests
{
    [TestFixture]
    public class FloorPlanUnitTests
    {
        private Mock<INavigationService> _navigationServiceMock;
        private Mock<ISpaceRepository> _spaceRepositoryMock;
        private Mock<IRoomRepository> _roomRepositoryMock;
        private FloorplanViewModel _floorplanViewModel;
        private SpaceBuilder _spaceBuilder;
        private List<Space> _expectedSpaces;
        private List<Room> _expectedRooms;

        [SetUp]
        public void Setup()
        {
            _spaceBuilder = new SpaceBuilder();
            _navigationServiceMock = new Mock<INavigationService>();
            _spaceRepositoryMock = new Mock<ISpaceRepository>();
            _roomRepositoryMock = new Mock<IRoomRepository>();

            _expectedSpaces = _spaceBuilder.Init(5, 0, 0);
            _expectedRooms = new List<Room>
            {
                new Room(1, "naam", Color.Red, Room.RoomType.Workspace),
                new Room(2, "naam", Color.Blue, Room.RoomType.Kitchen),
                new Room(3, "naam", Color.Black, Room.RoomType.Empty),
                new Room(4, "naam", Color.Green, Room.RoomType.Workspace),
                new Room(5, "naam", Color.Yellow, Room.RoomType.Workspace),
            };

            _spaceRepositoryMock.Setup(x => x.GetAllSpaces()).Returns(_expectedSpaces);
            _roomRepositoryMock.Setup(x => x.GetAllRooms()).Returns(_expectedRooms);
        }

        [TearDown]
        public void TearDown()
        {
            _spaceBuilder = null;
            _navigationServiceMock = null;
            _spaceRepositoryMock = null;
            _roomRepositoryMock = null;
        }


        [Test]
        public void ConstructorShouldInitCommand()
        {
            _floorplanViewModel = new FloorplanViewModel(_navigationServiceMock.Object, _spaceRepositoryMock.Object, _roomRepositoryMock.Object);

            Assert.IsNotNull(_floorplanViewModel.RoomCommand);
        }

        [Test]
        public void ContstuctorShouldInitSpaces()
        {
            _floorplanViewModel = new FloorplanViewModel(_navigationServiceMock.Object, _spaceRepositoryMock.Object, _roomRepositoryMock.Object);

            Assert.AreEqual(_expectedSpaces, _floorplanViewModel.Spaces);
        }

        [Test]
        public void ContstuctorShouldInitRooms()
        {
            _floorplanViewModel = new FloorplanViewModel(_navigationServiceMock.Object, _spaceRepositoryMock.Object, _roomRepositoryMock.Object);

            Assert.AreEqual(_expectedRooms, _floorplanViewModel.Rooms);
        }
    }
}
