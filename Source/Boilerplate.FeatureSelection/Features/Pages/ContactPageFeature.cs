namespace Boilerplate.FeatureSelection.Features
{
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;

    public class ContactPageFeature : BinaryChoiceFeature
    {
        public ContactPageFeature(IProjectService projectService)
            : base(projectService)
        {
        }

        public override string Description
        {
            get { return "Adds a contact page action and view to the HomeController, where you can explain how you can be contacted."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.Pages; }
        }

        public override string Id
        {
            get { return "ContactPage"; }
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
            get { return "Contact Page"; }
        }

        protected override async Task AddFeature()
        {
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Constants\HomeController\HomeControllerAction.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Constants\HomeController\HomeControllerRoute.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Controllers\HomeController.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Services\Sitemap\SitemapService.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Views\Home\Index.cshtml");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Views\Shared\_Layout.cshtml");
        }

        protected override async Task RemoveFeature()
        {
            this.ProjectService.DeleteFile(@"Views\Home\Contact.cshtml");

            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Constants\HomeController\HomeControllerAction.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Constants\HomeController\HomeControllerRoute.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Controllers\HomeController.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Services\Sitemap\SitemapService.cs");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Views\Home\Index.cshtml");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Views\Shared\_Layout.cshtml");
        }
    }
}
