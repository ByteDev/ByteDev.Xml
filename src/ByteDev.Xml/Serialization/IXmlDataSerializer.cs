using System.Text;

namespace ByteDev.Xml.Serialization
{
    /// <summary>
    /// Provides a way to serialize and deserialize objects.
    /// </summary>
    public interface IXmlDataSerializer
    {
        /// <summary>
        /// Serializes a object to a string.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <param name="encoding">Encoding type to use.</param>
        /// <returns>Serialized representation of <paramref name="obj" />.</returns>
        string Serialize(object obj, Encoding encoding = null);

        /// <summary>
        /// Deserialize a serialized representation to type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">Type to deserialize to.</typeparam>
        /// <param name="xml">Serialized string representation.</param>
        /// <returns>Deserialized type.</returns>
        T Deserialize<T>(string xml);
    }
}