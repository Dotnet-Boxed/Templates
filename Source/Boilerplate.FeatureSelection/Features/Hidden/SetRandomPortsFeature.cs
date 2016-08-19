namespace Boilerplate.FeatureSelection.Features
{
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;

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
            get { return "Set random development server and SSL ports in launchsettings.json."; }
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
            var httpPort = portService.GetRandomFreePort().ToString();
            var httpsPort = portService.GetRandomFreePort(https: true).ToString();

            await this.ProjectService.ReplaceInFile("1025", httpPort, @"Properties\launchSettings.json");
            await this.ProjectService.ReplaceInFile("1025", httpPort, @"nginx.conf");
            await this.ProjectService.ReplaceInFile("44300", httpsPort, @"Properties\launchSettings.json");
        }
    }
}
