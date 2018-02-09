using System;
using Moq;
using NUnit.Framework;
using UnleashedApp.Services;
using Xamarin.Forms;

namespace UnleashedApp.Tests.ServiceTests
{
    [TestFixture]
    public class LegendGridServiceTests
    {
        [Test]
        [Repeat(10)]
        public void CreateLegendGridRowDefinitions_ShouldAddSpecifiedRowDefs()
        {
            var random = new Random();
            var amount = random.Next(50);
            var grid = new Mock<Grid>();

            LegendGridService.CreateLegendGridRowDefinitions(grid.Object, amount);

            //+2 because of extra margin on top and bottom
            Assert.AreEqual(amount + 2, grid.Object.RowDefinitions.Count);
        }

        [Test]
        public void CreateLegendGridColumnDefinitions_ShouldAddThreeColumnDefs()
        {
            var grid = new Mock<Grid>();

            LegendGridService.CreateLegendGridColumnDefinitions(grid.Object);

            Assert.AreEqual(3, grid.Object.ColumnDefinitions.Count);
        }
    }
}
