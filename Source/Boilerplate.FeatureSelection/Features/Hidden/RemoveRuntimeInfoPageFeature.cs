namespace Boilerplate.FeatureSelection.Features
{
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;

    public class RemoveRuntimeInfoPageFeature : BinaryChoiceFeature
    {
        public RemoveRuntimeInfoPageFeature(IProjectService projectService)
            : base(projectService)
        {
        }

        public override string Description
        {
            get { return "Remove the /runtimeinfo page until it comes back."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.Hidden; }
        }

        public override string Id
        {
            get { return "RuntimeInfoPage"; }
        }

        public override bool IsSelectedDefault
        {
            get { return true; }
        }

        public override bool IsVisible
        {
            get { return false; }
        }

        public override int Order
        {
            get { return 4; }
        }

        public override string Title
        {
            get { return "Remove RuntimeInfoPage"; }
        }

        protected override async Task AddFeature()
        {
            await this.ProjectService.EditCommentInFile(
                this.Id,
                EditCommentMode.DeleteCode,
                @"ApplicationBuilderExtensions.cs");
            await this.ProjectService.EditCommentInFile(
                this.Id,
                EditCommentMode.DeleteCode,
                @"Views\Home\Index.cshtml");
            await this.ProjectService.EditCommentInFile(
                this.Id,
                EditCommentMode.DeleteCode,
                @"Views\Shared\_Layout.cshtml");
        }
    }
}