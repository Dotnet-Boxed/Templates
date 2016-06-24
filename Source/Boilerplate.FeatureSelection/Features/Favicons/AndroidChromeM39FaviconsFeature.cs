namespace Boilerplate.FeatureSelection.Features
{
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;

    public class AndroidChromeM39FaviconsFeature : BinaryChoiceFeature
    {
        public AndroidChromeM39FaviconsFeature(IProjectService projectService)
            : base(projectService)
        {
        }

        public override string Description
        {
            get { return "Add support for favicons for use by Android Chrome versions M39+. All of these favicons are specified in a http://example.com/manifest.json file which is dynamically generated and served from HomeController."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.Favicons; }
        }

        public override string Icon
        {
            get { return "/Boilerplate.FeatureSelection;component/Assets/Android Chrome.png"; }
        }

        public override string Id
        {
            get { return "AndroidChromeM39Favicons"; }
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
            get { return 4; }
        }

        public override string Title
        {
            get { return "Android Chrome M39+"; }
        }

        protected override async Task AddFeature()
        {
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Constants\HomeController\HomeControllerRoute.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Constants\CacheProfileName.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Controllers\HomeController.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Views\Home\Index.cshtml");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Views\Shared\_Layout.cshtml");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"config.json");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"ServiceCollectionExtensions.cs");
        }

        protected override async Task RemoveFeature()
        {
            this.ProjectService.DeleteDirectory(@"Services\Manifest");

            this.ProjectService.DeleteFile(@"wwwroot\img\icons\android-chrome-36x36.png");
            this.ProjectService.DeleteFile(@"wwwroot\img\icons\android-chrome-48x48.png");
            this.ProjectService.DeleteFile(@"wwwroot\img\icons\android-chrome-72x72.png");
            this.ProjectService.DeleteFile(@"wwwroot\img\icons\android-chrome-96x96.png");
            this.ProjectService.DeleteFile(@"wwwroot\img\icons\android-chrome-144x144.png");
            this.ProjectService.DeleteFile(@"wwwroot\img\icons\android-chrome-192x192.png");

            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Constants\HomeController\HomeControllerRoute.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Constants\CacheProfileName.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Controllers\HomeController.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Views\Home\Index.cshtml");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Views\Shared\_Layout.cshtml");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"config.json");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"ServiceCollectionExtensions.cs");
        }
    }
}
