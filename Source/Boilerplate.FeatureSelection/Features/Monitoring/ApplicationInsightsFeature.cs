namespace Boilerplate.FeatureSelection.Features
{
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;

    public class ApplicationInsightsFeature : BinaryChoiceFeature
    {
        public ApplicationInsightsFeature(IProjectService projectService)
            : base(projectService)
        {
        }

        public override string Description
        {
            get { return "Monitor internal information about how your application is running, as well as external user information. Note that you must get a Application Insights Instrumentation Key from Azure and add it to config.json."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.Monitoring; }
        }

        public override string Icon
        {
            get { return "/Boilerplate.FeatureSelection;component/Assets/Application Insights.png"; }
        }

        public override string Id
        {
            get { return "ApplicationInsights"; }
        }

        public override bool IsSelectedDefault
        {
            get { return false; }
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
            get { return "Application Insights"; }
        }

        protected override async Task AddFeature()
        {
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Services\Sitemap\SitemapService.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Services\SitemapPinger\SitemapPingerService.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Views\Shared\_Layout.cshtml");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Views\_ViewImports.cshtml");
            await this.ProjectService.EditCommentInFile(this.Id + "-On", EditCommentMode.LeaveCodeUnchanged, @"ApplicationBuilderExtensions.cs");
            await this.ProjectService.EditCommentInFile(this.Id + "-Off", EditCommentMode.DeleteCode, @"ApplicationBuilderExtensions.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Startup.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"config.json");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"gulpfile.js");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"package.json");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, this.ProjectService.ProjectFileName);
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"ReadMe.html");
        }

        protected override async Task RemoveFeature()
        {
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Services\Sitemap\SitemapService.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Services\SitemapPinger\SitemapPingerService.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Views\Shared\_Layout.cshtml");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Views\_ViewImports.cshtml");
            await this.ProjectService.EditCommentInFile(this.Id + "-On", EditCommentMode.DeleteCode, @"ApplicationBuilderExtensions.cs");
            await this.ProjectService.EditCommentInFile(this.Id + "-Off", EditCommentMode.UncommentCode, @"ApplicationBuilderExtensions.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Startup.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"config.json");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"gulpfile.js");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"package.json");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, this.ProjectService.ProjectFileName);
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"ReadMe.html");
        }
    }
}
