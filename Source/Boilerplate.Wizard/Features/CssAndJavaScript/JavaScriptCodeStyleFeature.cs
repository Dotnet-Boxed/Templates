namespace Boilerplate.Wizard.Features
{
    using System.Threading.Tasks;
    using Boilerplate.Wizard.Services;

    public class JavaScriptCodeStyleFeature : BinaryChoiceFeature
    {
        public JavaScriptCodeStyleFeature(IProjectService projectService)
            : base(projectService)
        {
        }

        public override string Description
        {
            get { return "Adds JavaScript Code Style (JSCS) linting to the JavaScript build pipe-line. This checks that the structure (not functionality) of the JavaScript is consistent. The default code style is to use Douglas Crockford's recommended rule set found in the .jscsrc file."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.CssAndJavaScript; }
        }

        public override string Icon
        {
            get { return "/Boilerplate.Wizard;component/Assets/JSCS.png"; }
        }

        public override string Id
        {
            get { return "JavaScriptCodeStyle"; }
        }

        public override bool IsDefaultSelected
        {
            get { return true; }
        }

        public override bool IsVisible
        {
            get { return true; }
        }

        public override int Order
        {
            get { return 3; }
        }

        public override string Title
        {
            get { return "JavaScript Code Style (JSCS)"; }
        }

        protected override async Task AddFeature()
        {
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, "gulpfile.js");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, "package.json");
        }

        protected override async Task RemoveFeature()
        {
            await this.ProjectService.DeleteFile(".jscsrc");

            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, "gulpfile.js");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, "package.json");
        }
    }
}
