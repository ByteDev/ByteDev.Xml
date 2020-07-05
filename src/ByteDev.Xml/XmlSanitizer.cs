using System.Text;

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

            var buffer = new StringBuilder(value.Length);

            foreach (char ch in value)
            {
                if (CharExtensions.IsLegalXmlChar(ch))
                    buffer.Append(ch);
            }

            return buffer.ToString();
        }
    }
}