namespace Boilerplate.FeatureSelection.Features
{
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;

    public class Windows81IE11EdgeFaviconFeature : BinaryChoiceFeature
    {
        public Windows81IE11EdgeFaviconFeature(IProjectService projectService)
            : base(projectService)
        {
        }

        public override string Description
        {
            get { return "Add support for a favicon used by Windows 8.1 and IE 11 or Edge."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.Favicons; }
        }

        public override string Icon
        {
            get { return "/Boilerplate.FeatureSelection;component/Assets/Windows.png"; }
        }

        public override string Id
        {
            get { return "Windows81IE11EdgeFavicon"; }
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
            get { return 7; }
        }

        public override string Title
        {
            get { return "Windows 8.1 IE 11/Edge Favicon"; }
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
            this.ProjectService.DeleteDirectory(@"Services\BrowserConfig");

            this.ProjectService.DeleteFile(@"wwwroot\img\icons\mstile-70x70.png");
            this.ProjectService.DeleteFile(@"wwwroot\img\icons\mstile-150x150.png");
            this.ProjectService.DeleteFile(@"wwwroot\img\icons\mstile-310x310.png");
            this.ProjectService.DeleteFile(@"wwwroot\img\icons\mstile-310x150.png");

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
