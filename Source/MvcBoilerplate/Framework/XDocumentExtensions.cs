namespace MvcBoilerplate.Framework
{
    using System.IO;
    using System.Text;
    using System.Xml.Linq;

    /// <summary>
    /// <see cref="XDocument"/> extension methods
    /// </summary>
    public static class XDocumentExtensions
    {
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents the XML document in the specified encoding.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents the XML document.
        /// </returns>
        public static string ToString(this XDocument document, Encoding encoding)
        {
            StringBuilder stringBuilder = new StringBuilder();

            using (StringWriter stringWriter = new StringWriterWithEncoding(stringBuilder, encoding))
            {
                document.Save(stringWriter);
            }

            return stringBuilder.ToString();
        }
    }
}