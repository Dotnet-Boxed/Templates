namespace Boilerplate.FeatureSelection.Features
{
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;

    public class ReverseProxyWebServerFeature : MultiChoiceFeature
    {
        private readonly IFeatureItem iis;
        private readonly IFeatureItem nginx;

        public ReverseProxyWebServerFeature(IProjectService projectService)
            : base(projectService)
        {
            this.iis = new FeatureItem(
                "IIS",
                "Internet Information Services (IIS) or Azure",
                "Internet Information Services (IIS) for Windows Server is a flexible, secure and manageable Web server for hosting anything on the Web. Select this option if you are deploying your site to Azure web apps.",
                2,
                "/Boilerplate.FeatureSelection;component/Assets/IIS.png")
            {
                IsSelected = true
            };
            this.Items.Add(this.iis);

            this.nginx = new FeatureItem(
                "Nginx",
                "Nginx",
                "Nginx is a free, open-source, cross-platform high-performance HTTP server and reverse proxy, as well as an IMAP/POP3 proxy server. It does have a Windows version but it's not very fast and IIS is better on that platform.",
                3,
                "/Boilerplate.FeatureSelection;component/Assets/Nginx.png")
            {
                IsSelected = true
            };
            this.Items.Add(this.nginx);
        }

        public override string Description
        {
            get { return "The internet facing reverse proxy web server you want to use in front of the primary web server to host the site."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.Hosting; }
        }

        public override string Id
        {
            get { return "ReverseProxyWebServer"; }
        }

        public override bool IsMultiSelect
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
            get { return "Reverse Proxy Web Server"; }
        }

        public override async Task AddOrRemoveFeature()
        {
            if (this.iis.IsSelected)
            {
                await this.ProjectService.EditCommentInFile(this.iis.CommentName, EditCommentMode.LeaveCodeUnchanged, "Program.cs");
                await this.ProjectService.EditCommentInFile(this.iis.CommentName, EditCommentMode.LeaveCodeUnchanged, this.ProjectService.ProjectFileName);
                await this.ProjectService.EditCommentInFile(this.iis.CommentName, EditCommentMode.LeaveCodeUnchanged, "ReadMe.html");
            }
            else
            {
                await this.ProjectService.EditCommentInFile(this.iis.CommentName, EditCommentMode.DeleteCode, "Program.cs");
                await this.ProjectService.EditCommentInFile(this.iis.CommentName, EditCommentMode.DeleteCode, this.ProjectService.ProjectFileName);
                await this.ProjectService.EditCommentInFile(this.iis.CommentName, EditCommentMode.DeleteCode, "ReadMe.html");
                this.ProjectService.DeleteFile("web.config");
            }

            if (!this.nginx.IsSelected)
            {
                this.ProjectService.DeleteFile("mime.types");
                this.ProjectService.DeleteFile("nginx.conf");
            }
        }
    }
}
