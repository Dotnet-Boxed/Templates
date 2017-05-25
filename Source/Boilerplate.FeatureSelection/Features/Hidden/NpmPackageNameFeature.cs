namespace Boilerplate.FeatureSelection.Features
{
    using System.Linq;
    using System.Threading.Tasks;
    using Boilerplate.FeatureSelection.Services;

    public class NpmPackageNameFeature : BinaryChoiceFeature
    {
        private const int MaxNameLength = 50;

        public NpmPackageNameFeature(IProjectService projectService)
            : base(projectService)
        {
        }

        public override string Description
        {
            get { return "Sets the name property in a package.json file."; }
        }

        public override IFeatureGroup Group
        {
            get { return FeatureGroups.Hidden; }
        }

        public override string Id
        {
            get { return "NpmPackageName"; }
        }

        public override bool IsSelectedDefault
        {
            get { return true; }
        }

        public override bool IsVisible
        {
            get { return false; }
        }

        public override int Order
        {
            get { return 4; }
        }

        public override string Title
        {
            get { return "Set the package.json package name"; }
        }

        protected override async Task AddFeature()
        {
            var name = new string(this.ProjectService
                .ProjectName
                .TrimStart('.', '_')
                .ToLower()
                .Replace('.', '-')
                .Replace('_', '-')
                .Where(x => char.IsLetterOrDigit(x) || x == '-')
                .ToArray());
            name = name.Length > MaxNameLength ? name.Substring(0, MaxNameLength) : name;

            await this.ProjectService.ReplaceInFile(
                "asp.net-core-boilerplate",
                name,
                @"package.json");
        }
    }
}