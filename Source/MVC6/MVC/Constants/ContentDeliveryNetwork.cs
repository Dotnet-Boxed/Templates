namespace MvcBoilerplate.Constants
{
    public static class ContentDeliveryNetwork
    {
        public static class Google
        {
            public const string Domain = "ajax.googleapis.com";
            public const string JQueryUrl = "https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js";
        }

        public static class MaxCdn
        {
            public const string Domain = "maxcdn.bootstrapcdn.com";
            public const string FontAwesomeUrl = "https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css";
        }

        public static class Microsoft
        {
            public const string Domain = "ajax.aspnetcdn.com";
            public const string JQueryValidateUrl = "https://ajax.aspnetcdn.com/ajax/jquery.validate/1.16.0/jquery.validate.min.js";
            public const string JQueryValidateUnobtrusiveUrl = "https://ajax.aspnetcdn.com/ajax/mvc/5.2.3/jquery.validate.unobtrusive.min.js";
            public const string BootstrapUrl = "https://ajax.aspnetcdn.com/ajax/bootstrap/3.3.7/bootstrap.min.js";
        }
    }
}