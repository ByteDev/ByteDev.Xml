using System;
using System.Linq;
using System.Xml.Linq;
using ByteDev.Collections;
using NUnit.Framework;

namespace ByteDev.Xml.UnitTests
{
    [TestFixture]
    public class XElementExtensionsTests
    {
        private const string Namespace = "http://schemas.microsoft.com/packaging/2011/08/nuspec.xsd";

        private static XElement CreateSut(string xml)
        {
            var template = @"<?xml version=""1.0"" encoding=""utf-8""?>" + Environment.NewLine +
                           @"<AppRoot xmlns=""{0}"">{1}</AppRoot>";

            var text = string.Format(template, Namespace, xml);

            return XDocument.Parse(text).Root;
        }

        [TestFixture]
        public class GetChildElements : XElementExtensionsTests
        {
            [Test]
            public void WhenSutIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => XElementExtensions.GetChildElements(null, "A"));
            }

            [TestCase(null)]
            [TestCase("")]
            public void WhenElementNameIsNullOrEmpty_ThenReturnEmpty(string elementName)
            {
                var sut = CreateSut("<A>1</A><A>2</A>");

                Assert.Throws<ArgumentException>(() => sut.GetChildElements(elementName));
            }

            [Test]
            public void WhenElementDoesNotExist_ThenReturnEmpty()
            {
                var sut = CreateSut(string.Empty);

                var result = sut.GetChildElements("A");
                
                Assert.That(result, Is.Empty);
            }

            [Test]
            public void WhenElementsExist_ThenReturnElements()
            {
                var sut = CreateSut("<A>1</A><A>2</A>");

                var result = sut.GetChildElements("A").ToList();

                Assert.That(result.Count, Is.EqualTo(2));
                Assert.That(result.First().Value, Is.EqualTo("1"));
                Assert.That(result.Second().Value, Is.EqualTo("2"));
            }

            [Test]
            public void WhenElementsExistInNamespace_ThenReturnElements()
            {
                var sut = CreateSut("<A>1</A><A>2</A>");

                var result = sut.GetChildElements("A", Namespace).ToList();

                Assert.That(result.Count, Is.EqualTo(2));
                Assert.That(result.First().Value, Is.EqualTo("1"));
                Assert.That(result.Second().Value, Is.EqualTo("2"));
            }

