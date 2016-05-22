namespace MvcBoilerplate.Settings
{
    public class AppSettings
    {
        public string SiteTitle { get; set; }

        public string SiteDescription { get; set; }

        public string SiteTermsOfService { get; set; }

        public Contact Contact { get; set; }

        public License License { get; set; }
    }

    public class Contact
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Url { get; set; }
    }

    public class License
    {
        public string Name { get; set; }

        public string Url { get; set; }
    }
}