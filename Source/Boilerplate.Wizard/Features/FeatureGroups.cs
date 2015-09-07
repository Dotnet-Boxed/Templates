namespace Boilerplate.Wizard.Features
{
    public static class FeatureGroups
    {
        public static readonly IFeatureGroup CssAndJavaScript = new FeatureGroup("CSS & JavaScript", 1);

        public static readonly IFeatureGroup Formatters = new FeatureGroup("Formatters", 2);

        public static readonly IFeatureGroup Security = new FeatureGroup("Security", 3);

        public static readonly IFeatureGroup Other = new FeatureGroup("Other", 4);
    }
}
