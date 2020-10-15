using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

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
        public XmlDataSerializer(XmlSerializerType type)
        {
            _type = type;
        }

        /// <summary>
        /// Serializes a object to a XML string.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <param name="encoding">Encoding type to use.</param>
        /// <returns>Serialized XML representation of <paramref name="obj" />.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="obj" /> is null.</exception>
        public string Serialize(object obj, Encoding encoding = null)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            if (encoding == null)
                encoding = Encoding.UTF8;

            if (_type == XmlSerializerType.Xml)
            {
                var xmlWriterSettings = new XmlWriterSettings
                {
                    Encoding = encoding
                };

                using (var sw = new StringWriter())
                {
                    using (var xmlWriter = XmlWriter.Create(sw, xmlWriterSettings))
                    {
                        var xmlSerializer = new XmlSerializer(obj.GetType());

                        xmlSerializer.Serialize(xmlWriter, obj);
                    }

                    return sw.ToString();
                }
            }
            else
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var sr = new StreamReader(memoryStream))
                    {
                        var serializer = new DataContractSerializer(obj.GetType());

                        serializer.WriteObject(memoryStream, obj);
                        memoryStream.Position = 0;

                        return sr.ReadToEnd();
                    }
                }
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

            if (_type == XmlSerializerType.Xml)
            {
                var xmlSerializer = new XmlSerializer(typeof(T));

                using (var sr = new StringReader(xml))
                {
                    return (T)xmlSerializer.Deserialize(sr);
                }
            }
            else
            {
                using(Stream stream = new MemoryStream()) 
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(xml);
                    
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Position = 0;

                    DataContractSerializer deserializer = new DataContractSerializer(typeof(T));

                    return (T)deserializer.ReadObject(stream);
                }
            }
        }
    }
}
