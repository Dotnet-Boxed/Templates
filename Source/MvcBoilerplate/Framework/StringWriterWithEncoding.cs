namespace MvcBoilerplate.Framework
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// The <see cref="StringWriter"/> class always outputs UTF-16 encoded strings. 
    /// To use a different encoding, we must inherit from <see cref="StringWriter"/>.
    /// See http://stackoverflow.com/questions/9459184/why-is-the-xmlwriter-always-outputing-utf-16-encoding.
    /// </summary>
    public class StringWriterWithEncoding : StringWriter
    {
        private readonly Encoding encoding;

        public StringWriterWithEncoding()
        {
        }

        public StringWriterWithEncoding(IFormatProvider formatProvider)
            : base(formatProvider)
        {
        }

        public StringWriterWithEncoding(StringBuilder stringBuilder)
            : base(stringBuilder)
        {
        }

        public StringWriterWithEncoding(StringBuilder stringBuilder, IFormatProvider formatProvider)
            : base(stringBuilder, formatProvider)
        {
        }

        public StringWriterWithEncoding(Encoding encoding)
        {
            this.encoding = encoding;
        }

        public StringWriterWithEncoding(IFormatProvider formatProvider, Encoding encoding)
            : base(formatProvider)
        {
            this.encoding = encoding;
        }

        public StringWriterWithEncoding(StringBuilder sb, Encoding encoding)
            : base(sb)
        {
            this.encoding = encoding;
        }

        public StringWriterWithEncoding(StringBuilder sb, IFormatProvider formatProvider, Encoding encoding)
            : base(sb, formatProvider)
        {
            this.encoding = encoding;
        }

        public override Encoding Encoding
        {
            get { return this.encoding == null ? base.Encoding : this.encoding; }
        }
    }
}