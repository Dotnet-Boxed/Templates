namespace Boilerplate.FeatureSelection.Features
{
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;

    public class TwitterCardFeature : BinaryChoiceFeature
    {
        public TwitterCardFeature(IProjectService projectService)
             : base(projectService)
        {
        }

        public override string Description
        {
            get { return "Adds Twitter Card meta tags to the head of the HTML pages to improve the experience when the page is shared on Twitter."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.Social; }
        }

        public override string Icon
        {
            get { return "/Boilerplate.FeatureSelection;component/Assets/Twitter.png"; }
        }

        public override string Id
        {
            get { return "TwitterCard"; }
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
            get { return "Twitter Cards"; }
        }

        protected override async Task AddFeature()
        {
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Views\Home\About.cshtml");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Views\Home\Contact.cshtml");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Views\Home\Index.cshtml");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"ReadMe.html");
        }

        protected override async Task RemoveFeature()
        {
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Views\Home\About.cshtml");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Views\Home\Contact.cshtml");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Views\Home\Index.cshtml");
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"ReadMe.html");
        }
    }
}
