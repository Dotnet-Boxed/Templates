namespace Boilerplate.Wizard.Features
{
    using System.Threading.Tasks;
    using Boilerplate.Wizard.Constants;
    using Boilerplate.Wizard.Services;

    public class XmlFormatterFeature : MultiChoiceFeature
    {
        private const string StartupFormattersCs = "Startup.Formatters.cs";

        private readonly IFeatureItem dataContractSerializer;
        private readonly IFeatureItem none;
        private readonly IFeatureItem xmlSerializer;

        public XmlFormatterFeature(IProjectService projectService, FeatureSet featureSet)
            : base(projectService)
        {
            this.none = new FeatureItem(
                "None",
                "None",
                "Removes the XML formatter.",
                featureSet == FeatureSet.Mvc6 ? 1 : 3)
            {
                IsSelected = featureSet == FeatureSet.Mvc6
            };
            this.Items.Add(none);

            this.dataContractSerializer = new FeatureItem(
                "DataContractSerializer",
                "DataContractSerializer",
                "Include an XML input and output formatter using the DataContractSerializer.",
                featureSet == FeatureSet.Mvc6 ? 2 : 1)
            {
                IsSelected = featureSet == FeatureSet.Mvc6Api
            };
            this.Items.Add(dataContractSerializer);

            this.xmlSerializer = new FeatureItem(
                "XmlSerializer",
                "XmlSerializer",
                "Include an XML input and output formatter using the XmlSerializer.",
                featureSet == FeatureSet.Mvc6 ? 3 : 2);
            this.Items.Add(xmlSerializer);
        }

        public override string Description
        {
            get { return "Choose whether to use an XML input/output formatter and whether to use the DataContract or XmlSerialiser."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.Formatters; }
        }

        public override string Id
        {
            get { return "XmlFormatter"; }
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
                await this.ProjectService.EditComment(
                    this.none.CommentName,
                    EditCommentMode.LeaveCodeUnchanged,
                    StartupFormattersCs);
                await this.ProjectService.EditComment(
                    this.dataContractSerializer.CommentName,
                    EditCommentMode.DeleteCode,
                    StartupFormattersCs);
                await this.ProjectService.EditComment(
                    this.xmlSerializer.CommentName,
                    EditCommentMode.DeleteCode,
                    StartupFormattersCs);
            }
            else if (this.dataContractSerializer.IsSelected)
            {
                await this.ProjectService.EditComment(
                    this.none.CommentName,
                    EditCommentMode.LeaveCodeUnchanged,
                    StartupFormattersCs);
                await this.ProjectService.EditComment(
                    this.dataContractSerializer.CommentName,
                    EditCommentMode.DeleteCode,
                    StartupFormattersCs);
                await this.ProjectService.EditComment(
                    this.xmlSerializer.CommentName,
                    EditCommentMode.DeleteCode,
                    StartupFormattersCs);
            }
            else if (this.xmlSerializer.IsSelected)
            {
                await this.ProjectService.EditComment(
                    this.none.CommentName,
                    EditCommentMode.LeaveCodeUnchanged,
                    StartupFormattersCs);
                await this.ProjectService.EditComment(
                    this.dataContractSerializer.CommentName,
                    EditCommentMode.DeleteCode,
                    StartupFormattersCs);
                await this.ProjectService.EditComment(
                    this.xmlSerializer.CommentName,
                    EditCommentMode.DeleteCode,
                    StartupFormattersCs);
            }
        }
    }
}