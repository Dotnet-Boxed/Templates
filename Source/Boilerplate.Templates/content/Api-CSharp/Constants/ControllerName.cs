namespace ApiTemplate.Constants
{
    public static class ControllerName
    {
        public const string Car = nameof(Car);
#if (Swagger)
        public const string Home = nameof(Home);
#endif
#if (HealthCheck)
        public const string Status = nameof(Status);
#endif
    }
}