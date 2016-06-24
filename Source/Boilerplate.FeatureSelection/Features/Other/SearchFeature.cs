namespace Boilerplate.FeatureSelection.Features
{
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;

    public class SearchFeature : BinaryChoiceFeature
    {
        public SearchFeature(IProjectService projectService)
            : base(projectService)
        {
        }

        public override string Description
        {
            get { return "Adds a search box to the navigation bar, a search action in the controller, a dynamically generated open search xml file at /opensearch.xml to give a better search experience in the browser and a meta tag in the HTML head to point to the open search XML file."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.Other; }
        }

        public override string Id
        {
            get { return "Search"; }
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
            get { return "Search"; }
        }

        protected override async Task AddFeature()
        {
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Constants\HomeController\HomeControllerAction.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Constants\HomeController\HomeControllerRoute.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Constants\CacheProfileName.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Controllers\HomeController.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Views\Home\Index.cshtml");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Views\Shared\_Layout.cshtml");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"config.json");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"ReadMe.html");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"ServiceCollectionExtensions.cs");
        }

        protected override async Task RemoveFeature()
        {
            this.ProjectService.DeleteDirectory(@"Services\OpenSearch");

            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Constants\HomeController\HomeControllerAction.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Constants\HomeController\HomeControllerRoute.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Constants\CacheProfileName.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Controllers\HomeController.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Views\Home\Index.cshtml");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Views\Shared\_Layout.cshtml");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"config.json");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"ReadMe.html");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"ServiceCollectionExtensions.cs");
        }
    }
}
