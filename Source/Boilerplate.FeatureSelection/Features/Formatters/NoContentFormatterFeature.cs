namespace Boilerplate.FeatureSelection.Features
{
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;

    public class NoContentFormatterFeature : BinaryChoiceFeature
    {
        public NoContentFormatterFeature(IProjectService projectService)
            : base(projectService)
        {
        }

        public override string Description
        {
            get { return "Force HTTP 204 No Content response, when returning null values."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.Formatters; }
        }

        public override string Id
        {
            get { return "NoContentFormatter"; }
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
            get { return "HTTP No Content Formatter"; }
        }

        protected override Task AddFeature()
        {
            return this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, "Startup.cs");
        }

        protected override Task RemoveFeature()
        {
            return this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, "Startup.cs");
        }
    }
}
