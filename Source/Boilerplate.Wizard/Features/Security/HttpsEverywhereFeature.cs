namespace Boilerplate.Wizard.Features
{
    using Boilerplate.Wizard.Services;

    public class HttpsEverywhereFeature : BinaryFeature
    {
        public HttpsEverywhereFeature(IProjectService projectService)
            : base(projectService, true)
        {
        }

        public override string Description
        {
            get { return "Use HTTPS scheme across the entire site."; }
        }

        public override IFeatureGroup Group
        {
            get{ return FeatureGroups.Security; }
        }

        public override string Id
        {
            get { return "HttpsEverywhere"; }
        }

        public override int Order
        {
            get { return 1; }
        }

        public override string Title
        {
            get { return "HTTPS Everywhere"; }
        }
    }
}
