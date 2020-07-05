using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Schema;

namespace ByteDev.Xml
{
    public static class StringExtensions
    {
        public static bool IsXml(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return false;

            var settings = new XmlReaderSettings
            {
                CheckCharacters = true,
                ConformanceLevel = ConformanceLevel.Document,
                DtdProcessing = DtdProcessing.Ignore,
                IgnoreComments = true,
                IgnoreProcessingInstructions = true,
                IgnoreWhitespace = true,
                ValidationFlags = XmlSchemaValidationFlags.None,
                ValidationType = ValidationType.None,
            };

            using (var reader = XmlReader.Create((TextReader) new StringReader(source), (XmlReaderSettings) settings))
            {
                try
                {
                    while (reader.Read()) { }
                    return true;
                }
                catch (XmlException)
                {
                    return false;
                }
            }
        }

        public static bool ContainsOnlyXmlChars(this string source)
        {
            return source.All(XmlConvert.IsXmlChar);
        }
    }
}