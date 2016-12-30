namespace MvcBoilerplate.Constants
{
    public static class ControllerName
    {
        public const string Car = nameof(Car);
        public const string Home = nameof(Home);
#if (StatusController)
        public const string Status = nameof(Status);
#endif
    }
}