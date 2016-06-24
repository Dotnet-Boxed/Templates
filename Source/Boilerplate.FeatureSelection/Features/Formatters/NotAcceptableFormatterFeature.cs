namespace Boilerplate.FeatureSelection.Features
{
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;

    public class NotAcceptableFormatterFeature : BinaryChoiceFeature
    {
        public NotAcceptableFormatterFeature(IProjectService projectService)
            : base(projectService)
        {
        }

        public override string Description
        {
            get { return "Force HTTP 406 Not Acceptable responses if the media type is not supported, instead of returning the default."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.Formatters; }
        }

        public override string Id
        {
            get { return "NotAcceptableFormatter"; }
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
            get { return 5; }
        }

        public override string Title
        {
            get { return "HTTP Not Acceptable Formatter"; }
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
