using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ByteDev.Xml.Serialization
{
    internal class MyXmlSerializer : IXmlDataSerializer
    {
        public string Serialize(object obj, Encoding encoding = null)
        {
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

        public T Deserialize<T>(string xml)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            using (var sr = new StringReader(xml))
            {
                return (T)xmlSerializer.Deserialize(sr);
            }
        }
    }
}