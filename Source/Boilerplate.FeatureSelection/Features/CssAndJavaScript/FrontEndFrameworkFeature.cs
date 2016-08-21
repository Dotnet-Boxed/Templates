namespace Boilerplate.FeatureSelection.Features
{
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;

    public class FrontEndFrameworkFeature : MultiChoiceFeature
    {
        private readonly IFeatureItem bootstrap;
        private readonly IFeatureItem semanticUI;
        private readonly IFeatureItem zurbFoundation;

        public FrontEndFrameworkFeature(IProjectService projectService)
            : base(projectService)
        {
            this.bootstrap = new FeatureItem(
                "Bootstrap",
                "Bootstrap",
                "Bootstrap is the most popular HTML, CSS, and JS framework for developing responsive, mobile first projects on the web.",
                1,
                "/Boilerplate.FeatureSelection;component/Assets/Bootstrap.png")
            {
                IsSelected = true
            };
            this.Items.Add(this.bootstrap);

            this.zurbFoundation = new FeatureItem(
                "ZurbFoundation",
                "Zurb Foundation",
                "The most advanced responsive front-end framework in the world.",
                2,
                "/Boilerplate.FeatureSelection;component/Assets/Zurb Foundation.png")
            {
                IsContributionWanted = true,
                IsEnabled = false
            };
            this.Items.Add(this.zurbFoundation);

            this.semanticUI = new FeatureItem(
                "SemanticUI",
                "Semantic UI",
                "Semantic is a development framework that helps create beautiful, responsive layouts using human-friendly HTML.",
                3,
                "/Boilerplate.FeatureSelection;component/Assets/Semantic UI.png")
            {
                IsContributionWanted = true,
                IsEnabled = false
            };
            this.Items.Add(this.semanticUI);
        }

        public override string Description
        {
            get { return "The type of front end CSS and JavaScript framework you want to use."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.CssAndJavaScript; }
        }

        public override string Id
        {
            get { return "FrontEndFramework"; }
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
            get { return "Front End Framework"; }
        }

        public override Task AddOrRemoveFeature()
        {
            return Task.FromResult<object>(null);
        }
    }
}
