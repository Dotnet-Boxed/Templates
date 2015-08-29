namespace Boilerplate.Wizard.Features
{
    using System.Threading.Tasks;
    using Constants;
    using Boilerplate.Wizard.Services;

    public class JsonFormatterFeature : MultiChoiceFeature
    {
        public JsonFormatterFeature(IProjectService projectService)
            : base(projectService,
                  new IFeatureItem[]
                  {
                      new FeatureItem(
                          "Camel-Case (e.g. camelCase)",
                          "The first character of the variable starts with a lower-case. Each word in the variable name after that starts with an upper-case character.",
                          1)
                      {
                          IsSelected = true
                      },
                      new FeatureItem(
                          "Title Case (e.g. TitleCase)",
                          "Each word in the variable name starts with an upper-case character.",
                          2),
                      new FeatureItem(
                          "None",
                          "Removes the JSON formatter.",
                          3)
                  })
        {
        }

        public override string Description
        {
            get { return "Choose whether to use the JSON input/output formatter and whether it should use camel or title casing."; }
        }

        public override string GroupName
        {
            get { return FeatureGroupName.Formatters; }
        }

        public override int Order
        {
            get { return 2; }
        }

        public override string Title
        {
            get { return "JSON Formatter"; }
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