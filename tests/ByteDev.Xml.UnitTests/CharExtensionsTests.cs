using NUnit.Framework;

namespace ByteDev.Xml.UnitTests
{
    [TestFixture]
    public class CharExtensionsTests
    {
        [TestFixture]
        public class IsLegalXmlChar
        {
            [Test]
            public void WhenIsLegal_ThenReturnTrue()
            {
                var result = 'A'.IsLegalXmlChar();

                Assert.That(result, Is.True);
            }
        }
    }
}