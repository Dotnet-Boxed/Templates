namespace ApiTemplate.Constants
{
    public static class ControllerName
    {
        public const string Car = nameof(Car);
#if (HealthCheck)
        public const string Status = nameof(Status);
#endif
    }
}