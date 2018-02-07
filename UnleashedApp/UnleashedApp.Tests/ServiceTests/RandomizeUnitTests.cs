using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnleashedApp.Services;

namespace UnleashedApp.Tests.ServiceTests
{
    [TestFixture]
    public class RandomizeUnitTests
    {
        private List<int> _list;

        [SetUp]
        public void Setup()
        {
            _list = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
        }

        [TearDown]
        public void TearDown()
        {
            _list = null;
        }

        [Test]
        public void GetSpecifiedAmountOfRandomObjectsFromList_ShouldReturnNullIfAmountIsGreaterThanListCount()
        {
            var result = RandomizeService.GetSpecifiedAmountOfRandomObjectsFromList(_list, 20);

            Assert.IsNull(result);
        }

        [Test]
        public void GetSpecifiedAmountOfRandomObjectsFromList_ShouldReturnAmountSpecified()
        {
            var random = new Random();
            var amount = random.Next(10);
            var result = RandomizeService.GetSpecifiedAmountOfRandomObjectsFromList(_list, amount);

            Assert.AreEqual(amount, result.Count);
        }

        [Test]
        public void GetRandomObjectFromList_ShouldReturnOneObject()
        {
            var result = RandomizeService.GetRandomObjectFromList(_list);

            Assert.IsNotNull(result);
            Assert.IsNotInstanceOf<List>(result);
        }

        [Test]
        [Repeat(10)]
        public void GetRandomGender_ShouldReturn_M_or_F()
        {
            char result = RandomizeService.GetRandomGender();

            Assert.IsTrue(result.Equals('M') || result.Equals('F'));
        }
    }
}
