namespace MvcBoilerplate.Settings
{
    using System.Collections.Generic;
    using Microsoft.AspNet.Mvc;

    public class CacheProfileSettings
    {
        public Dictionary<string, CacheProfile> CacheProfiles { get; set; }
    }
}
