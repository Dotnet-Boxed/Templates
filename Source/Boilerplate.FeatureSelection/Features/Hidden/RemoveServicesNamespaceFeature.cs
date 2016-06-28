namespace Boilerplate.FeatureSelection.Features
{
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;

    public class RemoveServicesNamespaceFeature : BinaryChoiceFeature
    {
        private readonly AndroidChromeM39FaviconsFeature androidChromeM39FaviconsFeature;
        private readonly FeedFeature feedFeature;
        private readonly RobotsTextFeature robotsTextFeature;
        private readonly SearchFeature searchFeature;
        private readonly SitemapFeature sitemapFeature;
        private readonly Windows81IE11EdgeFaviconFeature windows81IE11EdgeFaviconFeature;

        public RemoveServicesNamespaceFeature(
            IProjectService projectService,
            AndroidChromeM39FaviconsFeature androidChromeM39FaviconsFeature,
            FeedFeature feedFeature,
            RobotsTextFeature robotsTextFeature,
            SearchFeature searchFeature,
            SitemapFeature sitemapFeature,
            Windows81IE11EdgeFaviconFeature windows81IE11EdgeFaviconFeature)
            : base(projectService)
        {
            this.androidChromeM39FaviconsFeature = androidChromeM39FaviconsFeature;
            this.feedFeature = feedFeature;
            this.robotsTextFeature = robotsTextFeature;
            this.searchFeature = searchFeature;
            this.sitemapFeature = sitemapFeature;
            this.windows81IE11EdgeFaviconFeature = windows81IE11EdgeFaviconFeature;

            this.androidChromeM39FaviconsFeature.PropertyChanged += this.OnOtherFeaturePropertyChanged;
            this.feedFeature.PropertyChanged += this.OnOtherFeaturePropertyChanged;
            this.robotsTextFeature.PropertyChanged += this.OnOtherFeaturePropertyChanged;
            this.searchFeature.PropertyChanged += this.OnOtherFeaturePropertyChanged;
            this.sitemapFeature.PropertyChanged += this.OnOtherFeaturePropertyChanged;
            this.windows81IE11EdgeFaviconFeature.PropertyChanged += this.OnOtherFeaturePropertyChanged;
        }

        public override string Description
        {
            get { return "Deletes the services namespace if all services are deselected."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.Hidden; }
        }

        public override string Id
        {
            get { return "Services"; }
        }

        public override bool IsSelected
        {
            get
            {
                return !this.androidChromeM39FaviconsFeature.IsSelected &&
                    !this.feedFeature.IsSelected &&
                    !this.robotsTextFeature.IsSelected &&
                    !this.searchFeature.IsSelected &&
                    !this.sitemapFeature.IsSelected &&
                    !this.windows81IE11EdgeFaviconFeature.IsSelected;
            }

            set { }
        }

        public override bool IsSelectedDefault
        {
            get { return false; }
        }

        public override bool IsVisible
        {
            get { return false; }
        }

        public override int Order
        {
            get { return 2; }
        }

        public override string Title
        {
            get { return "Services"; }
        }

        private void OnOtherFeaturePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (string.Equals(e.PropertyName, nameof(this.IsSelected)))
            {
                this.OnPropertyChanged(nameof(this.IsSelected), nameof(this.IsDefaultSelected));
            }
        }

        protected override async Task AddFeature()
        {
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Controllers\HomeController.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"ServiceCollectionExtensions.cs");
            this.ProjectService.DeleteDirectory("Services");
        }

        protected override async Task RemoveFeature()
        {
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Controllers\HomeController.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"ServiceCollectionExtensions.cs");
        }
    }
}
