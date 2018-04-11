namespace ApiTemplate.Constants
{
    public static class ControllerName
    {
#if (HealthCheck)
        public const string Status = nameof(Status);
#endif
    }
}