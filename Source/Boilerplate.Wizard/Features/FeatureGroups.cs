namespace Boilerplate.Wizard.Features
{
    public static class FeatureGroups
    {
        public static readonly IFeatureGroup Hidden = new FeatureGroup("Hidden", 0);

        public static readonly IFeatureGroup CssAndJavaScript = new FeatureGroup("CSS & JavaScript", 1);

        public static readonly IFeatureGroup Rest = new FeatureGroup("REST", 2);

        public static readonly IFeatureGroup Performance = new FeatureGroup("Performance", 3);

        public static readonly IFeatureGroup Security = new FeatureGroup("Security", 4);

        public static readonly IFeatureGroup SearchEngineOptimization = new FeatureGroup("Search Engine Optimization (SEO)", 5);

        public static readonly IFeatureGroup Pages = new FeatureGroup("Pages", 6);

        public static readonly IFeatureGroup Formatters = new FeatureGroup("Formatters", 7);

        public static readonly IFeatureGroup Social = new FeatureGroup("Social", 8);

        public static readonly IFeatureGroup Favicons = new FeatureGroup("Favicons", 9);

        public static readonly IFeatureGroup Other = new FeatureGroup("Other", 10);
    }
}
