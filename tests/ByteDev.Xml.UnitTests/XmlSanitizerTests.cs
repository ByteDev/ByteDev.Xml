using NUnit.Framework;

namespace ByteDev.Xml.UnitTests
{
    public static class XmlSanitizerTests
    {
        [TestFixture]
        public class Sanitize
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
                char illegalChar = '\0';

                string s = $"this {illegalChar} that {illegalChar} this";

                var result = XmlSanitizer.Sanitize(s);

                Assert.That(result, Is.EqualTo("this  that  this"));
            }
        }
    }
}