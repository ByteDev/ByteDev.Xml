using NUnit.Framework;

namespace ByteDev.Xml.UnitTests
{
    public static class XmlSanitizerTests
    {
        [TestFixture]
        public class SanitizeForXml
        {
            [Test]
            public void WhenIsNull_ThenReturnNull()
            {
                var result = XmlSanitizer.Sanitize(null);

                Assert.That(result, Is.Null);
            }

            [Test]
            public void WhenIsEmpty_ThenReturnEmpty()
            {
                var result = XmlSanitizer.Sanitize(string.Empty);

                Assert.That(result, Is.Empty);
            }

            [Test]
            public void WhenHasIllegalChars_ThenRemoveIllegalChars()
            {
                const string illegalChar = "\u0008";
                const string s = "this {0} that {0} this";

                var result = XmlSanitizer.Sanitize(s.FormatWith(illegalChar));

                Assert.That(result, Is.EqualTo(s.FormatWith("")));
            }
        }
    }
}