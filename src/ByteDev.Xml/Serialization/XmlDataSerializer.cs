using System;
using System.Text;

namespace ByteDev.Xml.Serialization
{
    /// <summary>
    /// Represents a XML encoded string serializer.
    /// </summary>
    public class XmlDataSerializer : IXmlDataSerializer
    {
        private readonly XmlSerializerType _type;

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
        /// <exception cref="T:System.ArgumentException"><paramref name="type" /> is not valid.</exception>
        public XmlDataSerializer(XmlSerializerType type)
        {
            if (!Enum.IsDefined(typeof(XmlSerializerType), type))
                throw new ArgumentException("Serializer type is not valid.", nameof(type));

            _type = type;
        }

        /// <summary>
        /// Serializes a object to a XML string.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <param name="encoding">Encoding type to use. If null default of UTF-16 (Unicode) is used.</param>
        /// <returns>Serialized XML representation of <paramref name="obj" />.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="obj" /> is null.</exception>
        public string Serialize(object obj, Encoding encoding = null)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            if (encoding == null)
                encoding = Encoding.Unicode;

            switch (_type)
            {
                case XmlSerializerType.Xml:
                    return new MyXmlSerializer().Serialize(obj, encoding);

                case XmlSerializerType.DataContract:
                    return new MyDataContractSerializer().Serialize(obj, encoding);

                default:
                    throw new InvalidOperationException("Unhandled serializer type.");
            }
        }

        /// <summary>
        /// Deserialize a serialized XML representation to type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">Type to deserialize to.</typeparam>
        /// <param name="xml">Serialized XML string representation.</param>
        /// <returns>Deserialized type.</returns>
        public T Deserialize<T>(string xml)
        {
            if (string.IsNullOrEmpty(xml))
                return default;

            switch (_type)
            {
                case XmlSerializerType.Xml:
                    return new MyXmlSerializer().Deserialize<T>(xml);

                case XmlSerializerType.DataContract:
                    return new MyDataContractSerializer().Deserialize<T>(xml);

                default:
                    throw new InvalidOperationException("Unhandled serializer type.");
            }
        }
    }
}
