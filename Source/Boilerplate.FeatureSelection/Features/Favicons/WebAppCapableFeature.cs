namespace Boilerplate.FeatureSelection.Features
{
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;

    public class WebAppCapableFeature : BinaryChoiceFeature
    {
        public WebAppCapableFeature(IProjectService projectService)
            : base(projectService)
        {
        }

        public override string Description
        {
            get { return "Treat the site like an app on iOS and Android Chrome devices by adding a splash screen, toolbar theme colour and hiding the address bar."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.Favicons; }
        }

        public override string Id
        {
            get { return "WebAppCapable"; }
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
            get { return 8; }
        }

        public override string Title
        {
            get { return "Web-App Capable"; }
        }

        protected override async Task AddFeature()
        {
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"Views\Shared\_Layout.cshtml");
        }

        protected override async Task RemoveFeature()
        {
            this.ProjectService.DeleteFile(@"\img\icons\apple-touch-startup-image-1536x2008.png");
            this.ProjectService.DeleteFile(@"\img\icons\apple-touch-startup-image-1496x2048.png");
            this.ProjectService.DeleteFile(@"\img\icons\apple-touch-startup-image-768x1004.png");
            this.ProjectService.DeleteFile(@"\img\icons\apple-touch-startup-image-748x1024.png");
            this.ProjectService.DeleteFile(@"\img\icons\apple-touch-startup-image-640x1096.png");
            this.ProjectService.DeleteFile(@"\img\icons\apple-touch-startup-image-640x920.png");
            this.ProjectService.DeleteFile(@"\img\icons\apple-touch-startup-image-320x460.png");

            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"Views\Shared\_Layout.cshtml");
        }
    }
}
