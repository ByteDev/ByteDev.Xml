using System.IO;
using System.Text;

namespace ByteDev.Xml.Serialization
{
    internal sealed class StringWriterWithEncoding : StringWriter
    {
        public override Encoding Encoding { get; }

        public StringWriterWithEncoding(Encoding encoding)
        {
            Encoding = encoding;
        }    
    }
}