using System.Globalization;
using NUnit.Framework;
using UnleashedApp.Converters;

namespace UnleashedApp.Tests.Converters
{
    [TestFixture]
    public class CurrencyConverterUnitTests
    {
        [Test]
        public void ConvertShouldAddEurosignToTheStart()
        {
            var converter = new CurrencyConverter();
            var result = converter.Convert("500", typeof(string), "", CultureInfo.CurrentCulture);

            Assert.AreEqual("€500", result);
        }

        [Test]
        public void ConvertBackShouldRemoveEurosign()
        {
            var converter = new CurrencyConverter();
            var result = converter.ConvertBack("€500", typeof(string), "", CultureInfo.CurrentCulture);

            Assert.AreEqual("500", result);
        }
    }
}
