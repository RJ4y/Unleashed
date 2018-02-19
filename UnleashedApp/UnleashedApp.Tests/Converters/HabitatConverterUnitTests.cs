using System.Globalization;
using NUnit.Framework;
using UnleashedApp.Converters;
using UnleashedApp.Models;

namespace UnleashedApp.Tests.Converters
{
    [TestFixture]
    public class HabitatConverterUnitTests
    {
        [Test]
        public void ConvertBackShouldReturnId()
        {
            var converter = new HabitatConverter();

            var habitat = new Habitat
            {
                Id = 1,
                Name = "Habitat"
            };

            var result = converter.ConvertBack(habitat, typeof(int), "", CultureInfo.CurrentCulture);

            Assert.AreEqual(1, result);
        }
    }
}
