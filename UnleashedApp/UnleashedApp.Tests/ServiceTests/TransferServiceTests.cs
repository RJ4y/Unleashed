using Xamarin.Forms;
using NUnit.Framework;
using UnleashedApp.Models;
using UnleashedApp.Services;

namespace UnleashedApp.Tests.ServiceTests
{
    [TestFixture]
    public class TransferServiceTests
    {
        private SpaceBuilder _spaceBuilder;

        [SetUp]
        public void Setup()
        {
            _spaceBuilder = new SpaceBuilder();
        }

        [TearDown]
        public void TearDown()
        {
            _spaceBuilder = null;
        }

        [Test]
        public void GetSelectedRoomShouldReturnPassedRoom()
        {
            var spaceList = _spaceBuilder.Init(10, 0, 0);
            var room = new Room(2, "naam", Color.Red, Room.RoomType.Workspace );

            TransferService.Store(room, spaceList);
            var result = TransferService.GetSelectedRoom();

            Assert.AreEqual(room ,result);
        }

        [Test]
        public void GetSelectedSpacesShouldReturnSpacesOfRoom()
        {
            var spaceList = _spaceBuilder.Init(10, 0, 0, 1);
            var room = new Room(0, "naam", Color.Red, Room.RoomType.Workspace);

            TransferService.Store(room, spaceList);
            var result = TransferService.GetSelectedSpaces();

            var onlyMatchingSpaces = true;

            foreach (var space in result)
            {
                if (space.RoomId != room.Id)
                {
                    onlyMatchingSpaces = false;
                }
            }

            Assert.IsTrue(result.Count < spaceList.Count);
            Assert.IsTrue(onlyMatchingSpaces);
        }
    }
}
