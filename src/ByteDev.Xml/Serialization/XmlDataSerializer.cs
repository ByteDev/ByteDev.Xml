using System;
using System.IO;
using System.Text;
using System.Xml;

namespace ByteDev.Xml.Serialization
{
    /// <summary>
    /// Represents a XML encoded string serializer.
    /// </summary>
    public class XmlDataSerializer : IXmlDataSerializer
    {
        private readonly XmlSerializerAdaptor _xmlSerializerAdaptor;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Xml.Serialization.XmlDataSerializer" /> class
        /// using <see cref="T:ByteDev.Xml.Serialization.XmlSerializerType.Xml" />.
        /// </summary>
        public XmlDataSerializer() : this(XmlSerializerType.Xml)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Xml.Serialization.XmlDataSerializer" /> class.
        /// </summary>
        /// <param name="type">Type of serializer to use.</param>
        public XmlDataSerializer(XmlSerializerType type)
        {
            _xmlSerializerAdaptor = new XmlSerializerAdaptor(type);
        }

        /// <summary>
        /// Serializes a object to a XML string.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <returns>Serialized XML representation of <paramref name="obj" />.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="obj" /> is null.</exception>
        public string Serialize(object obj)
        {
            if(obj == null)
                throw new ArgumentNullException(nameof(obj));

            using (var memoryStream = new MemoryStream())
            {
                using (var reader = new StreamReader(memoryStream))
                {
                    _xmlSerializerAdaptor.Serialize(obj, memoryStream);

                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Deserialize a serialized XML representation to type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">Type to deserialize to.</typeparam>
        /// <param name="value">Serialized XML string representation.</param>
        /// <param name="encoding">Encoding type used for the value.</param>
        /// <returns>Deserialized type.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="value" /> is null.</exception>
        public T Deserialize<T>(string value, Encoding encoding = null)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (encoding == null)
                encoding = Encoding.UTF8;

            var buffer = encoding.GetBytes(value);

            return Deserialize<T>(buffer, Encoding.UTF8);
        }

        /// <summary>
        /// Deserialize a serialized XML representation to type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">Type to deserialize to.</typeparam>
        /// <param name="bytes">Serialized XML string representation.</param>
        /// <param name="encoding">Encoding type to use.</param>
        /// <returns>Deserialized type.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="bytes" /> is null.</exception>
        public T Deserialize<T>(byte[] bytes, Encoding encoding)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            using (var memoryStream = new MemoryStream(bytes))
            {
                var reader = XmlDictionaryReader.CreateTextReader(memoryStream, encoding, new XmlDictionaryReaderQuotas(), null);

                return _xmlSerializerAdaptor.Deserialize<T>(reader);
            }
        }
    }
}
