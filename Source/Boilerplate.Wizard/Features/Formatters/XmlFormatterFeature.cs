namespace Boilerplate.Wizard.Features
{
    using System.Threading.Tasks;
    using Boilerplate.Wizard.Services;
    using Constants;

    public class XmlFormatterFeature : MultiChoiceFeature
    {
        public XmlFormatterFeature(IProjectService projectService, FeatureSet featureSet)
            : base(projectService,
                  new IFeatureItem[]
                  {
                      new FeatureItem(
                          "None",
                          "Removes the XML formatter.",
                          featureSet == FeatureSet.Mvc6 ? 1 : 3)
                      {
                          IsSelected = featureSet == FeatureSet.Mvc6
                      },
                      new FeatureItem(
                          "DataContractSerializer",
                          "Include an XML input and output formatter using the DataContractSerializer.",
                          featureSet == FeatureSet.Mvc6 ? 2 : 1)
                      {
                          IsSelected = featureSet == FeatureSet.Mvc6Api
                      },
                      new FeatureItem(
                          "XmlSerializer",
                          "Include an XML input and output formatter using the XmlSerializer.",
                          featureSet == FeatureSet.Mvc6 ? 3 : 2),
                      
                  })
        {
        }

        public override string Description
        {
            get { return "Choose whether to use an XML input/output formatter and whether to use the DataContract or XmlSerialiser."; }
        }

        public override string GroupName
        {
            get { return FeatureGroupName.Formatters; }
        }

        public override int Order
        {
            get { return 4; }
        }

        public override string Title
        {
            get { return "XML Formatter"; }
        }

        public override async Task AddOrRemoveFeature()
        {
            if (this.Items[0].IsSelected)
            {
                await this.ProjectService.DeleteComment(
                    "JsonFormatter-CamelCase",
                    DeleteCommentMode.StartEndComment);
                await this.ProjectService.DeleteComment(
                    "JsonFormatter-TitleCase",
                    DeleteCommentMode.StartEndCommentAndCode);
                await this.ProjectService.DeleteComment(
                    "JsonFormatter-None",
                    DeleteCommentMode.StartEndCommentAndCode);
            }
            else if (this.Items[1].IsSelected)
            {
                await this.ProjectService.DeleteComment(
                    "JsonFormatter-CamelCase",
                    DeleteCommentMode.StartEndCommentAndCode);
                await this.ProjectService.DeleteComment(
                    "JsonFormatter-TitleCase",
                    DeleteCommentMode.StartEndCommentAndUncommentCode);
                await this.ProjectService.DeleteComment(
                    "JsonFormatter-None",
                    DeleteCommentMode.StartEndCommentAndCode);
            }
            else
            {
                await this.ProjectService.DeleteComment(
                    "JsonFormatter-CamelCase",
                    DeleteCommentMode.StartEndCommentAndCode);
                await this.ProjectService.DeleteComment(
                    "JsonFormatter-TitleCase",
                    DeleteCommentMode.StartEndCommentAndCode);
                await this.ProjectService.DeleteComment(
                    "JsonFormatter-None",
                    DeleteCommentMode.StartEndCommentAndUncommentCode);
            }
        }
    }
}