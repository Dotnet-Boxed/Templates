namespace Boilerplate.FeatureSelection.Features
{
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;

    public class JavaScriptTestFrameworkFeature : MultiChoiceFeature
    {
        private readonly IFeatureItem mocha;
        private readonly IFeatureItem none;

        public JavaScriptTestFrameworkFeature(IProjectService projectService)
            : base(projectService)
        {
            this.mocha = new FeatureItem(
                "Mocha",
                "Mocha",
                "Mocha is a feature-rich JavaScript test framework running on Node.js and the browser, making asynchronous testing simple and fun. The Chai assertion framework and Sinon stub/mocking library are also included.",
                1,
                "/Boilerplate.FeatureSelection;component/Assets/Mocha.png")
            {
                IsSelected = true
            };
            this.Items.Add(this.mocha);

            this.none = new FeatureItem(
                "None",
                "None",
                "No JavaScript testing framework.",
                2);
            this.Items.Add(this.none);
        }

        public override string Description
        {
            get { return "The type of JavaScript testing framework you want to use."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.CssAndJavaScript; }
        }

        public override string Id
        {
            get { return "JavaScriptTestFramework"; }
        }

        public override bool IsVisible
        {
            get { return true; }
        }

        public override int Order
        {
            get { return 5; }
        }

        public override string Title
        {
            get { return "JavaScript Test Framework"; }
        }

        public override async Task AddOrRemoveFeature()
        {
            if (this.mocha.IsSelected)
            {
                await this.ProjectService.EditCommentInFile(
                    this.mocha.CommentName,
                    EditCommentMode.LeaveCodeUnchanged,
                    "gulpfile.js");
                await this.ProjectService.EditCommentInFile(
                    this.mocha.CommentName,
                    EditCommentMode.LeaveCodeUnchanged,
                    "package.json");
            }
            else
            {
                this.ProjectService.DeleteDirectory("Tests");
                await this.ProjectService.EditCommentInFile(
                    this.mocha.CommentName,
                    EditCommentMode.DeleteCode,
                    "gulpfile.js");
                await this.ProjectService.EditCommentInFile(
                    this.mocha.CommentName,
                    EditCommentMode.DeleteCode,
                    "package.json");
            }
        }
    }
}