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
            get { return "HumansTxt"; }
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
            get { return 1; }
        }

        protected override Task RemoveFeature()
        {
            return this.ProjectService.DeleteFile(@"wwwroot\humans.txt");
        }
    }
}
