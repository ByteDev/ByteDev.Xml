using System;
using System.Xml.Linq;

namespace ByteDev.Xml
{
    /// <summary>
    /// Extension methods for <see cref="T:System.Xml.Linq.XDocument" />.
    /// </summary>
    public static class XDocumentExtensions
    {
        /// <summary>
        /// Determines if the documents root element's name is equal to <paramref name="rootName" />.
        /// </summary>
        /// <param name="source">XDocument to perform the operation on.</param>
        /// <param name="rootName">Root element name.</param>
        /// <returns>True if root element name is equal; otherwise false.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="rootName" /> is null or empty.</exception>
        public static bool IsRootName(this XDocument source, string rootName)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (string.IsNullOrEmpty(rootName))
                throw new ArgumentException("Root name was null or empty.", nameof(rootName));

            return source.Root?.Name.LocalName == rootName;
        }
    }
}