namespace ApiTemplate.Options
{
    public class CertificateOptions
    {
        public bool AllowInvalid { get; set; }

        public string Subject { get; set; }

        public string Store { get; set; }

        public string Location { get; set; }

        public string Path { get; set; }

        public string Password { get; set; }
    }
}
