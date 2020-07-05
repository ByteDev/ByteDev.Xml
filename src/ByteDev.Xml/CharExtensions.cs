namespace ByteDev.Xml
{
    /// <summary>
    /// Extension methods for <see cref="T:System.Char" />.
    /// </summary>
    public static class CharExtensions
    {
        /// <summary>
        /// Indicates whether a given character is allowed by XML 1.0.
        /// </summary>
        /// <param name="source">Character to check.</param>
        /// <returns>True if the character is legal XML character; otherwise returns false.</returns>
        public static bool IsLegalXmlChar(this char source)
        {
            return
            (
                source == 0x9 /* == '\t' == 9   */          ||
                source == 0xA /* == '\n' == 10  */          ||
                source == 0xD /* == '\r' == 13  */          ||
                source >= 0x20 && source <= 0xD7FF ||
                source >= 0xE000 && source <= 0xFFFD ||
                source >= 0x10000 && source <= 0x10FFFF
            );
        }
    }
}