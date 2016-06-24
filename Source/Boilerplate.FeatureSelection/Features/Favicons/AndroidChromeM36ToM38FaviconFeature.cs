namespace Boilerplate.FeatureSelection.Features
{
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;

    public class AndroidChromeM36ToM38FaviconFeature : BinaryChoiceFeature
    {
        public AndroidChromeM36ToM38FaviconFeature(IProjectService projectService)
            : base(projectService)
        {
        }

        public override string Description
        {
            get { return "Add support for a high resolution favicon, for use by Android Chrome versions M36 to M38."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.Favicons; }
        }

        public override string Id
        {
            get { return "AndroidChromeM36ToM38Favicon"; }
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
            get { return "Android Chrome M36 to M38 Favicon"; }
        }

        protected override async Task AddFeature()
        {
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Views\Shared\_Layout.cshtml");
        }

        protected override async Task RemoveFeature()
        {
            this.ProjectService.DeleteFile(@"wwwroot\img\icons\favicon-192x192.png");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Views\Shared\_Layout.cshtml");
        }
    }
}
