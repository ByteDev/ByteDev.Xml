using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ByteDev.Xml
{
    /// <summary>
    /// Extension methods for <see cref="T:System.Xml.Linq.XElement" />.
    /// </summary>
    public static class XElementExtensions
    {
        /// <summary>
        /// Retrieves all direct child elements with a particular name.
        /// </summary>
        /// <param name="source">XElement to perform the operation on.</param>
        /// <param name="elementName">Element name.</param>
        /// <param name="ns">Element namespace.</param>
        /// <returns>Collection of elements.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="elementName" /> is null or empty.</exception>
        public static IEnumerable<XElement> GetChildElements(this XElement source, string elementName, XNamespace ns = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (string.IsNullOrEmpty(elementName))
                throw new ArgumentException("Element name was null or empty.", nameof(elementName));

            if (ns == null)
                return source.Elements().Where(e => e.Name.LocalName == elementName);

            return source.Elements(ns + elementName);
        }

        /// <summary>
        /// Retrieves direct child element based on it's name.
        /// </summary>
        /// <param name="source">XElement to perform the operation on.</param>
        /// <param name="elementName">Element name.</param>
        /// <param name="ns">Element namespace.</param>
        /// <returns>Element if found; otherwise returns null.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="elementName" /> is null or empty.</exception>
        public static XElement GetChildElement(this XElement source, string elementName, XNamespace ns = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (string.IsNullOrEmpty(elementName))
                throw new ArgumentException("Element name was null or empty.", nameof(elementName));

            if (ns == null)
                return source.Elements().SingleOrDefault(e => e.Name.LocalName == elementName);

            return source.Element(ns + elementName);
        }

        /// <summary>
        /// Retrieves a direct child element's value as a string.
        /// </summary>
        /// <param name="source">XElement to perform the operation on.</param>
        /// <param name="elementName">Element name.</param>
        /// <param name="ns">Element namespace.</param>
        /// <returns>Element value if found; otherwise null.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="elementName" /> is null or empty.</exception>
        public static string GetChildElementValue(this XElement source, string elementName, XNamespace ns = null)
        {
            return GetChildElement(source, elementName, ns)?.Value;
        }

        /// <summary>
        /// Retrieves a direct child element's value converted to a given type.
        /// </summary>
        /// <typeparam name="TValue">Type to convert value to.</typeparam>
        /// <param name="source">XElement to perform the operation on.</param>
        /// <param name="elementName">Element name.</param>
        /// <param name="ns">Element namespace.</param>
        /// <returns>Element value if found; otherwise null.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="elementName" /> is null or empty.</exception>
        /// <exception cref="T:System.FormatException">Cannot convert to given type.</exception>
        public static TValue GetChildElementValue<TValue>(this XElement source, string elementName, XNamespace ns = null)
        {
            var value = GetChildElementValue(source, elementName, ns);

            return (TValue) Convert.ChangeType(value, typeof(TValue));
        }

        /// <summary>
        /// Retrieves an element's specific attribute value.
        /// </summary>
        /// <param name="source">XElement to perform the operation on.</param>
        /// <param name="attributeName">Attribute name.</param>
        /// <returns>Element attribute value if exists; otherwise null.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="attributeName" /> is null or empty.</exception>
        public static string GetAttributeValue(this XElement source, string attributeName)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (string.IsNullOrEmpty(attributeName))
                throw new ArgumentException("Attribute name was null or empty.", nameof(attributeName));

            return source.Attribute(attributeName)?.Value;
        }

        /// <summary>
        /// Retrieves an element's specific attribute value converted to a given type.
        /// </summary>
        /// <typeparam name="TValue">Type to convert value to.</typeparam>
        /// <param name="source">XElement to perform the operation on.</param>
        /// <param name="attributeName">Attribute name.</param>
        /// <returns>Element attribute value if exists; otherwise null.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="attributeName" /> is null or empty.</exception>
        /// <exception cref="T:System.FormatException">Cannot convert to given type.</exception>
        public static TValue GetAttributeValue<TValue>(this XElement source, string attributeName)
        {
            var value = GetAttributeValue(source, attributeName);

            return (TValue) Convert.ChangeType(value, typeof(TValue));
        }

        /// <summary>
        /// Indicates whether element has descendents.
        /// </summary>
        /// <param name="source">XElement to perform the operation on.</param>
        /// <param name="elementName">Element name.</param>
        /// <returns>True if element has descendants; otherwise returns false.</returns>
        public static bool HasDescendants(this XElement source, string elementName)
        {
            return source.Descendants(elementName).Any();
        }
    }
}
