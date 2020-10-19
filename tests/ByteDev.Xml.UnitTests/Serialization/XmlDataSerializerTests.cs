using System;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using ByteDev.Xml.Serialization;
using NUnit.Framework;

namespace ByteDev.Xml.UnitTests.Serialization
{
    [TestFixture]
    public class XmlDataSerializerTests
    {
        protected ProductXml[] GetProductXmls()
        {
            return new[] 
            {
                new ProductXml { Code = "A11", Name = "Apple"},
                new ProductXml { Code = "A12", Name = "Microsoft" },
                new ProductXml { Code = "A13", Name = "Google" }
            };
        }

        private static XmlDataSerializer CreateSut()
        {
            return new XmlDataSerializer();
        }

        private static string SerializeToXml(object obj)
        {
            return CreateSut().Serialize(obj);
        }

        [TestFixture]
        public class Serialize_XmlSerializer : XmlDataSerializerTests
        {
            [Test]
            public void WhenArgIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => CreateSut().Serialize(null));
            }

            [Test]
            public void WhenArgIsProduct_ThenSerializeToXml()
            {
                var product = new Product { Code = "code1", Name = "name1" };

                var result = CreateSut().Serialize(product);

                Assert.That(result.IsXml(), Is.True);
            }

            [Test]
            public void WhenArgIsProductXml_ThenSerializeToXml()
            {
                var product = new ProductXml { Code = "code1", Name = "name1" };

                var result = CreateSut().Serialize(product);

                Assert.That(result.IsXml(), Is.True);
            }

            [Test]
            public void WhenArgIsArrayOfObjects_ThenSerialize()
            {
                ProductXml[] products = GetProductXmls();

                var result = CreateSut().Serialize(products);

                Assert.That(result.IsXml(), Is.True);
            }

            [Test]
            public void WhenEncodingIsDefault_ThenSetEncodingToUtf16()  // (Unicode)
            {
                var product = new Product { Code = "code1", Name = "name1" };
                
                var result = CreateSut().Serialize(product);
                
                Assert.That(XDocument.Parse(result).GetEncoding(), Is.EqualTo(Encoding.Unicode));
            }

            [Test]
            public void WhenEncodingIsNotDefault_ThenSetEncoding()
            {
                var product = new Product { Code = "code1", Name = "name1" };
                
                var result = CreateSut().Serialize(product, Encoding.UTF8);

                Assert.That(result.IsXml(), Is.True);
                Assert.That(XDocument.Parse(result).GetEncoding(), Is.EqualTo(Encoding.UTF8));
            }
        }

        [TestFixture]
        public class Deserialize : XmlDataSerializerTests
        {
            [Test]
            public void WhenArgIsNull_ThenThrowException()
            {
                var result = CreateSut().Deserialize<ProductXml>(null);

                Assert.That(result, Is.EqualTo(default(ProductXml)));
            }

            [Test]
            public void WhenSerializedXmlIsCustomer_AndTryToDeserializeToProduct_ThenThrowException()
            {
                var customer = new Customer { Name = "John" };

                var xml = SerializeToXml(customer);

                Assert.Throws<InvalidOperationException>(() => CreateSut().Deserialize<Product>(xml));
            }

            [Test]
            public void WhenSerializedXmlIsProduct_ThenDeserialize()
            {
                var product = new Product { Code = "code1", Name = "name1" };

                var xml = SerializeToXml(product);

                var result = CreateSut().Deserialize<Product>(xml);

                Assert.That(result.Code, Is.EqualTo(product.Code));
                Assert.That(result.Name, Is.EqualTo(product.Name));
            }

            [Test]
            public void WhenSerializedXmlIsProductXml_ThenDeserialize()
            {
                ProductXml[] products = GetProductXmls();

                var xml = SerializeToXml(products);

                var result = CreateSut().Deserialize<ProductXml[]>(xml);

                Assert.That(result.Length, Is.EqualTo(products.Length));
            }

            [Test]
            public void WhenEncodingNotDefault_ThenDeserialize()
            {
                var product = new Product { Code = "code1", Name = "name1" };

                var sut = CreateSut();

                var xml = sut.Serialize(product, Encoding.UTF8);

                var result = sut.Deserialize<Product>(xml);

                Assert.That(result.Name, Is.EqualTo(product.Name));
                Assert.That(result.Code, Is.EqualTo(product.Code));
            }
        }
    }

    #region Test Types

    public class Customer
    {
        public string Name { get; set; }
    }

    public class Product
    {
        public string Code { get; set; }

        public string Name { get; set; }
    }

    [XmlRoot("product")]
    public class ProductXml
    {
        [XmlElement("code")]
        public string Code { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }
    }

    #endregion
}
