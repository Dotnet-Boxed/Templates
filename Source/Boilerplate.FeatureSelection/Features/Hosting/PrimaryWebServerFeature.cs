namespace Boilerplate.FeatureSelection.Features
{
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;

    public class PrimaryWebServerFeature : MultiChoiceFeature
    {
        private readonly IFeatureItem kestrel;
        private readonly IFeatureItem webListener;

        public PrimaryWebServerFeature(IProjectService projectService)
            : base(projectService)
        {
            this.kestrel = new FeatureItem(
                "Kestrel",
                "Kestrel",
                "A web server for ASP.NET Core. Not intended to be internet facing as it has not been security tested. IIS or Nginx should be placed in front as reverse proxy web servers.",
                1,
                "/Boilerplate.FeatureSelection;component/Assets/Kestrel.png")
            {
                IsSelected = true
            };
            this.Items.Add(this.kestrel);

            this.webListener = new FeatureItem(
                "WebListener",
                "Web Listener",
                "WebListener is a Windows only web server. It gives you the option to take advantage of Windows specific features, like Windows authentication, port sharing, HTTPS with SNI, HTTP/2 over TLS (Windows 10), direct file transmission, and response caching WebSockets (Windows 8).",
                2,
                "/Boilerplate.FeatureSelection;component/Assets/WebListener.png")
            {
                IsSelected = false
            };
            this.Items.Add(this.webListener);
        }

        public override string Description
        {
            get { return "The primary web server you want to use to host the site."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.Hosting; }
        }

        public override string Id
        {
            get { return "PrimaryWebServer"; }
        }

        public override bool IsMultiSelect
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
            get { return "Primary Web Server"; }
        }

        public override async Task AddOrRemoveFeature()
        {
            if (this.kestrel.IsSelected)
            {
                await this.ProjectService.EditCommentInFile(this.kestrel.CommentName, EditCommentMode.LeaveCodeUnchanged, "Program.cs");
                await this.ProjectService.EditCommentInFile(this.kestrel.CommentName, EditCommentMode.LeaveCodeUnchanged, this.ProjectService.ProjectFileName);
            }
            else
            {
                await this.ProjectService.EditCommentInFile(this.kestrel.CommentName, EditCommentMode.DeleteCode, "Program.cs");
                await this.ProjectService.EditCommentInFile(this.kestrel.CommentName, EditCommentMode.DeleteCode, this.ProjectService.ProjectFileName);
            }

            if (this.webListener.IsSelected)
            {
                await this.ProjectService.EditCommentInFile(this.webListener.CommentName, EditCommentMode.LeaveCodeUnchanged, "Program.cs");
                await this.ProjectService.EditCommentInFile(this.webListener.CommentName, EditCommentMode.LeaveCodeUnchanged, this.ProjectService.ProjectFileName);
            }
            else
            {
                await this.ProjectService.EditCommentInFile(this.webListener.CommentName, EditCommentMode.DeleteCode, "Program.cs");
                await this.ProjectService.EditCommentInFile(this.webListener.CommentName, EditCommentMode.DeleteCode, this.ProjectService.ProjectFileName);
            }
        }
    }
}
