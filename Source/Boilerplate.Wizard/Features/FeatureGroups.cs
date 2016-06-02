namespace Boilerplate.Wizard.Features
{
    public static class FeatureGroups
    {
        public static readonly IFeatureGroup Hidden = new FeatureGroup("Hidden", 0);

        public static readonly IFeatureGroup Project = new FeatureGroup("Project", 1);

        public static readonly IFeatureGroup CssAndJavaScript = new FeatureGroup("CSS & JavaScript", 2);

        public static readonly IFeatureGroup Rest = new FeatureGroup("REST", 3);

        public static readonly IFeatureGroup Performance = new FeatureGroup("Performance", 4);

        public static readonly IFeatureGroup Security = new FeatureGroup("Security", 5);

        public static readonly IFeatureGroup Monitoring = new FeatureGroup("Monitoring", 6);

        public static readonly IFeatureGroup Debugging = new FeatureGroup("Debugging", 7);

        public static readonly IFeatureGroup SearchEngineOptimization = new FeatureGroup("Search Engine Optimization (SEO)", 8);

        public static readonly IFeatureGroup Pages = new FeatureGroup("Pages", 9);

        public static readonly IFeatureGroup Formatters = new FeatureGroup("Formatters", 10);

        public static readonly IFeatureGroup Social = new FeatureGroup("Social", 11);

        public static readonly IFeatureGroup Favicons = new FeatureGroup("Favicons", 12);

        public static readonly IFeatureGroup Other = new FeatureGroup("Other", 13);
    }
}
