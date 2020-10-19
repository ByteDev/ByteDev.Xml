using System;
using System.IO;
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

            var xmlWriterSettings = new XmlWriterSettings
            {
                Encoding = encoding
            };

            using (var sw = new StringWriterWithEncoding(encoding))
            {
                using (var xmlWriter = XmlWriter.Create(sw, xmlWriterSettings))
                {
                    var xmlSerializer = new XmlSerializer(obj.GetType());

                    xmlSerializer.Serialize(xmlWriter, obj);
                }

                return sw.ToString();
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

            var xmlSerializer = new XmlSerializer(typeof(T));

            using (var sr = new StringReader(xml))
            {
                return (T)xmlSerializer.Deserialize(sr);
            }
        }
    }
}
