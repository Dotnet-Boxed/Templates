namespace Boilerplate.FeatureSelection.Features
{
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;

    public class JavaScriptHintFeature : BinaryChoiceFeature
    {
        public JavaScriptHintFeature(IProjectService projectService)
            : base(projectService)
        {
        }

        public override string Description
        {
            get { return "Adds JavaScript Hint (JS-Hint) linting to the JavaScript build pipe-line. This checks for common JavaScript errors and code smells. The default rules can be changed in the .jshintrc file."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.CssAndJavaScript; }
        }

        public override string Icon
        {
            get { return "/Boilerplate.FeatureSelection;component/Assets/JSHint.png"; }
        }

        public override string Id
        {
            get { return "JavaScriptHint"; }
        }

        public override bool IsSelectedDefault
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
            get { return "JavaScript Hint (JS-Hint)"; }
        }

        protected override async Task AddFeature()
        {
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, "gulpfile.js");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, "package.json");
        }

        protected override async Task RemoveFeature()
        {
            this.ProjectService.DeleteFile(".jshintrc");

            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, "gulpfile.js");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, "package.json");
        }
    }
}
