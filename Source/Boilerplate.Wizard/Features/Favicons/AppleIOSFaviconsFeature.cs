namespace Boilerplate.Wizard.Features
{
    using System.Threading.Tasks;
    using Boilerplate.Wizard.Services;

    public class AppleIOSFaviconsFeature : BinaryChoiceFeature
    {
        public AppleIOSFaviconsFeature(IProjectService projectService)
            : base(projectService)
        {
        }

        public override string Description
        {
            get { return "Add support for Apple touch icons used on iOS. Note that these are also used by older versions of Android Chrome."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.Favicons; }
        }

        public override string Id
        {
            get { return "AppleIOSFavicons"; }
        }

        public override bool IsDefaultSelected
        {
            get { return true; }
        }

        public override bool IsVisible
        {
            get { return true; }
        }

        public override int Order
        {
            get { return 1; }
        }

        public override string Title
        {
            get { return "Apple iOS Favicons"; }
        }

        protected override async Task AddFeature()
        {
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Views\Shared\_Layout.cshtml");
        }

        protected override async Task RemoveFeature()
        {
            await this.ProjectService.DeleteFile(@"wwwroot\img\icons\apple-touch-icon-57x57.png");
            await this.ProjectService.DeleteFile(@"wwwroot\img\icons\apple-touch-icon-60x60.png");
            await this.ProjectService.DeleteFile(@"wwwroot\img\icons\apple-touch-icon-72x72.png");
            await this.ProjectService.DeleteFile(@"wwwroot\img\icons\apple-touch-icon-76x76.png");
            await this.ProjectService.DeleteFile(@"wwwroot\img\icons\apple-touch-icon-114x114.png");
            await this.ProjectService.DeleteFile(@"wwwroot\img\icons\apple-touch-icon-120x120.png");
            await this.ProjectService.DeleteFile(@"wwwroot\img\icons\apple-touch-icon-144x144.png");
            await this.ProjectService.DeleteFile(@"wwwroot\img\icons\apple-touch-icon-152x152.png");
            await this.ProjectService.DeleteFile(@"wwwroot\img\icons\apple-touch-icon-180x180.png");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Views\Shared\_Layout.cshtml");
        }
    }
}
