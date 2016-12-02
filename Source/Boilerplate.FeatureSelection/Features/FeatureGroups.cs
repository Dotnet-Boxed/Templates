namespace Boilerplate.FeatureSelection.Features
{
    public static class FeatureGroups
    {
        public static readonly IFeatureGroup Hidden = new FeatureGroup("Hidden", 0);

        public static readonly IFeatureGroup Project = new FeatureGroup("Project", 1);

        public static readonly IFeatureGroup Hosting = new FeatureGroup("Hosting", 2);

        public static readonly IFeatureGroup CssAndJavaScript = new FeatureGroup("CSS & JavaScript", 3);

        public static readonly IFeatureGroup Rest = new FeatureGroup("REST", 4);

        public static readonly IFeatureGroup Performance = new FeatureGroup("Performance", 5);

        public static readonly IFeatureGroup Security = new FeatureGroup("Security", 6);

        public static readonly IFeatureGroup Monitoring = new FeatureGroup("Monitoring", 7);

        public static readonly IFeatureGroup Debugging = new FeatureGroup("Debugging", 8);

        public static readonly IFeatureGroup SearchEngineOptimization = new FeatureGroup("Search Engine Optimization (SEO)", 9);

        public static readonly IFeatureGroup Pages = new FeatureGroup("Pages", 10);

        public static readonly IFeatureGroup Formatters = new FeatureGroup("Formatters", 11);

        public static readonly IFeatureGroup Social = new FeatureGroup("Social", 12);

        public static readonly IFeatureGroup Favicons = new FeatureGroup("Favicons", 13);

        public static readonly IFeatureGroup Other = new FeatureGroup("Other", 14);
    }
}
