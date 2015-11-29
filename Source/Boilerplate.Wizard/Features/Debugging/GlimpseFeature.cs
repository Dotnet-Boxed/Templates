namespace Boilerplate.Wizard.Features
{
    using System.Threading.Tasks;
    using Boilerplate.Wizard.Services;

    public class GlimpseFeature : BinaryChoiceFeature
    {
        public GlimpseFeature(IProjectService projectService)
            : base(projectService)
        {
        }

        public override string Description
        {
            get { return "Add support for Glimpse for easier debugging. Get detailed information through the Glimpse dashboard in the browser. Find out more at getglimpse.com."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.Debugging; }
        }

        public override string Icon
        {
            get { return "/Boilerplate.Wizard;component/Assets/Glimpse.png"; }
        }

        public override string Id
        {
            get { return "Glimpse"; }
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
            get { return "Glimpse"; }
        }

        protected override async Task AddFeature()
        {
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"project.json");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Startup.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Startup.Debugging.cs");
        }

        protected override async Task RemoveFeature()
        {
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"project.json");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Startup.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Startup.Debugging.cs");
        }
    }
}
