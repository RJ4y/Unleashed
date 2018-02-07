using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnleashedApp.Models;
using UnleashedApp.Services;

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
            var result = GridService.GetDifferenceAsDimension(spaceList, true);

            Assert.AreEqual(amount - minY, result.X);
            Assert.AreEqual(amount - minX, result.Y);
        }
    }

    public class SpaceBuilder
    {
        public List<Space> Init(int amount, int minX, int minY)
        {
            var list = new List<Space>();

            for (var i = minX; i < amount; i++)
            {
                for (var j = minY; j < amount; j++)
                {
                    var space = new Space(i, j, 0, 0);
                    list.Add(space);
                }
            }

            return list;
        }
    }
}
