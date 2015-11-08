namespace Boilerplate.Wizard.Features
{
    using System.Threading.Tasks;
    using Boilerplate.Wizard.Services;

    public class BsonFormatterFeature : BinaryChoiceFeature
    {
        private const string StartupFormattersCs = "Startup.Formatters.cs";

        private readonly FeatureSet featureSet;

        public BsonFormatterFeature(IProjectService projectService, FeatureSet featureSet)
            : base(projectService)
        {
            this.featureSet = featureSet;
        }

        public override string Description
        {
            get { return "Choose whether to use the BSON input/output formatter."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.Formatters; }
        }

        public override string Id
        {
            get { return "BsonFormatter"; }
        }

        public override bool IsSelectedDefault
        {
            get { return this.featureSet == FeatureSet.Mvc6Api; }
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
            get { return "BSON Formatter"; }
        }

        protected override Task AddFeature()
        {
            return this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, StartupFormattersCs);
        }

        protected override Task RemoveFeature()
        {
            return this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, StartupFormattersCs);
        }
    }
}