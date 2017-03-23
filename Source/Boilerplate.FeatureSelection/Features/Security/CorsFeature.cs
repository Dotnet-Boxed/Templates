namespace Boilerplate.FeatureSelection.Features
{
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;

    public class CorsFeature : BinaryChoiceFeature
    {
        public CorsFeature(
            IProjectService projectService)
            : base(projectService)
        {
        }

        public override string Description
        {
            get { return "Browser security prevents a web page from making AJAX requests to another domain. This restriction is called the same-origin policy, and prevents a malicious site from reading sensitive data from another site. CORS is a W3C standard that allows a server to relax the same-origin policy. Using CORS, a server can explicitly allow some cross-origin requests while rejecting others."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.Security; }
        }

        public override string Id
        {
            get { return "CORS"; }
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
            get { return 2; }
        }

        public override string Title
        {
            get { return "Cross Origin Resource Sharing (CORS)"; }
        }

        protected override async Task AddFeature()
        {
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, this.ProjectService.ProjectFileName);
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, "ServiceCollectionExtensions.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, "Startup.cs");
        }

        protected override async Task RemoveFeature()
        {
            this.ProjectService.DeleteFile(@"Constants\CorsPolicyName.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, this.ProjectService.ProjectFileName);
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, "ServiceCollectionExtensions.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, "Startup.cs");
        }
    }
}
