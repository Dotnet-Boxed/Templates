namespace Boilerplate.Wizard.Features
{
    using System.Threading.Tasks;
    using Boilerplate.Wizard.Services;

    public class JavaScriptTestFrameworkFeature : MultiChoiceFeature
    {
        private const string BowerJson = "bower.json";
        private const string GulpfileJs = "gulpfile.js";
        private const string PackageJson = "package.json";
        private const string TestDirectory = "Test";

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
                true)
            {
                IsSelected = true
            };
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

        public override int Order
        {
            get { return 2; }
        }

        public override string Title
        {
            get { return "JavaScript Test Framework"; }
        }

        public override Task AddOrRemoveFeature()
        {
            if (this.mocha.IsSelected)
            {
                Task t1 = this.ProjectService.EditComment(
                   this.Id,
                   EditCommentMode.UncommentCode,
                   BowerJson);
                Task t2 = this.ProjectService.EditComment(
                    this.Id,
                    EditCommentMode.UncommentCode,
                    GulpfileJs);
                Task t3 = this.ProjectService.EditComment(
                    this.Id,
                    EditCommentMode.UncommentCode,
                    PackageJson);
                return Task.WhenAll(t1, t2, t3);
            }
            else
            {
                Task t1 = this.ProjectService.DeleteDirectory(TestDirectory);
                Task t2 = this.ProjectService.EditComment(
                    this.Id,
                    EditCommentMode.DeleteCode,
                    BowerJson);
                Task t3 = this.ProjectService.EditComment(
                    this.Id,
                    EditCommentMode.DeleteCode,
                    GulpfileJs);
                Task t4 = this.ProjectService.EditComment(
                    this.Id,
                    EditCommentMode.DeleteCode,
                    PackageJson);
                return Task.WhenAll(t1, t2, t3, t4);
            }
        }
    }
}