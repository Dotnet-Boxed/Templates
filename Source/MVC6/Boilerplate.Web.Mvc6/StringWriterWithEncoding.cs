namespace Boilerplate.Web.Mvc
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// The <see cref="StringWriter" /> class always outputs UTF-16 encoded strings. To use a different encoding, we
    /// must inherit from <see cref="StringWriter" />. See
    /// http://stackoverflow.com/questions/9459184/why-is-the-xmlwriter-always-outputing-utf-16-encoding.
    /// </summary>
    public class StringWriterWithEncoding : StringWriter
    {
        private readonly Encoding encoding;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StringWriterWithEncoding"/> class.
        /// </summary>
        public StringWriterWithEncoding()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringWriterWithEncoding"/> class.
        /// </summary>
        /// <param name="formatProvider">An <see cref="T:System.IFormatProvider" /> object that controls formatting.</param>
        public StringWriterWithEncoding(IFormatProvider formatProvider)
            : base(formatProvider)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringWriterWithEncoding"/> class.
        /// </summary>
        /// <param name="stringBuilder">The string builder to write to.</param>
        public StringWriterWithEncoding(StringBuilder stringBuilder)
            : base(stringBuilder)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringWriterWithEncoding"/> class.
        /// </summary>
        /// <param name="stringBuilder">The string builder to write to.</param>
        /// <param name="formatProvider">The format provider.</param>
        public StringWriterWithEncoding(StringBuilder stringBuilder, IFormatProvider formatProvider)
            : base(stringBuilder, formatProvider)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringWriterWithEncoding"/> class.
        /// </summary>
        /// <param name="encoding">The encoding.</param>
        public StringWriterWithEncoding(Encoding encoding)
        {
            this.encoding = encoding;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringWriterWithEncoding"/> class.
        /// </summary>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="encoding">The encoding.</param>
        public StringWriterWithEncoding(IFormatProvider formatProvider, Encoding encoding)
            : base(formatProvider)
        {
            this.encoding = encoding;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringWriterWithEncoding"/> class.
        /// </summary>
        /// <param name="stringBuilder">The string builder to write to.</param>
        /// <param name="encoding">The encoding.</param>
        public StringWriterWithEncoding(StringBuilder stringBuilder, Encoding encoding)
            : base(stringBuilder)
        {
            this.encoding = encoding;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringWriterWithEncoding"/> class.
        /// </summary>
        /// <param name="stringBuilder">The string builder to write to.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="encoding">The encoding.</param>
        public StringWriterWithEncoding(StringBuilder stringBuilder, IFormatProvider formatProvider, Encoding encoding)
            : base(stringBuilder, formatProvider)
        {
            this.encoding = encoding;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the <see cref="T:System.Text.Encoding" /> in which the output is written.
        /// </summary>
        public override Encoding Encoding
        {
            get { return this.encoding == null ? base.Encoding : this.encoding; }
        }

        #endregion
    }
}