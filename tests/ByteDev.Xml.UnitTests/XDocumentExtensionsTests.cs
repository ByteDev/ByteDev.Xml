using System;
using System.Xml.Linq;
using NUnit.Framework;

namespace ByteDev.Xml.UnitTests
{
    [TestFixture]
    public class XDocumentExtensionsTests
    {
        private static XDocument CreateSut(string xml)
        {
            var template = @"<?xml version=""1.0"" encoding=""utf-8""?>" + Environment.NewLine +
                           @"<AppRoot>{0}</AppRoot>";

            var text = string.Format(template, xml);

            return XDocument.Parse(text);
        }

        [TestFixture]
        public class IsRootName
        {
            [Test]
            public void WhenIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => XDocumentExtensions.IsRootName(null, "MyRoot"));
            }

            [TestCase(null)]
            [TestCase("")]
            public void WhenRootNameIsNullOrEmpty_ThenThrowException(string rootName)
            {
                var sut = CreateSut("<A>10</A>");

                Assert.Throws<ArgumentException>(() => sut.IsRootName(rootName));
            }

            [Test]
            public void WhenRootNameIsEqual_ThenReturnTrue()
            {
                var sut = CreateSut("<A>10</A>");

                var result = sut.IsRootName("AppRoot");

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenRootNameIsNotEqual_ThenReturnFalse()
            {
                var sut = CreateSut("<A>10</A>");

                var result = sut.IsRootName("NotAppRoot");

                Assert.That(result, Is.False);
            }
        }
    }
}