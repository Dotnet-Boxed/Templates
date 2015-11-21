namespace Boilerplate.Wizard.Features
{
    using System.Threading.Tasks;
    using Boilerplate.Wizard.Services;

    public class CstmlMinificationFeature : BinaryChoiceFeature
    {
        public CstmlMinificationFeature(IProjectService projectService)
            : base(projectService)
        {
        }

        public override string Description
        {
            get { return "Minifies .cshtml files into .min.cshtml files which are then served using a special Razor view engine looking for these files. This is an experimental feature that uses the gulp-minify-cshtml Gulp plugin created by Muhammad Rehan Saeed as a proof of concept."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.Performance; }
        }

        public override string Id
        {
            get { return "CshtmlMinification"; }
        }

        public override bool IsSelectedDefault
        {
            get { return false; }
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
            get { return "CSHTML Minification (Experimental)"; }
        }

        protected override async Task AddFeature()
        {
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"gulpfile.js");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"package.json");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"project.json");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Startup.cs");
        }

        protected override async Task RemoveFeature()
        {
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"gulpfile.js");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"package.json");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"project.json");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Startup.cs");
        }
    }
}
