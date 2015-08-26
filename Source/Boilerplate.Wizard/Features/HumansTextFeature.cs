namespace Boilerplate.Wizard.Features
{
    using System;
    using System.Threading.Tasks;
    using Boilerplate.Wizard.Services;

    public class HumansTextFeature : BinaryFeature
    {
        public HumansTextFeature(IProjectService projectService)
            : base(projectService)
        {
            this.IsSelected = true;
        }

        public override string Title
        {
            get { return "Humans.txt"; }
        }

        public override string Description
        {
            get { return "Tells the world who wrote the application. This file is a good place to thank your developers."; }
        }

        public override string GroupName
        {
            get { return "Other"; }
        }

        public override int Order
        {
            get { return 4; }
        }

        protected override Task RemoveFeature()
        {
            return this.ProjectService.DeleteFile(@"wwwroot\humans.txt");
        }
    }
}
