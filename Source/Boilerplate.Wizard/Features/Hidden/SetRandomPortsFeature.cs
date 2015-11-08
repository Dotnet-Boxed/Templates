namespace Boilerplate.Wizard.Features
{
    using System.Threading.Tasks;
    using Boilerplate.Wizard.Services;

    public class SetRandomPortsFeature : BinaryChoiceFeature
    {
        private readonly IPortService portService;

        public SetRandomPortsFeature(
            IPortService portService,
            IProjectService projectService)
            : base(projectService)
        {
            this.portService = portService;
        }

        public override string Description
        {
            get { return "Set random development server and SSL ports in .xproj."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.Hidden; }
        }

        public override string Id
        {
            get { return "SetRandomPorts"; }
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
            get { return 1; }
        }

        public override string Title
        {
            get { return "Set Random Ports"; }
        }

        protected override async Task AddFeature()
        {
            await this.ProjectService.ReplaceByPattern("1025", portService.GetRandomFreePort().ToString(), "*.xproj");
            await this.ProjectService.ReplaceByPattern("44300", portService.GetRandomFreePort(true).ToString(), "*.xproj");
        }
    }
}
