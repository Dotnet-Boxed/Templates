namespace Boilerplate.Wizard.Features
{
    using Boilerplate.Wizard.Services;

    public class FrontEndFrameworkFeature : MultiChoiceFeature
    {
        public FrontEndFrameworkFeature(IProjectService projectService)
            : base(projectService, 
                  new IFeatureItem[] 
                  {
                      new FeatureItem(
                          "Bootstrap",
                          "Bootstrap is the most popular HTML, CSS, and JS framework for developing responsive, mobile first projects on the web.",
                          1,
                          "/Boilerplate.Wizard;component/Assets/Bootstrap.png")
                      {
                          IsSelected = true
                      },
                      new FeatureItem(
                          "Zurb Foundation", 
                          "The most advanced responsive front-end framework in the world.", 
                          2,
                          "/Boilerplate.Wizard;component/Assets/Zurb Foundation.png"),
                      new FeatureItem(
                          "Semantic UI", 
                          "Semantic is a development framework that helps create beautiful, responsive layouts using human-friendly HTML.", 
                          3,
                          "/Boilerplate.Wizard;component/Assets/Semantic UI.png")
                  })
        {
        }

        public override string Description
        {
            get { return "The type of front end CSS and JavaScript framework you want to use."; }
        }

        public override string GroupName
        {
            get { return "CSS & JavaScript"; }
        }

        public override int Order
        {
            get { return 2; }
        }

        public override string Title
        {
            get { return "Front End Framework"; }
        }

        public override void AddOrRemoveFeature()
        {
            this.ProjectService.DeleteComment("FrontEndFramework", DeleteCommentMode.StartEndComment);
        }
    }
}
