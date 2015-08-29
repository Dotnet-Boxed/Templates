namespace Boilerplate.Wizard.Features
{
    using System.Threading.Tasks;
    using Constants;
    using Boilerplate.Wizard.Services;

    public class BsonFormatterFeature : MultiChoiceFeature
    {
        public BsonFormatterFeature(IProjectService projectService, FeatureSet featureSet)
            : base(projectService,
                  new IFeatureItem[]
                  {
                      new FeatureItem(
                          "None",
                          "Removes the BSON formatter.",
                          featureSet == FeatureSet.Mvc6 ? 1 : 3)
                      {
                          IsSelected = featureSet == FeatureSet.Mvc6
                      },
                      new FeatureItem(
                          "Camel-Case (e.g. camelCase)",
                          "The first character of the variable starts with a lower-case. Each word in the variable name after that starts with an upper-case character.",
                          featureSet == FeatureSet.Mvc6 ? 2 : 1)
                      {
                          IsSelected = featureSet == FeatureSet.Mvc6Api
                      },
                      new FeatureItem(
                          "Title Case (e.g. TitleCase)",
                          "Each word in the variable name starts with an upper-case character.",
                          featureSet == FeatureSet.Mvc6 ? 3 : 2)
                  })
        {
        }

        public override string Description
        {
            get { return "Choose whether to use the BSON input/output formatter and whether it should use camel or title casing."; }
        }

        public override string GroupName
        {
            get { return FeatureGroupName.Formatters; }
        }

        public override int Order
        {
            get { return 3; }
        }

        public override string Title
        {
            get { return "BSON Formatter"; }
        }

        public override async Task AddOrRemoveFeature()
        {
            if (this.Items[0].IsSelected)
            {
                await this.ProjectService.DeleteComment(
                    "BsonFormatter-CamelCase",
                    DeleteCommentMode.StartEndComment);
                await this.ProjectService.DeleteComment(
                    "BsonFormatter-TitleCase",
                    DeleteCommentMode.StartEndCommentAndCode);
                await this.ProjectService.DeleteComment(
                    "BsonFormatter-None",
                    DeleteCommentMode.StartEndCommentAndCode);
            }
            else if (this.Items[1].IsSelected)
            {
                await this.ProjectService.DeleteComment(
                    "BsonFormatter-CamelCase",
                    DeleteCommentMode.StartEndCommentAndCode);
                await this.ProjectService.DeleteComment(
                    "BsonFormatter-TitleCase",
                    DeleteCommentMode.StartEndCommentAndUncommentCode);
                await this.ProjectService.DeleteComment(
                    "BsonFormatter-None",
                    DeleteCommentMode.StartEndCommentAndCode);
            }
            else
            {
                await this.ProjectService.DeleteComment(
                    "BsonFormatter-CamelCase",
                    DeleteCommentMode.StartEndCommentAndCode);
                await this.ProjectService.DeleteComment(
                    "BsonFormatter-TitleCase",
                    DeleteCommentMode.StartEndCommentAndCode);
                await this.ProjectService.DeleteComment(
                    "BsonFormatter-None",
                    DeleteCommentMode.StartEndCommentAndUncommentCode);
            }
        }
    }
}