namespace Boilerplate.FeatureSelection.Features
{
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;

    public class FeedFeature : BinaryChoiceFeature
    {
        public FeedFeature(IProjectService projectService)
            : base(projectService)
        {
        }

        public override string Description
        {
            get { return "Adds an Atom 1.0 Syndication Feed at /feed. Note that Atom supersedes RSS."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.Other; }
        }

        public override string Icon
        {
            get { return "/Boilerplate.FeatureSelection;component/Assets/Atom Feed.png"; }
        }

        public override string Id
        {
            get { return "Feed"; }
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
            get { return 1; }
        }

        public override string Title
        {
            get { return "Atom Feed"; }
        }

        protected override async Task AddFeature()
        {
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Constants\HomeController\HomeControllerAction.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Constants\HomeController\HomeControllerRoute.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Constants\CacheProfileName.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Controllers\HomeController.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Services\BrowserConfig\BrowserConfigService.cs");
            await this.ProjectService.EditCommentInFile(this.Id + "-On", EditCommentMode.LeaveCodeUnchanged, @"Services\BrowserConfig\BrowserConfigService.cs");
            await this.ProjectService.EditCommentInFile(this.Id + "-Off", EditCommentMode.DeleteCode, @"Services\BrowserConfig\BrowserConfigService.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Views\Home\Index.cshtml");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Views\Shared\_Layout.cshtml");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"config.json");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"ReadMe.html");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"ServiceCollectionExtensions.cs");
        }

        protected override async Task RemoveFeature()
        {
            this.ProjectService.DeleteDirectory(@"Services\Feed");

            this.ProjectService.DeleteFile(@"wwwroot\img\icons\atom-icon-48x48.png");
            this.ProjectService.DeleteFile(@"wwwroot\img\icons\atom-logo-96x48.png");

            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Constants\HomeController\HomeControllerAction.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Constants\HomeController\HomeControllerRoute.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Constants\CacheProfileName.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Controllers\HomeController.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Services\BrowserConfig\BrowserConfigService.cs");
            await this.ProjectService.EditCommentInFile(this.Id + "-On", EditCommentMode.DeleteCode, @"Services\BrowserConfig\BrowserConfigService.cs");
            await this.ProjectService.EditCommentInFile(this.Id + "-Off", EditCommentMode.UncommentCode, @"Services\BrowserConfig\BrowserConfigService.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Views\Home\Index.cshtml");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Views\Shared\_Layout.cshtml");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"config.json");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"ReadMe.html");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"ServiceCollectionExtensions.cs");
        }
    }
}
