using System;
using System.Globalization;
using NUnit.Framework;
using UnleashedApp.Converters;

namespace UnleashedApp.Tests.Converters
{
    [TestFixture]
    public class DateConverterUnitTests
    {
        [Test]
        public void ConvertShouldReturnCorrectDateFormat()
        {
            //format of dd/MM/yyyy
            var converter = new DateConverter();
            var date = new DateTime(1952, 02, 08);

            var result = converter.Convert(date, typeof(DateTime), "", CultureInfo.CurrentCulture);

            Assert.AreEqual("08/02/1952", result.ToString());
        }

        [Test]
        public void ConvertShouldReturnValueIfFormattingFails()
        {
            //format of dd/MM/yyyy
            var converter = new DateConverter();
            var invalidDate = "ThisWontWork";

            var result = converter.Convert(invalidDate, typeof(string), "", CultureInfo.CurrentCulture);

            Assert.AreEqual(invalidDate, result);
        }

        [Test]
        public void ConvertBackShouldReturnSameValue()
        {
            //format of dd/MM/yyyy
            var converter = new DateConverter();
            var date = new DateTime(1952, 02, 08);

            var result = converter.ConvertBack(date, typeof(DateTime), "", CultureInfo.CurrentCulture);

            Assert.AreEqual(date, result);
        }
    }
}