            [Test]
            public void WhenElementsAreNotDirectChildren_ThenReturnEmpty()
            {
                var sut = CreateSut("<XYZ><A>1</A><A>2</A></XYZ>");

                var result = sut.GetChildElements("A").ToList();

                Assert.That(result, Is.Empty);
            }
        }

        [TestFixture]
        public class GetChildElement : XElementExtensionsTests
        {
            [Test]
            public void WhenSutIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => XElementExtensions.GetChildElement(null, "A"));
            }

            [TestCase(null)]
            [TestCase("")]
            public void WhenElementNameIsNullOrEmpty_ThenReturnNull(string elementName)
            {
                var sut = CreateSut("<A>1</A><A>2</A>");

                Assert.Throws<ArgumentException>(() => sut.GetChildElement(elementName));
            }

            [Test]
            public void WhenElementDoesNotExist_ThenReturnNull()
            {
                var sut = CreateSut("<A>1</A><B>2</B>");

                var result = sut.GetChildElement("C");

                Assert.That(result, Is.Null);
            }

            [Test]
            public void WhenElementExists_ThenReturnElement()
            {
                var sut = CreateSut("<A>1</A><B>2</B>");

                var result = sut.GetChildElement("B");

                Assert.That(result.Value, Is.EqualTo("2"));
            }

            [Test]
            public void WhenElementsAreNotDirectChildren_ThenReturnNull()
            {
                var sut = CreateSut("<XYZ><A>1</A><B>2</B></XYZ>");

                var result = sut.GetChildElement("B");

                Assert.That(result, Is.Null);
            }
        }

        [TestFixture]
        public class GetChildElementValue : XElementExtensionsTests
        {
            [Test]
            public void WhenSutIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => XElementExtensions.GetChildElementValue(null, "A"));
            }

            [TestCase(null)]
            [TestCase("")]
            public void WhenElementNameIsNullOrEmpty_ThenReturnNull(string elementName)
            {
                var sut = CreateSut("<A>1</A><A>2</A>");

                Assert.Throws<ArgumentException>(() => sut.GetChildElementValue(elementName));
            }

            [Test]
            public void WhenElementDoesNotExist_ThenReturnNull()
            {
                var sut = CreateSut("<A>1</A><B>2</B>");

                var result = sut.GetChildElementValue("C");

                Assert.That(result, Is.Null);
            }

            [Test]
            public void WhenElementExists_ThenReturnValue()
            {
                var sut = CreateSut("<A>1</A><B>2</B>");

                var result = sut.GetChildElementValue("B");

                Assert.That(result, Is.EqualTo("2"));
            }
        }

        [TestFixture]
        public class GetChildElementValueT : XElementExtensionsTests
        {
            [Test]
            public void WhenElementExists_AndCanConvertType_ThenReturnValue()
            {
                var sut = CreateSut("<A>1</A><B>true</B>");

                var result = sut.GetChildElementValue<bool>("B");

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenElementExists_AndConvertType_ThenThrowException()
            {
                var sut = CreateSut("<A>1</A><B>SomeString</B>");

                Assert.Throws<FormatException>(() => sut.GetChildElementValue<int>("B"));
            }
        }

        [TestFixture]
        public class GetAttributeValue : XElementExtensionsTests
        {
            [Test]
            public void WhenSutIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => XElementExtensions.GetAttributeValue(null, "A"));
            }

            [TestCase(null)]
            [TestCase("")]
            public void WhenAttributeNameIsNullOrEmpty_ThenThrowException(string name)
            {
                var sut = CreateSut("<A>1</A><B>2</B>");

                var element = sut.GetChildElement("B");

                Assert.Throws<ArgumentException>(() => element.GetAttributeValue(name));
            }

            [Test]
            public void WhenAttributeDoesNotExist_ThenReturnNull()
            {
                var sut = CreateSut("<A>1</A><B>2</B>");

                var result = sut.GetChildElement("B").GetAttributeValue("type");

                Assert.That(result, Is.Null);
            }

            [Test]
            public void WhenAttributeExists_ThenReturnValue()
            {
                var sut = CreateSut(@"<A>1</A><B type=""int"">2</B>");

                var result = sut.GetChildElement("B").GetAttributeValue("type");

                Assert.That(result, Is.EqualTo("int"));
            }
        }

        [TestFixture]
        public class GetAttributeValueT : XElementExtensionsTests
        {
            [Test]
            public void WhenAttributeExists_AndCanConvertType_ThenReturnValue()
            {
                var sut = CreateSut(@"<A>1</A><B type=""100"">2</B>");

                var result = sut.GetChildElement("B").GetAttributeValue<int>("type");

                Assert.That(result, Is.EqualTo(100));
            }

            [Test]
            public void WhenAttributeExists_AndCannotConvertType_ThenReturnValue()
            {
                var sut = CreateSut(@"<A>1</A><B type=""100"">2</B>");

                Assert.Throws<FormatException>(() => sut.GetChildElement("B").GetAttributeValue<bool>("type"));
            }
        }

        [TestFixture]
        public class HasDescendants : XElementExtensionsTests
        {
            private const string DoesntExistElement = "Address";

            private static XElement CreateSut()
            {
                const string xml =
                    @"<Records>
                <Customer>
                    <Name>John</Name>
                    <Age>35</Age>
                </Customer>
                <Customer>
                    <Name>Peter</Name>
                </Customer>
            </Records>";

                return XDocument.Parse(xml).Descendants("Records").First();
            } 

            private XElement _sut;

            [SetUp]
            public void SetUp()
            {
                _sut = CreateSut();
            }

            [Test]
            public void WhenElementExists_ThenReturnTrue()
            {
                var result = _sut.HasDescendants("Name");

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenElementNotExists_ThenReturnFalse()
            {
                var result = _sut.HasDescendants(DoesntExistElement);

                Assert.That(result, Is.False);
            }            
        }
    }
}
