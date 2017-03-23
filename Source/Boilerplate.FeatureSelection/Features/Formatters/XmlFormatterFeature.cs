namespace Boilerplate.FeatureSelection.Features
{
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;

    public class XmlFormatterFeature : MultiChoiceFeature
    {
        private IFeatureItem both;
        private IFeatureItem dataContractSerializer;
        private IFeatureItem none;
        private IFeatureItem xmlSerializer;

        public XmlFormatterFeature(IProjectService projectService)
            : base(projectService)
        {
            this.none = new FeatureItem(
                "None",
                "None",
                "No Xml Formatter",
                1)
            {
                IsSelected = true
            };
            this.Items.Add(this.none);

            this.dataContractSerializer = new FeatureItem(
                "DataContractSerializer",
                "DataContractSerializer",
                "Add the DataContractSerializer based input and output formatters.",
                2);
            this.Items.Add(this.dataContractSerializer);

            this.xmlSerializer = new FeatureItem(
                "XmlSerializer",
                "XmlSerializer",
                "Add the XmlSerializer based input and output formatters.",
                3);
            this.Items.Add(this.xmlSerializer);

            this.both = new FeatureItem(
                "Both",
                "Both",
                "Add the DataContractSerializer and XmlSerializer based input and output formatters.",
                4);
            this.Items.Add(this.both);
        }

        public override string Description
        {
            get { return "Choose whether to use the XML input/output formatter and which serializer to use."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.Formatters; }
        }

        public override string Id
        {
            get { return "XmlFormatter"; }
        }

        public override bool IsDefaultSelected
        {
            get { return this.none.IsSelected; }
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
            get { return "XML Formatter"; }
        }

        public override async Task AddOrRemoveFeature()
        {
            if (this.none.IsSelected)
            {
                await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, this.ProjectService.ProjectFileName);
            }
            else
            {
                await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, this.ProjectService.ProjectFileName);
            }

            if (this.dataContractSerializer.IsSelected || this.both.IsSelected)
            {
                await this.ProjectService.EditCommentInFile(
                    this.dataContractSerializer.CommentName,
                    EditCommentMode.LeaveCodeUnchanged,
                    "Startup.cs");
            }
            else
            {
                await this.ProjectService.EditCommentInFile(
                    this.dataContractSerializer.CommentName,
                    EditCommentMode.DeleteCode,
                    "Startup.cs");
            }

            if (this.xmlSerializer.IsSelected || this.both.IsSelected)
            {
                await this.ProjectService.EditCommentInFile(
                    this.xmlSerializer.CommentName,
                    EditCommentMode.LeaveCodeUnchanged,
                    "Startup.cs");
            }
            else
            {
                await this.ProjectService.EditCommentInFile(
                    this.xmlSerializer.CommentName,
                    EditCommentMode.DeleteCode,
                    "Startup.cs");
            }
        }
    }
}