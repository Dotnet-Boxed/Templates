namespace Boilerplate.FeatureSelection.Features
{
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;

    public class JsonSerializerSettingsFeature : MultiChoiceFeature
    {
        private const string StartupFormattersCs = "Startup.cs";

        private readonly IFeatureItem camelCase;
        private readonly IFeatureItem titleCase;

        public JsonSerializerSettingsFeature(IProjectService projectService)
            : base(projectService)
        {
            this.camelCase = new FeatureItem(
                "CamelCase",
                "Camel-Case (e.g. camelCase)",
                "The first character of the variable starts with a lower-case. Each word in the variable name after that starts with an upper-case character.",
                1)
            {
                IsSelected = true
            };
            this.Items.Add(this.camelCase);

            this.titleCase = new FeatureItem(
                "TitleCase",
                "Title-Case (e.g. TitleCase)",
                "Each word in the variable name starts with an upper-case character.",
                2);
            this.Items.Add(this.titleCase);
        }

        public override string Description
        {
            get { return "Choose whether the JSON serializer uses camel or title casing."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.Formatters; }
        }

        public override string Id
        {
            get { return "JsonSerializerSettings"; }
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
            get { return "JSON Serializer Settings"; }
        }

        public override async Task AddOrRemoveFeature()
        {
            if (this.camelCase.IsSelected)
            {
                await this.ProjectService.EditCommentInFile(
                    this.Id,
                    EditCommentMode.LeaveCodeUnchanged,
                    StartupFormattersCs);
            }
            else if (this.titleCase.IsSelected)
            {
                await this.ProjectService.EditCommentInFile(
                    this.Id,
                    EditCommentMode.DeleteCode,
                    StartupFormattersCs);
            }
        }
    }
}