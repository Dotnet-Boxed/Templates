namespace Boilerplate.FeatureSelection.Features
{
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;

    public class WebServerFeature : MultiChoiceFeature
    {
        private readonly IFeatureItem iis;
        private readonly IFeatureItem kestrel;
        private readonly IFeatureItem nginx;

        public WebServerFeature(IProjectService projectService)
            : base(projectService)
        {
            this.kestrel = new FeatureItem(
                "Kestrel",
                "Kestrel",
                "A web server for ASP.NET Core. Not intended to be internet facing as it has not been security tested. IIS and Nginx require the Kestrel webs server, so this option is enabled by default.",
                1,
                "/Boilerplate.FeatureSelection;component/Assets/Kestrel.png")
            {
                IsEnabled = false,
                IsSelected = true
            };
            this.Items.Add(this.kestrel);

            this.iis = new FeatureItem(
                "IIS",
                "Internet Information Services (IIS) or Azure",
                "Internet Information Services (IIS) for Windows Server is a flexible, secure and manageable Web server for hosting anything on the Web. Select this option if you are deploying your site to Azure web apps.",
                2,
                "/Boilerplate.FeatureSelection;component/Assets/IIS.png",
                true);
            this.Items.Add(this.iis);

            this.nginx = new FeatureItem(
                "Nginx",
                "Nginx",
                "Nginx is a free, open-source, cross-platform high-performance HTTP server and reverse proxy, as well as an IMAP/POP3 proxy server. It does have a Windows version but it's not very fast and IIS is better on that platform.",
                3,
                "/Boilerplate.FeatureSelection;component/Assets/Nginx.png",
                true);
            this.Items.Add(this.nginx);
        }

        public override string Description
        {
            get { return "The web server you want to use to host the site."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.Project; }
        }

        public override string Id
        {
            get { return "WebServer"; }
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
            get { return "Web Server"; }
        }

        public override async Task AddOrRemoveFeature()
        {
            if (!this.iis.IsSelected)
            {
                await this.ProjectService.EditCommentInFile(this.iis.CommentName, EditCommentMode.DeleteCode, "Program.cs");
                await this.ProjectService.EditCommentInFile(this.iis.CommentName, EditCommentMode.DeleteCode, "project.json");
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
