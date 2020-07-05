using System.Text;

namespace ByteDev.Xml
{
    /// <summary>
    /// Represents encoder of XML strings.
    /// </summary>
    public static class XmlEncoder
    {
        private static class XmlPredefinedEntities
        {
            public const string DoubleQuote = "&quot;";
            public const string Ampersand = "&amp;";
            public const string Apostraphe = "&apos;";
            public const string LessThan = "&lt;";
            public const string GreaterThan = "&gt;";
        }

        /// <summary>
        /// XML encode a string.
        /// </summary>
        /// <param name="value">The string to encode.</param>
        /// <returns>XML encoded string.</returns>
        public static string Encode(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            var builder = new StringBuilder();

            char[] originalChars = value.Trim().ToCharArray();

            foreach (var c in originalChars)
            {
                switch ((byte)c)
                {
                    case 34:
                        builder.Append(XmlPredefinedEntities.DoubleQuote);
                        break;
                    case 38:
                        builder.Append(XmlPredefinedEntities.Ampersand);
                        break;
                    case 39:
                        builder.Append(XmlPredefinedEntities.Apostraphe);
                        break;
                    case 60:
                        builder.Append(XmlPredefinedEntities.LessThan);
                        break;
                    case 62:
                        builder.Append(XmlPredefinedEntities.GreaterThan);
                        break;
                    default:
                        builder.Append(c);
                        break;
                }
            }

            return builder.ToString();
        }
    }
}
