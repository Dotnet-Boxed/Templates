namespace Boilerplate.Wizard.Features
{
    using System.Threading.Tasks;
    using Boilerplate.Wizard.Services;

    public class HttpsEverywhereFeature : BinaryChoiceFeature
    {
        public HttpsEverywhereFeature(
            IProjectService projectService)
            : base(projectService)
        {
        }

        public override string Description
        {
            get { return "Use the HTTPS scheme and TLS security across the entire site."; }
        }

        public override IFeatureGroup Group
        {
            get{ return FeatureGroups.Security; }
        }

        public override string Id
        {
            get { return "HttpsEverywhere"; }
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
            get { return "HTTPS Everywhere"; }
        }

        protected override async Task AddFeature()
        {
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.UncommentCode, "Startup.Antiforgery.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.UncommentCode, "Startup.Filters.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, "ReadMe.html");
        }

        protected override async Task RemoveFeature()
        {
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, "Startup.Antiforgery.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, "Startup.Filters.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, "ReadMe.html");
        }
    }
}
