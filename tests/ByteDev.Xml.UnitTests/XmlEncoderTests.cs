using NUnit.Framework;

namespace ByteDev.Xml.UnitTests
{
    [TestFixture]
    public class XmlEncoderTests
    {
        [TestFixture]
        public class Encode
        {
            private const string Text = "Using {0} special entity";

            [Test]
            public void WhenIsNull_ThenReturnNull()
            {
                var result = XmlEncoder.Encode(null);

                Assert.That(result, Is.Null);
            }

            [Test]
            public void WhenIsEmpty_ThenReturnEmpty()
            {
                var result = XmlEncoder.Encode(string.Empty);

                Assert.That(result, Is.Empty);
            }

            [Test]
            public void WhenContainsDoubleQuote_ThenEncodeToEntity()
            {
                var result = XmlEncoder.Encode(Text.FormatWith("\""));

                Assert.That(result, Is.EqualTo(Text.FormatWith("&quot;")));
            }

            [Test]
            public void WhenContainsAmpersand_ThenEncodeToEntity()
            {
                var result = XmlEncoder.Encode(Text.FormatWith("&"));

                Assert.That(result, Is.EqualTo(Text.FormatWith("&amp;")));
            }

            [Test]
            public void WhenContainsApostraphe_ThenEncodeToEntity()
            {
                var result = XmlEncoder.Encode(Text.FormatWith("'"));

                Assert.That(result, Is.EqualTo(Text.FormatWith("&apos;")));
            }

            [Test]
            public void WhenContainsLessThan_ThenEncodeToEntity()
            {
                var result = XmlEncoder.Encode(Text.FormatWith("<"));

                Assert.That(result, Is.EqualTo(Text.FormatWith("&lt;")));
            }

            [Test]
            public void WhenContainsGreaterThan_ThenEncodeToEntity()
            {
                var result = XmlEncoder.Encode(Text.FormatWith(">"));

                Assert.That(result, Is.EqualTo(Text.FormatWith("&gt;")));
            }
        }
    }
}