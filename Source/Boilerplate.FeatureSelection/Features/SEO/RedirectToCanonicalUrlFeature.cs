namespace Boilerplate.FeatureSelection.Features
{
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;

    public class RedirectToCanonicalUrlFeature : BinaryChoiceFeature
    {
        public RedirectToCanonicalUrlFeature(IProjectService projectService)
            : base(projectService)
        {
        }

        public override string Description
        {
            get { return "Redirects requests to the Canonical URL for better SEO e.g. http://example.com/NO-TRAILING-SLASH gets redirected to http://example.com/no-trailing-slash/."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.SearchEngineOptimization; }
        }

        public override string Id
        {
            get { return "RedirectToCanonicalUrl"; }
        }

        public override bool IsSelectedDefault
        {
            get { return true; }
        }

        public override bool IsVisible
        {
            get { return true; }
        }

        public override int Order
        {
            get { return 3; }
        }

        public override string Title
        {
            get { return "Redirect to Canonical URL"; }
        }

        protected override async Task AddFeature()
        {
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Startup.cs");
        }

        protected override async Task RemoveFeature()
        {
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Startup.cs");
        }
    }
}
