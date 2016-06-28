namespace Boilerplate.FeatureSelection.Features
{
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;

    public class NoJavaScriptLintingFeature : BinaryChoiceFeature
    {
        private readonly JavaScriptCodeStyleFeature javaScriptCodeStyleFeature;
        private readonly JavaScriptHintFeature javaScriptHintFeature;
        private readonly TypeScriptFeature typeScriptFeature;

        public NoJavaScriptLintingFeature(
            IProjectService projectService,
            JavaScriptCodeStyleFeature javaScriptCodeStyleFeature,
            JavaScriptHintFeature javaScriptHintFeature,
            TypeScriptFeature typeScriptFeature)
            : base(projectService)
        {
            this.javaScriptCodeStyleFeature = javaScriptCodeStyleFeature;
            this.javaScriptHintFeature = javaScriptHintFeature;
            this.typeScriptFeature = typeScriptFeature;

            this.javaScriptCodeStyleFeature.PropertyChanged += this.OnOtherFeaturePropertyChanged;
            this.javaScriptHintFeature.PropertyChanged += this.OnOtherFeaturePropertyChanged;
            this.typeScriptFeature.PropertyChanged += this.OnOtherFeaturePropertyChanged;
        }

        public override string Description
        {
            get { return "Deletes the js-lint Gulp task if all features are deselected."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.Hidden; }
        }

        public override string Id
        {
            get { return "JavaScriptLint"; }
        }

        public override bool IsSelectedDefault
        {
            get { return false; }
        }

        public override bool IsSelected
        {
            get
            {
                return !this.javaScriptCodeStyleFeature.IsSelected &&
                    !this.javaScriptHintFeature.IsSelected &&
                    !this.typeScriptFeature.IsSelected;
            }
        }

        public override bool IsVisible
        {
            get { return false; }
        }

        public override int Order
        {
            get { return 3; }
        }

        public override string Title
        {
            get { return "JavaScript Lint"; }
        }

        private void OnOtherFeaturePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (string.Equals(e.PropertyName, nameof(this.IsSelected)))
            {
                this.OnPropertyChanged(nameof(this.IsSelected), nameof(this.IsDefaultSelected));
            }
        }

        protected override async Task AddFeature()
        {
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.DeleteCode, @"gulpfile.js");
        }

        protected override async Task RemoveFeature()
        {
            await this.ProjectService.EditCommentInFile(this.Id, EditCommentMode.LeaveCodeUnchanged, @"gulpfile.js");
        }
    }
}
