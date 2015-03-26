namespace MvcBoilerplate.Constants
{
    public static class ErrorControllerRoute
    {
        public const string GetBadRequest = ControllerName.Error + "GetBadRequest";
        public const string GetInternalServerError = ControllerName.Error + "GetInternalServerError";
        public const string GetNotFound = ControllerName.Error + "GetNotFound";
        public const string GetUnauthorized = ControllerName.Error + "Unauthorized";
    }
}