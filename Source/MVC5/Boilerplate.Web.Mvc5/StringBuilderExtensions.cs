namespace Boilerplate.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// <see cref="StringBuilder"/> extension methods.
    /// </summary>
    internal static class StringBuilderExtensions
    {
        /// <summary>
        /// Appends a <see cref="string"/> representing a meta tag with the specified property and content attribute value, only if the 
        /// <paramref name="content"/> is not <c>null</c>.
        /// </summary>
        /// <typeparam name="T">The type of the content.</typeparam>
        /// <param name="stringBuilder">The string builder.</param>
        /// <param name="property">The property name of the meta tag.</param>
        /// <param name="content">The content value of the meta tag.</param>
        public static void AppendMetaIfNotNull<T>(this StringBuilder stringBuilder, string property, T content)
        {
            if (content != null)
            {
                AppendMeta<T>(stringBuilder, property, content);
            }
        }

        /// <summary>
        /// Appends a <see cref="string" /> representing a meta tag with the specified property and <see cref="DateTime"/> content value, only if the 
        /// <paramref name="content"/> is not <c>null</c>. The content is in the format yyyy-MM-dd if no time component is specified, otherwise 
        /// yyyy-MM-ddTHH:mm:ssZ.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        /// <param name="property">The property name of the meta tag.</param>
        /// <param name="content">The content value of the meta tag.</param>
        public static void AppendMetaIfNotNull(this StringBuilder stringBuilder, string property, DateTime? content)
        {
            if (content != null)
            {
                AppendMeta(stringBuilder, property, content.Value);
            }
        }

        /// <summary>
        /// Appends a <see cref="string" /> representing multiple meta tags with the specified property and content values, only if the 
        /// <paramref name="content"/> is not <c>null</c>.
        /// </summary>
        /// <typeparam name="T">The type of the content.</typeparam>
        /// <param name="stringBuilder">The string builder.</param>
        /// <param name="property">The property name of the meta tag.</param>
        /// <param name="content">The collection of content values of the meta tag.</param>
        public static void AppendMetaIfNotNull<T>(this StringBuilder stringBuilder, string property, IEnumerable<T> content)
        {
            if (content != null)
            {
                AppendMeta(stringBuilder, property, content);
            }
        }

        /// <summary>
        /// Appends a <see cref="string"/> representing a meta tag with the specified property and content attribute value.
        /// </summary>
        /// <typeparam name="T">The type of the content.</typeparam>
        /// <param name="stringBuilder">The string builder.</param>
        /// <param name="property">The property name of the meta tag.</param>
        /// <param name="content">The content value of the meta tag.</param>
        public static void AppendMeta<T>(this StringBuilder stringBuilder, string property, T content)
        {
            stringBuilder.Append("<meta property=\"");
            stringBuilder.Append(property);
            stringBuilder.Append("\" content=\"");
            stringBuilder.Append(content);
            stringBuilder.AppendLine("\">");
        }

        /// <summary>
        /// Appends a <see cref="string" /> representing a meta tag with the specified property and <see cref="DateTime"/> content value.
        /// The content is in the format yyyy-MM-dd if no time component is specified, otherwise yyyy-MM-ddTHH:mm:ssZ.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        /// <param name="property">The property name of the meta tag.</param>
        /// <param name="content">The content value of the meta tag.</param>
        public static void AppendMeta(this StringBuilder stringBuilder, string property, DateTime content)
        {
            stringBuilder.Append("<meta property=\"");
            stringBuilder.Append(property);
            stringBuilder.Append("\" content=\"");
            if ((content.Hour == 0) && (content.Minute == 0) && (content.Second == 0))
            {
                stringBuilder.Append(content.ToString("yyyy-MM-dd"));
            }
            else
            {
                stringBuilder.Append(content.ToString("s") + "Z");
            }
            stringBuilder.AppendLine("\">");
        }

        /// <summary>
        /// Appends a <see cref="string" /> representing multiple meta tags with the specified property and content values.
        /// </summary>
        /// <typeparam name="T">The type of the content.</typeparam>
        /// <param name="stringBuilder">The string builder.</param>
        /// <param name="property">The property name of the meta tag.</param>
        /// <param name="content">The collection of content values of the meta tag.</param>
        public static void AppendMeta<T>(this StringBuilder stringBuilder, string property, IEnumerable<T> content)
        {
            foreach (T item in content)
            {
                stringBuilder.AppendMeta(property, item);
            }
        }
    }
}