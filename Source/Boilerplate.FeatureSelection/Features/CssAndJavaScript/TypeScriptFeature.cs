namespace Boilerplate.FeatureSelection.Features
{
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;

    public class TypeScriptFeature : BinaryChoiceFeature
    {
        public TypeScriptFeature(IProjectService projectService)
            : base(projectService)
        {
        }

        public override string Description
        {
            get { return "TypeScript is a typed superset of JavaScript that compiles to plain JavaScript. Note that this feature enables the transpiling of TypeScript (.ts) files to JavaScript (.js) in Gulpfile.js but does not add TypeScript files instead of JavaScript files yet. That is coming in the future."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.CssAndJavaScript; }
        }

        public override string Icon
        {
            get { return "/Boilerplate.FeatureSelection;component/Assets/TypeScript.png"; }
        }

        public override string Id
        {
            get { return "TypeScript"; }
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
            get { return 2; }
        }

        public override string Title
        {
            get { return "TypeScript"; }
        }

        protected override async Task AddFeature()
        {
            this.ProjectService.DeleteFile(@"Scripts\site.js");

            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, "gulpfile.js");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, "package.json");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, "ReadMe.html");
        }

        protected override async Task RemoveFeature()
        {
            this.ProjectService.DeleteFile("tsconfig.json");
            this.ProjectService.DeleteFile("tslint.json");
            this.ProjectService.DeleteFile(@"Scripts\site.ts");

            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, "gulpfile.js");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, "package.json");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, "ReadMe.html");
        }
    }
}
