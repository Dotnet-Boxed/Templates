namespace Boilerplate.Wizard.ViewModels
{
    using System.ComponentModel;
    using System.Windows.Data;
    using Boilerplate.Wizard.Features;
    using Boilerplate.Wizard.Services;
    using Framework.UI.Input;

    public class DesignMainViewModel : IMainViewModel
    {
        private readonly FeatureCollection features;
        private readonly IProjectService projectService;
        private readonly ICollectionView featuresView;

        public DesignMainViewModel()
        {
            this.projectService = new ProjectService(new FileSystemService(), @"C:\");
            this.features = new FeatureCollection()
            {
                new FrontEndFrameworkFeature(this.projectService),
                new TypeScriptFeature(this.projectService),
                new JavaScriptCodeStyleFeature(this.projectService),
                new JavaScriptTestFrameworkFeature(this.projectService),

                new JsonSerializerSettingsFeature(this.projectService),
                new BsonFormatterFeature(this.projectService, FeatureSet.Mvc6),
                new XmlFormatterFeature(this.projectService, FeatureSet.Mvc6),

                new HttpsEverywhereFeature(this.projectService),

                new AboutPageFeature(this.projectService),
                new ContactPageFeature(this.projectService),

                new AppleIOSFaviconsFeature(this.projectService),
                new AppleMacSafariFaviconFeature(this.projectService),
                new AndroidChromeM36ToM38FaviconFeature(this.projectService),
                new AndroidChromeM39FaviconsFeature(this.projectService),
                new GoogleTvFaviconFeature(this.projectService),
                new Windows8IE10FaviconFeature(this.projectService),
                new Windows81IE11EdgeFaviconFeature(this.projectService),
                new WebAppCapableFeature(this.projectService),

                new FeedFeature(this.projectService),
                new SearchFeature(this.projectService),
                new SitemapFeature(this.projectService),
                new RobotsTextFeature(this.projectService),
                new HumansTextFeature(this.projectService)
            };
            this.featuresView = CollectionViewSource.GetDefaultView(this.features);
            this.featuresView.Filter = x => ((IFeature)x).IsVisible;
            this.featuresView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(IFeature.Group) + "." + nameof(IFeatureGroup.Name)));
            this.featuresView.SortDescriptions.Add(new SortDescription(nameof(IFeature.Group) + "." + nameof(IFeatureGroup.Order), ListSortDirection.Ascending));
            this.featuresView.SortDescriptions.Add(new SortDescription(nameof(IFeature.Order), ListSortDirection.Ascending));
        }

        public AsyncDelegateCommand AddOrRemoveFeaturesCommand
        {
            get { return null; }
        }

        public string Error
        {
            get { return null; }
        }

        public FeatureCollection Features
        {
            get { return this.features; }
        }

        public ICollectionView FeaturesView
        {
            get { return this.featuresView; }
        }

        public bool IsLoaded
        {
            get { return true; }
        }
    }
}
