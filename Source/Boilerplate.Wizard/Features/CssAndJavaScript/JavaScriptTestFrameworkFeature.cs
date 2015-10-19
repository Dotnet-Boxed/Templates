namespace Boilerplate.Wizard.Features
{
    using System.Threading.Tasks;
    using Boilerplate.Wizard.Services;

    public class JavaScriptTestFrameworkFeature : MultiChoiceFeature
    {
        private readonly IFeatureItem jasmine;
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
                "/Boilerplate.Wizard;component/Assets/Mocha.png")
            {
                IsSelected = true
            };
            this.Items.Add(this.mocha);

            this.jasmine = new FeatureItem(
                "Jasmine",
                "Jasmine",
                "Mocha is a feature-rich JavaScript test framework running on Node.js and the browser, making asynchronous testing simple and fun. The Sinon stub/mocking library is also included.",
                2,
                "/Boilerplate.Wizard;component/Assets/Jasmine.png",
                true);
            this.Items.Add(this.jasmine);

            this.none = new FeatureItem(
                "None",
                "None",
                "No JavaScript testing framework.",
                3);
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
            get { return 4; }
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
                   "bower.json");
                //await this.ProjectService.EditCommentInFile(
                //    this.Id,
                //    EditCommentMode.LeaveCodeUnchanged,
                //    "gulpfile.js");
                //await this.ProjectService.EditCommentInFile(
                //    this.Id,
                //    EditCommentMode.LeaveCodeUnchanged,
                //    "package.json");
            }
            else
            {
                await this.ProjectService.DeleteDirectory("Test");
                await this.ProjectService.EditCommentInFile(
                   this.mocha.CommentName,
                   EditCommentMode.DeleteCode,
                   "bower.json");
                //await this.ProjectService.EditCommentInFile(
                //    this.Id,
                //    EditCommentMode.DeleteCode,
                //    "gulpfile.js");
                //await this.ProjectService.EditCommentInFile(
                //    this.Id,
                //    EditCommentMode.DeleteCode,
                //    "package.json");
            }
        }
    }
}