using System;

namespace ByteDev.Xml.UnitTests
{
    internal static class StringExtensions
    {
        public static string FormatWith(this string source, params object[] args)
        {
            if(source == null)
                throw new ArgumentNullException(nameof(source));
            
            return string.Format(source, args);
        }
    }
}