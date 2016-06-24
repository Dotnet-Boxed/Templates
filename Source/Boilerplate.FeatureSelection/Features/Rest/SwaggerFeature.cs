namespace Boilerplate.FeatureSelection.Features
{
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;

    public class SwaggerFeature : BinaryChoiceFeature
    {
        public SwaggerFeature(IProjectService projectService)
            : base(projectService)
        {
        }

        public override string Description
        {
            get { return "Swagger is a simple yet powerful representation of your RESTful API. With a Swagger-enabled API, you get interactive documentation, client SDK generation and discoverability."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.Rest; }
        }

        public override string Icon
        {
            get { return "/Boilerplate.FeatureSelection;component/Assets/Swagger.png"; }
        }

        public override string Id
        {
            get { return "Swagger"; }
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
            get { return "Swagger"; }
        }

        protected override async Task AddFeature()
        {
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, "project.json");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, "Startup.cs");
        }

        protected override async Task RemoveFeature()
        {
            this.ProjectService.DeleteFile("Startup.Swagger.cs");

            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, "project.json");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, "Startup.cs");
        }
    }
}
