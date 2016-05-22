namespace Boilerplate.AspNetCore
{
    /// <summary>
    /// A list of internet media types, which are a standard identifier used on the Internet to indicate the type of
    /// data that a file contains. Web browsers use them to determine how to display, output or handle files and search
    /// engines use them to classify data files on the web.
    /// </summary>
    public static class ContentType
    {
        /// <summary>Atom feeds.</summary>
        public const string Atom = "application/atom+xml";

        /// <summary>HTML; Defined in RFC 2854.</summary>
        public const string Html = "text/html";

        /// <summary>GIF image; Defined in RFC 2045 and RFC 2046.</summary>
        public const string Gif = "image/gif";

        /// <summary>JPEG JFIF image; Defined in RFC 2045 and RFC 2046.</summary>
        public const string Jpg = "image/jpeg";

        /// <summary>Binary JavaScript Object Notation BSON.</summary>
        public const string Bson = "application/bson";

        /// <summary>JavaScript Object Notation JSON; Defined in RFC 4627.</summary>
        public const string Json = "application/json";

        /// <summary>Portable Network Graphics; Registered,[8] Defined in RFC 2083.</summary>
        public const string Png = "image/png";

        /// <summary>Textual data; Defined in RFC 2046 and RFC 3676.</summary>
        public const string Text = "text/plain";

        /// <summary>Extensible Markup Language; Defined in RFC 3023</summary>
        public const string Xml = "application/xml";
    }
}
