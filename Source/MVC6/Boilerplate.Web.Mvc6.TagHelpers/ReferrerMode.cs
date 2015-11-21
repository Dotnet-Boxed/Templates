namespace Boilerplate.Web.Mvc.TagHelpers
{
    /// <summary>
    /// Controls what is sent in the HTTP referrer header when a client navigates from your page to an external site.
    /// 
    /// Privacy
    /// A social networking site has a profile page for each of its users, and users add hyperlinks from their profile 
    /// page to their favourite bands. The social networking site might not wish to leak the user's profile URL to the 
    /// band web sites when other users follow those hyperlinks (because the profile URLs might reveal the identity of 
    /// the owner of the profile). Some social networking sites, however, might wish to inform the band web sites that 
    /// the links originated from the social networking site but not reveal which specific user's profile contained the 
    /// links.
    /// 
    /// Security
    /// A web application uses HTTPS and a URL-based session identifier. The web application might wish to link to 
    /// HTTPS resources on other web sites without leaking the user's session identifier in the URL. Alternatively, a 
    /// web application may use URLs which themselves grant some capability. Controlling the referrer can help prevent 
    /// these capability URLs from leaking via referrer headers.
    /// 
    /// See http://www.w3.org/TR/referrer-policy/
    /// </summary>
    public enum ReferrerMode
    {
        /// <summary>
        /// No referrer information is to be sent along with requests made from a particular global environment to any 
        /// origin. The header will be omitted entirely.
        /// </summary>
        /// <example>
        /// If a document at https://example.com/page.html sets a policy of None, then navigations to 
        /// https://example.com/ (or any other URL) would send no referrer header.
        /// </example>
        None,

        /// <summary>
        /// Sends a full URL along with requests from TLS-protected global environments to a non-a priori insecure 
        /// origin, and requests from global environments which are not TLS-protected to any origin.
        /// Requests from TLS-protected global environments to a priori insecure origins, on the other hand, will 
        /// contain no referrer information. A referrer will not be sent. This is a user agent's default behaviour, if 
        /// no policy is otherwise specified.
        /// </summary>
        /// <example>
        /// If a document at https://example.com/page.html sets a policy of None When Downgrade, then navigations to 
        /// https://not.example.com/ would send a referrer HTTP header with a value of https://example.com/page.html, 
        /// as neither resource's origin is an a priori insecure origin.
        /// Navigations from that same page to http://not.example.com/ would send no referrer header.
        /// </example>
        NoneWhenDowngrade,

        /// <summary>
        /// Specifies that only the ASCII serialization of the origin of the global environment from which a request is 
        /// made is sent as referrer information when making both same-origin requests and cross-origin requests from a 
        /// particular global environment.
        /// </summary>
        /// <example>
        /// If a document at https://example.com/page.html sets a policy of Origin Only, then navigations to any origin 
        /// would send a referrer header with a value of https://example.com/, even to a priori insecure origins. 
        /// </example>
        Origin,

        /// <summary>
        /// Specifies that a full URL, stripped for use as a referrer, is sent as referrer information when making 
        /// same-origin requests from a particular global environment, and only the ASCII serialization of the origin 
        /// of the global environment from which a request is made is sent at referrer information when making 
        /// cross-origin requests from a particular global environment.
        /// </summary>
        /// <remarks>
        /// For the Origin When Cross-Origin policy, we also consider protocol upgrades, e.g. requests from 
        /// http://exmaple.com/ to https://example.com/ to be cross-origin requests.
        /// </remarks>
        /// <example>
        /// If a document at https://example.com/page.html sets a policy of Origin When Cross-Origin, then navigations 
        /// to any https://example.com/not-page.html would send a referrer header with a value of 
        /// https://example.com/page.html.
        /// Navigations from that same page to https://not.example.com/ would send a referrer header with a value of 
        /// https://example.com/, even to a priori insecure origins.
        /// </example>
        OriginWhenCrossOrigin,

        /// <summary>
        /// Specifies that a full URL, stripped for use as a referrer, is sent along with both cross-origin requests 
        /// and same-origin requests made from a particular global environment.
        /// </summary>
        /// <remarks>
        /// The policy's name doesn't lie; it is unsafe. This policy will leak origins and paths from TLS-protected 
        /// resources to insecure origins. Carefully consider the impact of setting such a policy for potentially 
        /// sensitive documents.
        /// </remarks>
        /// <example>
        /// If a document at https://example.com/sekrit.html sets a policy of Unsafe URL, then navigations to 
        /// http://not.example.com/ (and every other origin) would send a referrer HTTP header with a value of 
        /// https://example.com/sekrit.html. 
        /// </example>
        UnsafeUrl
    }
}
