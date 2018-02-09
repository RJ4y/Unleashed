using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using UnleashedApp.Models;
using UnleashedApp.Services;
using Xamarin.Forms;

namespace UnleashedApp.Tests.ServiceTests
{
    [TestFixture]
    public class GridServiceUnitTests
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
        [Repeat(5)]
        public void GetGridTranslationShouldReturnMinimumCoordinates()
        {
            var random = new Random();
            var minX = random.Next(10);
            var minY = random.Next(10);

            var spaceList = _spaceBuilder.Init(10, minX, minY);

            var result = GridService.GetGridTranslation(spaceList);

            Assert.AreEqual(minX, result.X);
            Assert.AreEqual(minY, result.Y);
        }

        [Test]
        public void GetMinifiedSquareGridDimensionsShouldFormSquare()
        {
            var random = new Random();
            var amount = random.Next(50);
            var minX = random.Next(amount);
            var minY = random.Next(amount);

            var spaceList = _spaceBuilder.Init(amount, minX, minY);

            var result = GridService.GetMinifiedSquareGridDimensions(spaceList);

            minX = amount - minX + 1;
            minY = amount - minY + 1;
            var biggest = minX > minY ? minX : minY;

            Assert.AreEqual(biggest, result.X);
            Assert.AreEqual(biggest, result.Y);
        }

        [Test]
        public void GetMinifiedSquareGridDimensionsShouldFormSquareIfXIsBiggerThanY()
        {
            var random = new Random();
            var amount = random.Next(50);
            var minX = random.Next(amount);
            var minY = random.Next(minX);

            var spaceList = _spaceBuilder.Init(amount, minX, minY);

            var result = GridService.GetMinifiedSquareGridDimensions(spaceList);

            minX = amount - minX + 1;
            minY = amount - minY + 1;
            var biggest = minX > minY ? minX : minY;

            Assert.AreEqual(biggest, result.X);
            Assert.AreEqual(biggest, result.Y);
        }

        [Test]
        [Repeat(5)]
        public void GetDifferenceAsDimension_ShouldReturnCorrectDimensions()
        {
            var random = new Random();

            var amount = random.Next(50);
            var minX = random.Next(amount);
            var minY = random.Next(amount);
            var spaceList = _spaceBuilder.Init(amount, minX, minY);
            var result = GridService.GetDifferenceAsDimension(spaceList, false);

            Assert.AreEqual(amount - minX, result.X);
            Assert.AreEqual(amount - minY, result.Y);
        }

        [Test]
        [Repeat(5)]
        public void GetDifferenceAsDimension_ShouldReturnCorrectInvertedDimensions()
        {
            var random = new Random();

            var amount = random.Next(50);
            var minX = random.Next(amount);
            var minY = random.Next(amount);
            var spaceList = _spaceBuilder.Init(amount, minX, minY);
            var result = GridService.GetDifferenceAsDimension(spaceList);

            Assert.AreEqual(amount - minY, result.X);
            Assert.AreEqual(amount - minX, result.Y);
        }

        [Test]
        public void CreateGridRowDefinitions_ShouldAdd_DimensionYAmountOfRowDefs()
        {
            var grid = new Mock<Grid>();
            var random = new Random();
            var randomY = random.Next(40);

            var dimension = new Dimensions(2, randomY);

            GridService.CreateGridRowDefinitions(grid.Object, dimension);

            Assert.AreEqual(randomY, grid.Object.RowDefinitions.Count);
        }

        [Test]
        public void CreateGridColumnDefinitions_ShouldAdd_DimensionXAmountOfRowDefs()
        {
            var grid = new Mock<Grid>();
            var random = new Random();
            var randomX = random.Next(40);

            var dimension = new Dimensions(randomX, 2);

            GridService.CreateGridColumnDefinitions(grid.Object, dimension);

            Assert.AreEqual(randomX, grid.Object.ColumnDefinitions.Count);
        }

        [Test]
        public void AddColorLabelShouldAddLabelToGrid()
        {
            var grid = new Mock<Grid>();
            var random = new Random();
            var randomX = random.Next(40);
            var randomY = random.Next(40);

            var count = grid.Object.Children.Count;
            GridService.AddColorLabel(grid.Object, randomX, randomY, Color.Red);

            Assert.AreEqual(count + 1, grid.Object.Children.Count);
        }

        [Test]
        public void AddColorLabelShouldAddLabelToGridIfFalse()
        {
            var grid = new Mock<Grid>();
            var random = new Random();
            var randomX = random.Next(40);
            var randomY = random.Next(40);

            var count = grid.Object.Children.Count;
            GridService.AddColorLabel(grid.Object, randomX, randomY, Color.Red, false);

            Assert.AreEqual(count + 1, grid.Object.Children.Count);
        }

        [Test]
        public void AddTextLabelShouldAddLabelToGrid()
        {
            var grid = new Mock<Grid>();
            var random = new Random();
            var randomX = random.Next(40);
            var randomY = random.Next(40);

            var count = grid.Object.Children.Count;
            GridService.AddTextLabel(grid.Object, randomX, randomY, "test");

            Assert.AreEqual(count + 1, grid.Object.Children.Count);
        }

        [Test]
        public void AddTextLabelShouldAddLabelToGridIfFalse()
        {
            var grid = new Mock<Grid>();
            var random = new Random();
            var randomX = random.Next(40);
            var randomY = random.Next(40);

            var count = grid.Object.Children.Count;
            GridService.AddTextLabel(grid.Object, randomX, randomY, "test", false);

            Assert.AreEqual(count + 1, grid.Object.Children.Count);
        }
    }

    public class SpaceBuilder
    {
        public List<Space> Init(int amount, int minX, int minY, int roomId = 0)
        {
            var random = new Random();
            var list = new List<Space>();
            var id = roomId;

            for (var i = minX; i < amount; i++)
            {
                for (var j = minY; j < amount; j++)
                {
                    if (roomId != 0)
                    {
                        id = random.Next(2);
                    }

                    var space = new Space(i, j, 0, id);
                    list.Add(space);
                }
            }

            return list;
        }
    }
}
