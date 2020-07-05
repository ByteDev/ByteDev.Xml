using System;
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
        /// Indicates whether element has descendents.
        /// </summary>
        /// <param name="source">XElement to perform the operation on.</param>
        /// <param name="elementName">Element name.</param>
        /// <returns>True if element has descendants; otherwise returns false.</returns>
        public static bool HasDescendants(this XElement source, string elementName)
        {
            return source.Descendants(elementName).Any();
        }

        /// <summary>
        /// Read the value of element <paramref name="elementName" />.
        /// </summary>
        /// <param name="source">XElement to perform the operation on.</param>
        /// <param name="elementName">Name of the element.</param>
        /// <returns>Element value.</returns>
        /// <exception cref="T:System.NotSupportedException"> element does not exist.</exception>
        public static string Read(this XElement source, string elementName)
        {
            var childElements = source.Descendants(elementName).ToList();

            if (!childElements.Any())
                throw new NotSupportedException($"Element '{elementName}' does not exist.");

            return childElements.First().Value;
        }

        /// <summary>
        /// Read the value of element <paramref name="elementName" />. If element does not exist empty string is returned.
        /// </summary>
        /// <param name="source">XElement to perform the operation on.</param>
        /// <param name="elementName">Name of the element.</param>
        /// <returns>Element value if element exists; otherwise returns empty.</returns>
        public static string SoftRead(this XElement source, string elementName)
        {
            var childElements = source.Descendants(elementName).ToList();

            return !childElements.Any() ? string.Empty : childElements.First().Value;
        }

        /// <summary>
        /// Retrieves a single element with name <paramref name="elementName" />.
        /// </summary>
        /// <param name="source">XElement to perform the operation on.</param>
        /// <param name="elementName">Name of the element.</param>
        /// <returns>Element with name <paramref name="elementName" />.</returns>
        /// <exception cref="T:System.NotSupportedException"> if no element or if more than one element exists with name <paramref name="elementName" />.</exception>
        public static XElement GetSingleElement(this XElement source, string elementName)
        {
            var childElements = source.Descendants(elementName).ToList();

            if (childElements.Count != 1)
                throw new NotSupportedException($"Found {childElements.Count} '{elementName}' elements in '{source}'.");

            return childElements.First();
        }
    }
}
