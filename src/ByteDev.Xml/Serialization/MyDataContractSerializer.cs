using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace ByteDev.Xml.Serialization
{
    internal class MyDataContractSerializer : IXmlDataSerializer
    {
        public string Serialize(object obj, Encoding encoding = null)
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

        public T Deserialize<T>(string xml)
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