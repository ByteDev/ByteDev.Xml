using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ByteDev.Xml
{
    /// <summary>
    /// Extension methods for <see cref="T:System.Collections.Generic.IEnumerable`1" />.
    /// </summary>
    public static class XElementEnumerableExtensions
    {
        /// <summary>
        /// Retrieves direct child element based on it's name.
        /// </summary>
        /// <param name="source">XElement collection to perform the operation on.</param>
        /// <param name="elementName">Element name.</param>
        /// <param name="ns">Element namespace.</param>
        /// <returns>Element if found; otherwise returns null.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="elementName" /> is null or empty.</exception>
        public static XElement GetChildElement(this IEnumerable<XElement> source, string elementName, XNamespace ns = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (string.IsNullOrEmpty(elementName))
                throw new ArgumentException("Element name was null or empty.", nameof(elementName));

            XName xName = ns == null ? elementName : ns + elementName;

            return source
                .SingleOrDefault(e => e.Element(xName) != null)?
                .Element(xName);
        }

        /// <summary>
        /// Retrieves all direct child elements with a particular name.
        /// </summary>
        /// <param name="source">XElement to perform the operation on.</param>
        /// <param name="elementName">Element name.</param>
        /// <param name="ns">Element namespace.</param>
        /// <returns>Collection of elements.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="elementName" /> is null or empty.</exception>
        public static IEnumerable<XElement> GetChildElements(this IEnumerable<XElement> source, string elementName, XNamespace ns = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (string.IsNullOrEmpty(elementName))
                throw new ArgumentException("Element name was null or empty.", nameof(elementName));

            XName xName = ns == null ? elementName : ns + elementName;

            return source
                .Where(e => e.Element(xName) != null)
                .Elements(xName);
        }
    }
}