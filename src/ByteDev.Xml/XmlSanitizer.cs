using System.Linq;
using System.Xml;

namespace ByteDev.Xml
{
    public static class XmlSanitizer
    {
        /// <summary>
        /// Remove illegal XML characters from <paramref name="value" />.
        /// </summary>
        /// <param name="value">String to sanitize.</param>
        /// <returns>Sanitized string.</returns>
        public static string Sanitize(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return new string(value.Where(XmlConvert.IsXmlChar).ToArray());
        }
    }
}