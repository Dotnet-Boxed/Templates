namespace Boilerplate.Wizard.Features
{
    using System.Threading.Tasks;
    using Boilerplate.Wizard.Services;

    public class HumansTextFeature : BinaryChoiceFeature
    {
        public HumansTextFeature(IProjectService projectService)
            : base(projectService)
        {
        }

        public override string Id
        {
            get { return "HumansText"; }
        }

        public override bool IsDefaultSelected
        {
            get { return true; }
        }

        public override bool IsVisible
        {
            get { return true; }
        }

        public override string Title
        {
            get { return "Humans.txt"; }
        }

        public override string Description
        {
            get { return "Tells the world who wrote the application. This file is a good place to thank your developers."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.Other; }
        }

        public override int Order
        {
            get { return 5; }
        }

        protected override async Task AddFeature()
        {
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Views\Home\Index.cshtml");
        }

        protected override async Task RemoveFeature()
        {
            await this.ProjectService.DeleteFile(@"wwwroot\humans.txt");

            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Views\Home\Index.cshtml");
        }
    }
}
