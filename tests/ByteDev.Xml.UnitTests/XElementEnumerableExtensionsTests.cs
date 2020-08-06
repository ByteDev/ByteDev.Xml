using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ByteDev.Collections;
using NUnit.Framework;

namespace ByteDev.Xml.UnitTests
{
    [TestFixture]
    public class XElementEnumerableExtensionsTests
    {
        private static readonly XNamespace Namespace = "http://localhost/nstest";
        private static readonly XNamespace NamespacePrefix = "nstest";
        private static readonly XNamespace WrongNamespacePrefix = "notnstest";

        private static IEnumerable<XElement> CreateSutWithNs()
        {
            XElement element = new XElement("container",
                new XAttribute(XNamespace.Xmlns + "nstest", Namespace),
                new XElement(NamespacePrefix + "NotContact"),
                new XElement(NamespacePrefix + "Contact",
                    new XElement(NamespacePrefix + "Name"),
                    new XElement(NamespacePrefix + "Surname")),
                new XElement(NamespacePrefix + "Contact",
                    new XElement(NamespacePrefix + "Name")));
            
            return element.Elements();
        }

        private static IEnumerable<XElement> CreateSut()
        {
            XElement element = new XElement("container",
                new XElement("NotContact"),
                new XElement("Contact",
                    new XElement("Name"),
                    new XElement("Surname")),
                new XElement("Contact",
                    new XElement("Name")));
            
            return element.Elements();
        }

        [TestFixture]
        public class GetChildElement
        {
            [Test]
            public void WhenSutIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => XElementEnumerableExtensions.GetChildElement(null, "A"));
            }

            [TestCase(null)]
            [TestCase("")]
            public void WhenElementNameIsNullOrEmpty_ThenReturnEmpty(string elementName)
            {
                var sut = CreateSut();

                Assert.Throws<ArgumentException>(() => sut.GetChildElement(elementName));
            }

            [Test]
            public void WhenElementDoesNotExist_ThenReturnNull()
            {
                var sut = CreateSut();

                var result = sut.GetChildElement("A");
                
                Assert.That(result, Is.Null);
            }

            [Test]
            public void WhenSingleChildElementExists_ThenReturnElement()
            {
                var sut = CreateSut();

                var result = sut.GetChildElement("Surname");

                Assert.That(result.Name.LocalName, Is.EqualTo("Surname"));
            }

            [Test]
            public void WhenMultipleChildElementsExist_ThenThrowException()
            {
                var sut = CreateSut();

                var ex = Assert.Throws<InvalidOperationException>(() => sut.GetChildElement("Name"));
                Assert.That(ex.Message, Is.EqualTo("Sequence contains more than one matching element"));
            }

            [Test]
            public void WhenElementExists_WithNamespace_ThenReturnElement()
            {
                var sut = CreateSutWithNs();

                var result = sut.GetChildElement("Surname", NamespacePrefix);

                Assert.That(result.Name.LocalName, Is.EqualTo("Surname"));
            }

            [Test]
            public void WhenElementExists_WithWrongNs_ThenReturnNull()
            {
                var sut = CreateSutWithNs();

                var result = sut.GetChildElement("Surname", WrongNamespacePrefix);
                
                Assert.That(result, Is.Null);
            }
        }

        [TestFixture]
        public class GetChildElements
        {
            [Test]
            public void WhenSutIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => XElementEnumerableExtensions.GetChildElements(null, "A"));
            }

            [TestCase(null)]
            [TestCase("")]
            public void WhenElementNameIsNullOrEmpty_ThenReturnEmpty(string elementName)
            {
                var sut = CreateSut();

                Assert.Throws<ArgumentException>(() => sut.GetChildElements(elementName));
            }

            [Test]
            public void WhenElementsDoNotExist_ThenReturnEmpty()
            {
                var sut = CreateSut();

                var result = sut.GetChildElements("A");
                
                Assert.That(result, Is.Empty);
            }

            [Test]
            public void WhenSingleChildElementExists_ThenReturnElement()
            {
                var sut = CreateSut();

                var result = sut.GetChildElements("Surname");

                Assert.That(result.Single().Name.LocalName, Is.EqualTo("Surname"));
            }

            [Test]
            public void WhenMultipleChildElementExists_ThenReturnElements()
            {
                var sut = CreateSut();

                var result = sut.GetChildElements("Name").ToList();

                Assert.That(result.Count, Is.EqualTo(2));
                Assert.That(result.First().Name.LocalName, Is.EqualTo("Name"));
                Assert.That(result.Second().Name.LocalName, Is.EqualTo("Name"));
            }

            [Test]
            public void WhenMultipleElementsExist_WithNamespace_ThenReturnElements()
            {
                var sut = CreateSutWithNs();

                var result = sut.GetChildElements("Name", NamespacePrefix).ToList();

                Assert.That(result.Count, Is.EqualTo(2));
                Assert.That(result.First().Name.LocalName, Is.EqualTo("Name"));
                Assert.That(result.Second().Name.LocalName, Is.EqualTo("Name"));
            }

            [Test]
            public void WhenMultipleElementExists_WithWrongNs_ThenReturnEmpty()
            {
                var sut = CreateSutWithNs();

                var result = sut.GetChildElements("Name", WrongNamespacePrefix);
                
                Assert.That(result, Is.Empty);
            }
        }
    }
}