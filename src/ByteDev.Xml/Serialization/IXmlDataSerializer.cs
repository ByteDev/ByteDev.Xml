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
        /// <returns>Serialized representation of <paramref name="obj" />.</returns>
        string Serialize(object obj);

        /// <summary>
        /// Deserialize a serialized XML representation to type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">Type to deserialize to.</typeparam>
        /// <param name="bytes">Serialized XML string representation.</param>
        /// <param name="encoding">Encoding type to use.</param>
        /// <returns>Deserialized type.</returns>
        T Deserialize<T>(byte[] bytes, Encoding encoding);

        /// <summary>
        /// Deserialize a serialized representation to type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">Type to deserialize to.</typeparam>
        /// <param name="value">Serialized string representation.</param>
        /// <param name="encoding">Encoding type used for the value.</param>
        /// <returns>Deserialized type.</returns>
        T Deserialize<T>(string value, Encoding encoding = null);
    }
}