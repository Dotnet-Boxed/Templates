namespace Boilerplate.Wizard.Features
{
    using System.Threading.Tasks;
    using Boilerplate.Wizard.Services;

    public class NWebSecFeature : BinaryChoiceFeature
    {
        public NWebSecFeature(
            IProjectService projectService)
            : base(projectService)
        {
        }

        public override string Description
        {
            get { return "Use NWebSec to add better security through the use of HTTP Headers such as X-Content-Type-Options, X-Download-Options, X-Frame-Options, Strict-Transport-Security, Public-Key-Pins and Content-Security-Policy."; }
        }

        public override IFeatureGroup Group
        {
            get{ return FeatureGroups.Security; }
        }

        public override string Icon
        {
            get { return "/Boilerplate.Wizard;component/Assets/NWebSec.png"; }
        }

        public override string Id
        {
            get { return "NWebSec"; }
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
            get { return 2; }
        }

        public override string Title
        {
            get { return "NWebSec"; }
        }

        protected override async Task AddFeature()
        {
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, "project.json");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, "ReadMe.html");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, "Startup.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, "Startup.Filters.cs");
        }

        protected override async Task RemoveFeature()
        {
            this.ProjectService.DeleteFile("Startup.ContentSecurityPolicy.cs");

            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, "project.json");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, "ReadMe.html");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, "Startup.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, "Startup.Filters.cs");
        }
    }
}
