namespace Boilerplate.Wizard.Features
{
    public static class FeatureGroups
    {
        public static readonly IFeatureGroup Hidden = new FeatureGroup("Hidden", 0);

        public static readonly IFeatureGroup CssAndJavaScript = new FeatureGroup("CSS & JavaScript", 1); // MVC
        public static readonly IFeatureGroup Rest = new FeatureGroup("REST", 2);                         // API

        public static readonly IFeatureGroup Security = new FeatureGroup("Security", 3);

        public static readonly IFeatureGroup Formatters = new FeatureGroup("Formatters", 4);

        public static readonly IFeatureGroup Social = new FeatureGroup("Social", 5);

        public static readonly IFeatureGroup Favicons = new FeatureGroup("Favicons", 6);

        public static readonly IFeatureGroup Other = new FeatureGroup("Other", 7);
    }
}
